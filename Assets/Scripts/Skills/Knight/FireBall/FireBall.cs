using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour 
{
	private Animator anim;
	public GameObject[] Enemys;
	public GameObject Enemy = null;
	public AudioSource audio;
	
	private GameObject Player;
	public GameObject WarningText;
	
	public GameObject skillPrefab;
	private Transform startSkillPosition;
	
	public float ManaSkill = 25.0f;
	
	public Color _redColor;
	public Color _greenColor;
	
	public float dist = 10.0f;
	public float waitTime = 2.0f;
	public float damageSkill = 25.0f;
	
	public bool startSkill = false;
	public GameObject canvas;
	public Image imgSkill;
	private float distance;
	
	public float[] diff; 
	
	private void Start()
	{
		Player = GameObject.FindWithTag("Player");
		startSkillPosition = Player.transform.GetChild(0);
		anim = Player.GetComponent<Animator>();
		canvas = GameObject.Find("Canvas");
		WarningText = GameObject.Find("WarningObject");
	}
	
	private void Update()
	{
		if(startSkill)
		{
			imgSkill.fillAmount += 0.5f / waitTime * Time.deltaTime;
			if(imgSkill.fillAmount == 1.0f)
			{
				startSkill = false;
			}
		}
	}
	
	private void findEnemy()
	{
		distance = Mathf.Infinity;
		Vector3 position = Player.transform.position;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in Enemys)
		{
			Vector3 diff = go.transform.position - Player.transform.position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance)
			{
				Enemy = go;
				distance = curDistance;
				}
		}
	}
	
	private void changeSpeedPlayer()
	{
		MotionAndroid.instance.moveSpeed = 12f;
	}
	
	public void StartSkill()
	{
		if(!startSkill)
		{
			findEnemy();
			if(Enemy != null)
			{
				if(distance <= dist)
				{
					if(Player.GetComponent<PlayerAttributes>().PlayerMana >= ManaSkill)
					{
						if(Enemy.GetComponent<EnemyAttributes>().liveEnemy)
						{
							audio.Play();
							MotionAndroid.instance.moveSpeed = 0f;
							Invoke("changeSpeedPlayer", 0.5f);
							Player.transform.LookAt(Enemy.transform);
							GameObject obj = Instantiate(skillPrefab);
							obj.transform.position = startSkillPosition.position;
							obj.GetComponent<moveTarget>().target = Enemy;
							
							if(Random.Range(Player.GetComponent<PlayerAttributes>().critChangeMin, Player.GetComponent<PlayerAttributes>().critChangeMax) < Player.GetComponent<PlayerAttributes>().critChangeStat)
							{
								obj.GetComponent<moveTarget>().damage = damageSkill * 2.0f;
								obj.GetComponent<moveTarget>()._color = _redColor;
								obj.GetComponent<moveTarget>().crit = true;
							}
							else
							{
								obj.GetComponent<moveTarget>().damage = damageSkill;
								obj.GetComponent<moveTarget>()._color = _greenColor;
								obj.GetComponent<moveTarget>().crit = false;
							}
							startSkill = true;
							imgSkill.fillAmount = 0f;
							Player.GetComponent<PlayerAttributes>().PlayerMana -= ManaSkill;
							anim.SetTrigger("Attack");
							Enemy = null;
						}
					}
				}
			}
			else
			{
				WarningText.transform.SetParent(canvas.transform);
				WarningText.transform.GetChild(0).gameObject.SetActive(true);
				WarningText.transform.GetChild(0).GetComponent<Text>().text = "Low Mana";
				Invoke("passiveWarningTextText", 1.0f);
			}
		}
	}	
	
	private void passiveWarningTextText()
	{
		WarningText.transform.GetChild(0).gameObject.SetActive(false);
	}
}
