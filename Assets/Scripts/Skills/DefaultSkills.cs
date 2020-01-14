using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultSkills : MonoBehaviour 
{
	public GameObject[] Enemys;
	public GameObject explosion;
	public GameObject Enemy = null;
	public GameObject[] defaultSkillsPrefab;
	private GameObject objSkill;
	public Transform StartSkill;
	public Text ManaInt;
	public Text HealthInt;
	
	public Text timeSkillText;
	
	[Header("Skills")]
	public Image LowAttackImg;
	public Image HealerImg;
	public Image ManaImg;
	
	public Image theDelayBeforeImg;
	public bool IsCoolingDownLowAttack = false;
	public bool IsCoolingDownHealer = false;
	public bool IsCoolingDownMana= false;
	public float waitTime = 2.0f;
	public float theDelayBeforeTheAttack = 2.0f;
	public float timeSkillInt;
	public enum statusProcessSkill
	{
		noStart, ProcessStart
	}
	public statusProcessSkill _statusProcessSkill;
	
	public new AudioSource[] audio;
	
	private Animator anim;
	
	Inventory _inventory;
	PopupText _popupText;
	
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
		Invoke("changeSpeedCharacter", 0.2f);
	}
	
	private void changeTimeStart()
	{
		StartSkill = GameObject.Find("SkillStartPosition").transform;
		anim = SpawnCharacterPlayer.instance.player.GetComponent<Animator>();
		damageSkill = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage;
		changeClassPlayerToImageDefAttack();
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
		{
			ManaImg.GetComponent<Image>().color = Color.yellow;
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
		{
			ManaImg.GetComponent<Image>().color = Color.green;
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
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
				
			    if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
				{
					if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 1)
					{
						ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();	
					}
				}
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
				{
					if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 35)
					{
						ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();	
					}
				}
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
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
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
			{
				waitTime = 2.0f;
				LowAttackImg.fillAmount += SkillRollback / waitTime * Time.deltaTime;
			    if(LowAttackImg.fillAmount == 1.0f)
			    {
				    IsCoolingDownLowAttack = false;
				}
			}
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
			{
				waitTime = 4f;
				LowAttackImg.fillAmount += SkillRollback / waitTime * Time.deltaTime;
				if(LowAttackImg.fillAmount == 1.0f)
				{
					IsCoolingDownLowAttack = false;
				}
			}
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
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
		
		if(_statusProcessSkill == statusProcessSkill.ProcessStart)
		{
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._distOrMelleCharacter == distOrMelleCharacter.DistanceCharacter)
			{
				theDelayBeforeImg.fillAmount += 1.0f / theDelayBeforeTheAttack * Time.deltaTime;
			}

			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._distOrMelleCharacter == distOrMelleCharacter.MelleCharacter)
			{
				theDelayBeforeImg.fillAmount += 5.0f / theDelayBeforeTheAttack * Time.deltaTime;
			}

			if(timeSkillInt < 100)
			{
				timeSkillText.text = ((timeSkillInt  += 1.0f /theDelayBeforeTheAttack) * 100 * Time.deltaTime).ToString() + "%";
				anim.Play("StandingAttack");
			}
			if(theDelayBeforeImg.fillAmount  == 1.0f)
			{
				timeSkill();
				
				timeSkillInt = 0f;
				timeSkillText.text = timeSkillInt.ToString() + "%";
				_statusProcessSkill = statusProcessSkill.noStart;
				theDelayBeforeImg.fillAmount = 0f;
			}
		}
	}
	
	private void changeClassPlayerToImageDefAttack()
	{
	    if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
		{
			LowAttackImg.sprite = lowAttackImg[0];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[0];
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
		{
			LowAttackImg.sprite = lowAttackImg[1];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[1];
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
		{
			LowAttackImg.sprite = lowAttackImg[2];
			LowAttackImg.transform.parent.gameObject.GetComponent<Image>().sprite = lowAttackImg[2];
		}
	}
	
	private void timeSkill()
	{
		//dist = 20.0f;
		findEnemy();
		if(!IsCoolingDownLowAttack)
		{
			if(Enemy)
			{
				Invoke("changeSpeedCharacter", 0.5f);
				anim.SetTrigger("Attack");
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
				{
					objSkill = Instantiate(defaultSkillsPrefab[2], new Vector3(0,0,0), Quaternion.identity);
					objSkill.transform.position = StartSkill.position;
				    objSkill.GetComponent<moveTarget>().target = Enemy;
					audio[1].Play();
				}
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
				{
					objSkill = Instantiate(defaultSkillsPrefab[3], new Vector3(0,0,0), Quaternion.identity);
					audio[2].Play();
					objSkill.transform.position = StartSkill.position;
				    objSkill.GetComponent<moveTarget>().target = Enemy;
				}

				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._distOrMelleCharacter == distOrMelleCharacter.DistanceCharacter)
				{
			        if(Random.Range(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMin, SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMax) < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeStat)
				    {
					    objSkill.GetComponent<moveTarget>().damage = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage  * 2;
					    objSkill.GetComponent<moveTarget>()._color = _redColor;
					    objSkill.GetComponent<moveTarget>().crit = true;
				   }
			       else
				   {
					    objSkill.GetComponent<moveTarget>().damage = damageSkill;
				     	objSkill.GetComponent<moveTarget>()._color = _greenColor;
					    objSkill.GetComponent<moveTarget>().crit = false;
				   }
				}
				
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
				{
					float damage;
					char c = '-';
					if(Random.Range(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMin, SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMax) < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeStat)
				    {
					    Enemy.GetComponent<EnemyAttributes>().Health -= SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage * 2;
						damage = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage * 2;
						_popupText.instancePopupText(Enemy.transform.position, "Crit: ", (int)damage, _redColor, c);
				   }
			       else
				   {
					    Enemy.GetComponent<EnemyAttributes>().Health -= SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage;
						damage = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage * 2;
						_popupText.instancePopupText(Enemy.transform.position, "DMG: ", (int)damage, _greenColor, c);
				   }
				   audio[0].Play();
				   Invoke("instanceBlood", 0.5f);
				}
				
				Enemy.GetComponent<EnemyMotion>().playerAttackEnemy = true;
				
				IsCoolingDownLowAttack = true;
				LowAttackImg.fillAmount = 0f;
			}		
		}
	}

	private void instanceBlood()
	{
		GameObject obj = Instantiate(explosion);
		obj.transform.position = Enemy.transform.position;
		Destroy(obj, 0.5f);
		Enemy = null;
	}
	
	private void findEnemy()
	{
		distance = 500;
		Vector3 position = SpawnCharacterPlayer.instance.player.transform.position;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in Enemys)
		{
			Vector3 diff = go.transform.position - SpawnCharacterPlayer.instance.player.transform.position;
			float curDistance = diff.sqrMagnitude;
			
			if(curDistance < distance)
			{
				if(go.GetComponent<EnemyAttributes>().Health > 0)
				{
					Enemy = go;
					distance = curDistance;
					MotionAndroid.instance.moveSpeed = 0f;
					SpawnCharacterPlayer.instance.player.transform.LookAt(Enemy.transform);
					_statusProcessSkill = statusProcessSkill.ProcessStart;
				}
			}
		}
	}
	
	
	public void LowAttack()
	{
		if(_statusProcessSkill == statusProcessSkill.noStart)
		{
			findEnemy();
		}
	}
	
	private void changeSpeedCharacter()
	{
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
		{
			MotionAndroid.instance.moveSpeed = 8f;
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
		{
			MotionAndroid.instance.moveSpeed = 12f;
		}
		
		if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
		{
			MotionAndroid.instance.moveSpeed = 10f;
		}
	}
	
	/*public void AttackWarrior()
		{
		dist = 4.0f;
		Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < Enemys.Length; i++)
		{
		float _dist = Vector3.Distance(SpawnCharacterPlayer.instance.player.transform.position, Enemys[i].transform.position);
		if(_dist < dist)
		{
		MotionAndroid.instance.moveSpeed = 0f;
		Invoke("changeSpeedCharacter", 0.5f);
		if(Enemys[i].GetComponent<EnemyAttributes>().Health > 0)
		{
		Enemy = Enemys[i];
		SpawnCharacterPlayer.instance.player.transform.LookAt(Enemy.transform);
		if(!IsCoolingDownLowAttack)
		{
		anim.SetTrigger("Attack");
		audio[0].Play();			
		char c = '-';
		
		Enemy.GetComponent<EnemyMotion>().playerAttackEnemy = true;
		SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critDamage = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage * SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critMultiple;
		if(Random.Range(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMin, SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeMax) < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critChangeStat)
		{
		Enemy.GetComponent<EnemyAttributes>().Health -= SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critDamage;
		_popupText.instancePopupText(Enemy.transform.position, "Crit: ", SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().critDamage, _redColor, c);
		}
		else
		{
		Enemy.GetComponent<EnemyAttributes>().Health -= damageSkill;
		
		_popupText.instancePopupText(Enemy.transform.position, "DMG: ", SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerDamage, _greenColor, c);
		}
		
		LowAttackImg.fillAmount = 0f;
		IsCoolingDownLowAttack = true;
		Enemy = null;
		}
		}
		}
		}
	}*/
	
	public void PlayerHealer()
	{
		if(IsCoolingDownHealer == false)
		{
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerHealth < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerHealth)
			{
				for(int i = 0; i < _inventory.content.Length; i++)
				{
					if(_inventory.content[i].transform.childCount > 0)
					{
						if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 0 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0 )
						{
							_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--;
							HealthInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
							float healthRegen = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerHealth / 100;
							SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerHealth += healthRegen;
							HealerImg.fillAmount = 0f;
							IsCoolingDownHealer = true;
							
							GameObject obj = Instantiate(defaultSkillsPrefab[0]) as GameObject;
							char c = '+';
							_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "HP: ", (int)healthRegen, _greenColor, c);
							obj.transform.position = SpawnCharacterPlayer.instance.player.transform.position + new Vector3(0f, 4.0f, 0);
							obj.transform.SetParent(SpawnCharacterPlayer.instance.player.transform);
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
				_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "Health is full", 100, _redColor, c);
				Debug.Log("Player " + SpawnCharacterPlayer.instance.player.transform.position);
			}
		}
	}
	
	public void PlayerManaPlus()
	{
		if(IsCoolingDownMana == false)
		{
			if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana < SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerMana)
			{
				for(int i = 0; i < _inventory.content.Length; i++)
				{
					if(_inventory.content[i].transform.childCount > 0)
					{
						if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 35 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = SpawnCharacterPlayer.instance.player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(SpawnCharacterPlayer.instance.player.transform);
								audio[3].Play();
								Destroy(obj, 3.0f);
							}
						}
						if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 36 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = SpawnCharacterPlayer.instance.player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(SpawnCharacterPlayer.instance.player.transform);
								audio[3].Play();
								Destroy(obj, 3.0f);
							}
						}
						
						if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
						{
							if(_inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id == 1 && _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem > 0)
							{
								_inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem--; 
								ManaInt.text = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem.ToString();
								float regenMp = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().PlayerRegenHealthOrMana * SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerMana / 100;
								SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerMana += regenMp;
								ManaImg.fillAmount = 0f;
								IsCoolingDownMana = true;
								
								GameObject obj = Instantiate(defaultSkillsPrefab[1]) as GameObject;
								char c = '+';
								_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "MP: ", (int)regenMp, _greenColor, c);
								obj.transform.position = SpawnCharacterPlayer.instance.player.transform.position + new Vector3(0f, 4.0f, 0);
								obj.transform.SetParent(SpawnCharacterPlayer.instance.player.transform);
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
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Warrior)
				{
					_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "Warrior Mana is full", 100, _redColor, c);
				}
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Archer)
				{
					_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "Archer Mana is full", 100, _redColor, c);
				}
				if(SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>()._classCharacter == classCharacter.Mage)
				{
					_popupText.instancePopupText(SpawnCharacterPlayer.instance.player.transform.position, "Mana is full", 100, _redColor, c);
				}
				
			} 
		}
	}	
}
