using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum rangeMonster
{
	Low, Average, Strong, Advanced
}

public enum typeMonster
{
	Common, Boss
}
public class EnemyAttributes : MonoBehaviour 
{
	PopupText _popupText;
	nameNPC _nameNPC;
	public GameObject SkillPrefab;
	private Transform startSkillPosition;
	public string name;
	
	private int calculateDmg;
	public int Id;
	//MenuDie _menuDie;
	GameHelper _gameHelper;
	public GameObject Player;
	//тексты 
	public Text  HpText;
	
	public Color _expColor;
	public Color _dmgColor;
	public Color _critColor;
	
	public rangeMonster _rangeMonster;
	public typeMonster _typeMonster;
	
	//аттрибуты монстра
	public float Exp = 10.0f;
	public int Gold = 10;
	
	public float critChangeMin = 0f;
	public float critChangeMax = 1f;
	public float critChangeStat = 0.1f;
	
	public int monsterLevel = 1;
	public float Health = 100;
	public float MaxHealth = 100;
	public int EnemyDamage = 7;
	public Image EnemyHealthSlider;
	public float distAttack = 3.0f;
	private Animator anim;
	private AudioSource audioWarrior;
	private AudioSource audioMage;
	
	public bool liveEnemy = true;
	
	HistoryGame _historyGame;
	
	//public GameObject SpawnParticle;
	
	private void Start()
	{
		anim = transform.GetChild(0).GetComponent<Animator>();
		_gameHelper = GameObject.FindObjectOfType<GameHelper>();
		_historyGame = GameObject.FindObjectOfType<HistoryGame>();
		_popupText = GameObject.FindObjectOfType<PopupText>();
		Player = GameObject.FindWithTag("Player");
		_nameNPC = GameObject.FindObjectOfType<nameNPC>();
		
		_nameNPC.changeNameEnemy(this.gameObject, name, monsterLevel);
		//ButtonLowAttack = GameObject.FindWithTag("AttackStockButton");
		//_menuDie = GameObject.FindObjectOfType<MenuDie>();
		//EnemyHealthSlider = GameObject.FindGameObjectWithTag("EnemySlider").GetComponent<Slider>();
		//StartCoroutine("AttackTime");
		startSkillPosition = transform.GetChild(3);
		audioWarrior = GameObject.Find("Audios").transform.GetChild(0).GetComponent<AudioSource>();
		audioMage = GameObject.Find("Audios").transform.GetChild(1).GetComponent<AudioSource>();
		//Invoke("stopSpawnParticle", 2.0f);
	}
	
	/*private void stopSpawnParticle()
		{
		SpawnParticle.SetActive(false);
	}*/
	
	private void Drop()
	{
	    this.gameObject.GetComponent<EnemyDropItems>().instanceItem();
	}
	
	
	private void Update()
	{
		float procentHp = Mathf.Round(Health * 100) / MaxHealth;
		HpText.text = string.Format("{0:0}", procentHp) + "%";
		EnemyHealthSlider.fillAmount = Health / MaxHealth;
		
		if(liveEnemy)
		{
			if(Health <= 0)
			{
				Invoke("Die", 0.5f);
				Destroy(this.gameObject, 10f);
				liveEnemy = false;
			}
		}
	}
	
	private void calculateDmgDefence()
	{
		if(EnemyDamage <= this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerDefence)
		{
			if(Random.Range(critChangeMin, critChangeMax) < critChangeStat)
			{
				calculateDmg = 3;
			}
			
			else
			{
				calculateDmg = 1;
			}
			
		}
		else
		{
			if(Random.Range(critChangeMin, critChangeMax) < critChangeStat)
			{
				calculateDmg = (EnemyDamage - this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerDefence) * 3;
			}
			
			else
			{
				if(this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerDefence  > EnemyDamage)
				{
					calculateDmg = 1;
				}
				else
				{
					calculateDmg = EnemyDamage - this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerDefence;
				}
				
				
			}
			
		}
	}
	
	public void AttackTimeWarrior()
	{
		if(Health > 0)
		{
			calculateDmgDefence();
		this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth -= calculateDmg;
		anim.SetTrigger("Punch");
		this.gameObject.GetComponent<EnemyMotion>().damage = calculateDmg;
		audioWarrior.Play();
		}
		
		else
		{
			Health = 0;
		}
		
		if(this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth <= 0)
		{
			this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth  = 0;
			this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().livePlayer = false;
			//Time.timeScale = 0;
			//_menuDie.menuActive();
		}
	}
	
	private void SkillInstance()
	{
		GameObject obj = Instantiate(SkillPrefab);
		obj.transform.position = startSkillPosition.position;
		obj.GetComponent<moveTarget>().target = Player;
	}
	
	public void AttackTimeMage()
	{
		if(Health > 0)
		{
			calculateDmgDefence();
			anim.SetTrigger("Punch");
			this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth -= calculateDmg;
			SkillInstance();
			audioMage.Play();
			
			
			this.gameObject.GetComponent<EnemyMotion>().damage = calculateDmg;
		}
		
		else
		{
			Health = 0;
		}
		
		if(this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth <= 0)
		{
			this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerHealth  = 0;
			this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().livePlayer = false;
			
			//Time.timeScale = 0;
			//_menuDie.menuActive();
		}
	}
	
	public void TakeExp()
	{
		char c = '+';
		float exp = Exp * this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().RateExp;
		this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerExpValue += exp;
		this.GetComponent<EnemyMotion>().Player.GetComponent<PlayerAttributes>().PlayerGold += Gold;
		//this.gameObject.SetActive(false);
		_gameHelper.countSpawn--;
		_popupText.instancePopupText(Player.transform.position, "Exp: ", (int)exp, _expColor, c);
		//SelfQuest();
	}
	
	/*public void SelfQuest()
		{
		for(int i = 0; i < _activeQuests.Quests.Length; i++)
		{
		if(_activeQuests.Quests[i] != null)
		{
		_activeQuests.Quests[i].GetComponent<Quest>().enemyKills++;
		if(_activeQuests.Quests[i].GetComponent<Quest>().enemyKills == _activeQuests.Quests[i].GetComponent<Quest>().maxEnemyKills)
		{
		break;
		}
		}
		
		}
	}*/
	
	private void Die()
	{
		//Destroy(this.gameObject);
		anim.SetTrigger("Death");
		for(int i = 0; i < _historyGame.activeQuests.Length; i++)
		{
			if(_historyGame.activeQuests[i].GetComponent<Quest>())
			{
				if(_historyGame.activeQuests[i].GetComponent<Quest>().IdQuestKill == Id)
				{
					_historyGame.activeQuests[i].GetComponent<Quest>().killEnemy++;
				}
			}
		}
		TakeExp();
		Drop();
	}
}
