using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyMotion : MonoBehaviour 
{
	PopupText _popupText;
	
	public GameObject Player;
	public Transform EnemySpawn;
	private Transform thisTransform;
	
	public enum classMonster
	{
		Warrior, Mage, Archer 
	}
	public classMonster _classMonster;
	
	public Color _color;
	public float damage;
	UnityEngine.AI.NavMeshAgent agent;
	
	public float lookRadiusToPlayer = 10.0f;
	public float EnemyAttackTime = 0f;
	public float normalEnemyAttackTime = 5.5f;
	private Animator anim;
	public bool Attack, playerAttackEnemy, playerIsLookRadius = false;
	
	void Start()
	{
		thisTransform = transform;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		Player = GameObject.FindWithTag("Player") as GameObject;
		anim = thisTransform.GetChild(0).GetComponent<Animator>();
		_popupText = GameObject.FindObjectOfType<PopupText>();
	}
	
	void Update()
	{
		State();
		
		if(Attack)
		{
			if(_classMonster == classMonster.Warrior)
			{
				EnemyAttackTime -= Time.deltaTime;
				
			    if(EnemyAttackTime <= 0)
			    {
				    GetComponent<EnemyAttributes>().AttackTimeWarrior();
				    TextToPlayer();
				    EnemyAttackTime = normalEnemyAttackTime;
				}
			}
			else if(_classMonster == classMonster.Mage)
			{
				EnemyAttackTime -= Time.deltaTime;
				if(EnemyAttackTime <= 0)
			    {
				    GetComponent<EnemyAttributes>().AttackTimeMage();
				    TextToPlayer();
				    EnemyAttackTime = normalEnemyAttackTime;
				}
			}
			else if(_classMonster == classMonster.Archer)
			{
			    
			}
		}
	}
	
	public void TextToPlayer()
	{
		char c = '-';
		_popupText.instancePopupText(Player.transform.position, "DMG: ", (int)damage, _color, c);
	}
	
	private void State()
	{
		float distance = Vector3.Distance(Player.transform.position, this.transform.position);
		if(distance <= lookRadiusToPlayer && Player.GetComponent<PlayerAttributes>().livePlayer && this.GetComponent<EnemyAttributes>().liveEnemy)
		{
	        if(Player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
			{
			    agent.speed = 6.5f;
			}
			else if(Player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
			{
			    agent.speed = 6.5f;
			}
			
			else if(Player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
			{
			    agent.speed = 5f;
			}
			
			playerIsLookRadius = true;
			anim.SetBool("Idle", false);
			anim.SetBool("Run", true);
			
			if(distance <= GetComponent<EnemyAttributes>().distAttack)
			{
				anim.SetBool("Run", false);
				anim.SetBool("FightStance", true);
				Attack = true;
				FaceToPlayer();
			}
			else
			{
			    Attack = false;
			    transform.LookAt(Player.transform);
				anim.SetBool("FightStance", false);
				anim.SetBool("Run", true);
				agent.SetDestination(Player.transform.position);
			}
		}
		
		else if(playerAttackEnemy && Player.GetComponent<PlayerAttributes>().livePlayer && this.GetComponent<EnemyAttributes>().liveEnemy)
		{
		    transform.LookAt(Player.transform);
			anim.SetBool("Idle", false);
		    agent.SetDestination(Player.transform.position);
		}
		else
		{
			Attack = false;
		    playerIsLookRadius = false;
			if(!playerAttackEnemy && !playerIsLookRadius && !Player.GetComponent<PlayerAttributes>().livePlayer)
			{
				Vector3 diff = transform.position - EnemySpawn.transform.position;
			    float curDistance = diff.sqrMagnitude;
			    if(curDistance > 10.0f * 10.0f)
				{
				    transform.LookAt(EnemySpawn.transform);
					agent.SetDestination(EnemySpawn.position);
					anim.SetBool("Idle", false);
					anim.SetBool("Run", true);
				}
				else
				{   
					anim.SetBool("Run", false);
				    anim.SetBool("Idle", true);
				}
			}
			
		}
	}
	
	private void FaceToPlayer()
	{
		Vector3 direction = (Player.transform.position - thisTransform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, lookRotation, Time.deltaTime * 5f);
	}
	
	/*private IEnumerator AttackTime()
		{
		yield return new WaitForSeconds(EnemyAttackTime);
		while(true)
		{
		GetComponent<EnemyAttributes>().AttackTime();
		}
	}*/
}
