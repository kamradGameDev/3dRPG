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
		Invoke("timeStartChange", 0.2f);
		canvas = GameObject.Find("Canvas");
		WarningText = GameObject.Find("WarningObject");
	}
	
	private void timeStartChange()
	{
		startSkillPosition = SpawnCharacterPlayer.instance.player.transform.GetChild(0);
		anim = SpawnCharacterPlayer.instance.player.GetComponent<Animator>();
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
		Vector3 position = SpawnCharacterPlayer.instance.player.transform.position;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in Enemys)
		{
			Vector3 diff = go.transform.position - SpawnCharacterPlayer.instance.player.transform.position;
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
					if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana >= ManaSkill)
					{
						if(Enemy.GetComponent<EnemyAttributes>().Health > 0)
						{
							audio.Play();
							MotionAndroid.instance.moveSpeed = 0f;
							Invoke("changeSpeedPlayer", 0.5f);
							SpawnCharacterPlayer.instance.player.transform.LookAt(Enemy.transform);
							GameObject obj = Instantiate(skillPrefab);
							obj.transform.position = startSkillPosition.position;
							obj.GetComponent<moveTarget>().target = Enemy;
							
							if(Random.Range(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMin, SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMax) < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeStat)
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
							SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana -= ManaSkill;
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
