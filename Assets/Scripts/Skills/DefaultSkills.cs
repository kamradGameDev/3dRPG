using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultSkills : MonoBehaviour 
{
	public GameObject[] Enemys;
	public GameObject Enemy = null;
	public GameObject[] defaultSkillsPrefab;
	public Transform StartSkill;
	public Text ManaInt;
	public Text HealthInt;
	
	[Header("Skills")]
	public Image LowAttackImg;
	public Image HealerImg;
	public Image ManaImg;
	public bool IsCoolingDownLowAttack = false;
	public bool IsCoolingDownHealer = false;
	public bool IsCoolingDownMana= false;
	public float waitTime = 2.0f;
	
	public AudioSource[] audio;
	
	private Animator anim;
	
	public GameObject player; 
	Inventory _inventory;
	PopupText _popupText;
	
	public string classCharacter;
	
	public float dist = 2.0f;
	private float distance;
	public float SkillRollback = 1.0f;
	private int damageSkill;
	
	public Color _redColor;
	public Color _greenColor;
	public Sprite[] lowAttackImg;
	
	void Start()
	{
		Invoke("changeTimeStart", 0.2f);
		_inventory = GameObject.FindObjectOfType<Inventory>();
		_popupText = GameObject.FindObjectOfType<PopupText>();
	}
	
	private void changeTimeStart()
	{
	    player = GameObject.FindWithTag("Player");
		StartSkill = player.transform.GetChild(0);
		anim = player.GetComponent<Animator>();
		classCharacter = player.GetComponent<PlayerAttributes>()._classCharacter.ToString();
		damageSkill = player.GetComponent<PlayerAttributes>().PlayerDamage;
		Debug.Log(damageSkill);
		changeClassPlayerToImageDefAttack();
		
		if(classCharacter == "Warrior")
		{
			ManaImg.GetComponent<Image>().color = Color.yellow;
		}
		
		if(classCharacter == "Archer")
		{
			ManaImg.GetComponent<Image>().color = Color.green;
		}
		
		if(classCharacter == "Mage")
		{
			ManaImg.GetComponent<Image>().color = Color.blue;
		}
	}
	
	private void Update()
	{
		for(int i = 0; i < _inventory.content.Length; i++)
		{
			if(_inventory.content[i].transform.childCount > 0)
			{
				if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 0)
				{
					HealthInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
				}
				
				if(classCharacter == "Mage")
				{
					if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 1)
					{
						ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();	
					}
				}
				if(classCharacter == "Warrior")
				{
					if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 35)
					{
						ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();	
					}
				}
				if(classCharacter == "Archer")
				{
					if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 36)
					{
						ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();	
					}
				}
			}
		}
		if(IsCoolingDownLowAttack)
		{
			if(classCharacter == "Warrior")
			{
				waitTime = 2.0f;
				LowAttackImg.fillAmount += SkillRollback / waitTime * Time.deltaTime;
			    if(LowAttackImg.fillAmount == 1.0f)
			    {
				    IsCoolingDownLowAttack = false;
				}
			}
			else if (classCharacter == "Mage")
			{
				waitTime = 4f;
				LowAttackImg.fillAmount += SkillRollback / waitTime * Time.deltaTime;
				if(LowAttackImg.fillAmount == 1.0f)
				{
					IsCoolingDownLowAttack = false;
				}
			}
			else if(classCharacter == "Archer")
			{
				waitTime = 3.5f;
				LowAttackImg.fillAmount += SkillRollback / waitTime * Time.deltaTime;
				if(LowAttackImg.fillAmount == 1.0f)
				{
					IsCoolingDownLowAttack = false;
				}
			}
		}
		
		if(IsCoolingDownHealer)
		{
			HealerImg.fillAmount += 0.5f / waitTime * Time.deltaTime;
			if(HealerImg.fillAmount == 1.0f)
			{
				IsCoolingDownHealer = false;
			}
		}
		
		if(IsCoolingDownMana)
		{
			ManaImg.fillAmount += 0.5f / waitTime * Time.deltaTime;
			if(ManaImg.fillAmount == 1.0f)
			{
				IsCoolingDownMana = false;
			}
		}
	}
	
	private void changeClassPlayerToImageDefAttack()
	{
	    if( classCharacter == "Warrior")
		{
			LowAttackImg.sprite = lowAttackImg[0];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[0];
		}
		
		else if(classCharacter == "Mage")
		{
			LowAttackImg.sprite = lowAttackImg[1];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[1];
		}
		
		else if(classCharacter == "Archer")
		{
			LowAttackImg.sprite = lowAttackImg[2];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[2];
		}
	}
	
	private void findEnemy()
	{
		distance = Mathf.Infinity;
		Vector3 position = player.transform.position;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in Enemys)
		{
			Vector3 diff = go.transform.position - player.transform.position;
			float curDistance = diff.sqrMagnitude;
			
			if(curDistance < distance)
			{
				Enemy = go;
				distance = curDistance;
			}
		}
	}
	
	public void AttackMage()
	{
		dist = 500.0f;
		findEnemy();
		if(distance < dist)
		{
			if(!IsCoolingDownLowAttack)
			{
				
				if(Enemy)
				{
					if(Enemy.GetComponent<EnemyAttributes>().liveEnemy)
					{
						MotionAndroid.instance.moveSpeed = 0f;
						Invoke("changeSpeedPlayer", 0.5f);
						player.transform.LookAt(Enemy.transform);
						GameObject obj = Instantiate(defaultSkillsPrefab[2]);
						obj.transform.position = StartSkill.position;
						obj.GetComponent<moveTarget>().target = Enemy;
						audio[1].Play();
						
						Enemy.GetComponent<EnemyMotion>().playerAttackEnemy = true;
						if(Random.Range(player.GetComponent<PlayerAttributes>().critChangeMin, player.GetComponent<PlayerAttributes>().critChangeMax) < player.GetComponent<PlayerAttributes>().critChangeStat)
						{
							obj.GetComponent<moveTarget>().damage = player.GetComponent<PlayerAttributes>().critDamage;
							obj.GetComponent<moveTarget>()._color = _redColor;
							obj.GetComponent<moveTarget>().crit = true;
						}
						else
						{
							obj.GetComponent<moveTarget>().damage = damageSkill;
							obj.GetComponent<moveTarget>()._color = _greenColor;
							obj.GetComponent<moveTarget>().crit = false;
						}
						IsCoolingDownLowAttack = true;
						LowAttackImg.fillAmount = 0f;
						anim.SetTrigger("Attack");
						Enemy = null;
					}
					
				}
			}
		}
	}			
	
	
	private void changeSpeedPlayer()
	{
		MotionAndroid.instance.moveSpeed = 12f;
	}
	
	public void AttackArcher()
	{
		dist = 500f;
		if(!IsCoolingDownLowAttack)
		{
			findEnemy();
			if(distance < dist)
			{
				if(Enemy.GetComponent<EnemyAttributes>().liveEnemy)
				{
					player.transform.LookAt(Enemy.transform);
					GameObject obj = Instantiate(defaultSkillsPrefab[3]);
					obj.transform.position = StartSkill.position;
					obj.GetComponent<moveTarget>().target = Enemy;
					audio[2].Play();
					MotionAndroid.instance.moveSpeed = 0f;
					Invoke("changeSpeedPlayer", 0.5f);
					
					Enemy.GetComponent<EnemyMotion>().playerAttackEnemy = true;
					player.GetComponent<PlayerAttributes>().critDamage = player.GetComponent<PlayerAttributes>().PlayerDamage * player.GetComponent<PlayerAttributes>().critMultiple;
					if(Random.Range(player.GetComponent<PlayerAttributes>().critChangeMin, player.GetComponent<PlayerAttributes>().critChangeMax) < player.GetComponent<PlayerAttributes>().critChangeStat)
					{
						obj.GetComponent<moveTarget>().damage = player.GetComponent<PlayerAttributes>().critDamage;
						obj.GetComponent<moveTarget>()._color = _redColor;
						obj.GetComponent<moveTarget>().crit = true;
						//Debug.Log("crit" + damageSkill * 2.0f);
					}
					else
					{
						obj.GetComponent<moveTarget>().damage = damageSkill;
						obj.GetComponent<moveTarget>()._color = _greenColor;
						obj.GetComponent<moveTarget>().crit = false;
						Debug.Log("PlayerDamage " + player.GetComponent<PlayerAttributes>().PlayerDamage);
						//Debug.Log("DMG" + damageSkills);
					}
					IsCoolingDownLowAttack = true;
					LowAttackImg.fillAmount = 0f;
					anim.SetTrigger("Attack");
					Enemy = null;
				}
			}
		}
	}
	
	public void AttackWarrior()
	{
		dist = 4.0f;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < Enemys.Length; i++)
		{
			float _dist = Vector3.Distance(player.transform.position, Enemys[i].transform.position);
			if(_dist < dist)
			{
				if(Enemy.GetComponent<EnemyAttributes>().liveEnemy)
				{
					MotionAndroid.instance.moveSpeed = 0f;
					Invoke("changeSpeedPlayer", 0.5f);
					Enemy = Enemys[i];
					player.transform.LookAt(Enemy.transform);
					if(!IsCoolingDownLowAttack)
					{
						anim.SetTrigger("Attack");
						audio[0].Play();			
						char c = '-';
						
						Enemy.GetComponent<EnemyMotion>().playerAttackEnemy = true;
						player.GetComponent<PlayerAttributes>().critDamage = player.GetComponent<PlayerAttributes>().PlayerDamage * player.GetComponent<PlayerAttributes>().critMultiple;
						if(Random.Range(player.GetComponent<PlayerAttributes>().critChangeMin, player.GetComponent<PlayerAttributes>().critChangeMax) < player.GetComponent<PlayerAttributes>().critChangeStat)
						{
							Enemy.GetComponent<EnemyAttributes>().Health -= player.GetComponent<PlayerAttributes>().critDamage;
							_popupText.instancePopupText(Enemy.transform.position, "Crit: ", player.GetComponent<PlayerAttributes>().critDamage, _redColor, c);
						}
						else
						{
							Enemy.GetComponent<EnemyAttributes>().Health -= damageSkill;
							
							_popupText.instancePopupText(Enemy.transform.position, "DMG: ", player.GetComponent<PlayerAttributes>().PlayerDamage, _greenColor, c);
						}
						
						LowAttackImg.fillAmount = 0f;
						IsCoolingDownLowAttack = true;
						Enemy = null;
					}
				}
			}
		}
	}
	
	public void LowAttack()
	{
		if( classCharacter == "Warrior")
		{
			AttackWarrior();
		}
		
		else if(classCharacter == "Mage")
		{
			AttackMage();
		}
		
		else if(classCharacter == "Archer")
		{
			AttackArcher();
		}
	}
	
	public void PlayerHealer()
	{
		if(IsCoolingDownHealer == false)
		{
			if(player.GetComponent<PlayerAttributes>().PlayerHealth < player.GetComponent<PlayerAttributes>().MaxPlayerHealth)
			{
				for(int i = 0; i < _inventory.content.Length; i++)
				{
					if(_inventory.content[i].transform.childCount > 0)
					{
						if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 0 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0 )
						{
							_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--;
							HealthInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
							float healthRegen = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * player.GetComponent<PlayerAttributes>().MaxPlayerHealth / 100;
							player.GetComponent<PlayerAttributes>().PlayerHealth += healthRegen;
							HealerImg.fillAmount = 0f;
							IsCoolingDownHealer = true;
							
							GameObject obj = Instantiate(defaultSkillsPrefab[0]) as GameObject;
							char c = '+';
							_popupText.instancePopupText(player.transform.position, "HP: ", (int)healthRegen, _greenColor, c);
							obj.transform.position = player.transform.position + new Vector3(0f, 4.0f, 0);
							obj.transform.SetParent(player.transform);
							audio[3].Play();
							Destroy(obj, 3.0f);
						}
						/*else
							{
							char c = ':';
							if(player)
							{
							_popupText.instancePopupText(player.transform.position, "Bank HP is Inven", 0, _redColor, c);
							}
						}*/
					}
					/*else
						{
						char c = ':';
						if(player)
						{
						_popupText.instancePopupText(player.transform.position, "No is bank HP in inventory", 0, _redColor, c);
						}
					}*/
					//break;
				}	 
			}
			else
			{
				char c = ' ';
				_popupText.instancePopupText(player.transform.position, "Health is full", 100, _redColor, c);
				Debug.Log("Player " + player.transform.position);
			}
		}
	}
	
	public void PlayerManaPlus()
	{
		if(IsCoolingDownMana == false)
		{
			if(player.GetComponent<PlayerAttributes>().PlayerMana < player.GetComponent<PlayerAttributes>().MaxPlayerMana)
			{
				for(int i = 0; i < _inventory.content.Length; i++)
				{
					if(_inventory.content[i].transform.childCount > 0)
					{
						if(classCharacter == "Warrior")
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 35 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(player.transform);
								audio[3].Play();
								Destroy(obj, 3.0f);
							}
						}
						else if(classCharacter == "Archer")
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 36 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(player.transform);
								audio[3].Play();
								Destroy(obj, 3.0f);
							}
						}
						
						else if(classCharacter == "Mage")
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 1 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(player.transform);
								audio[3].Play();
								Destroy(obj, 3.0f);
							}
						}
						
						/*else 
							{
							char c = ':';
							if(player)
							{
							_popupText.instancePopupText(player.transform.position, "No is bank MP in inventory	", 0, _redColor, c);
							}
						}*/
					}
					/*else
						{
						char c = ':';
						if(player)
						{
						_popupText.instancePopupText(player.transform.position, "Bank MP is Inven", 0, _redColor, c);
						}
					}*/
					//break;
				}	 
			}
			else
			{
				
				char c = ' ';
				if(classCharacter == "Warrior")
				{
					_popupText.instancePopupText(player.transform.position, "Warrior Mana is full", 100, _redColor, c);
				}
				else if(classCharacter == "Archer")
				{
					_popupText.instancePopupText(player.transform.position, "Archer Mana is full", 100, _redColor, c);
				}
				else if(classCharacter == "Mage")
				{
					_popupText.instancePopupText(player.transform.position, "Mana is full", 100, _redColor, c);
				}
				
			} 
		}
	}	
}
