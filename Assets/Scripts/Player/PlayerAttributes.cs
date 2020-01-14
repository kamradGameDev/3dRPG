using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum distOrMelleCharacter
{
	DistanceCharacter, MelleCharacter
}
public enum classCharacter
{
	Warrior, Mage, Archer
}

public enum Warrior
{
	NoClass, Nothing, Knight, HightKnight, HolyKnight
}

public enum Archer
{
	NoClass, Nothing, Hawkeye, Sagittarius, SupremeArcher
}

public enum Mage
{
	NoClass, Nothing, Wizard, Sorcerer, Archimage
}
public class PlayerAttributes : MonoBehaviour 
{
	public classCharacter _classCharacter;

	public distOrMelleCharacter _distOrMelleCharacter;
	public Warrior _warrior;
	public Mage _mage;
	public Archer _archer;
	public GameObject LevelUpObj;
	public bool livePlayer = true;
	
	public float RateExp = 5.0f;
	
	[Header("Основные показатели игрока.")]
	public int PlayerDamage = 10;
	public int PlayerDefence = 3;
	public float TimeAttackLow = 2.0f;
	
	public GameObject levelUpText;
	
	[Header("Статы игрока.")]
	public int PlayerStrength = 10;
	public int PlayerDexterity = 7;
	public int PlayerStamina = 12;
	public int PlayerManaInt = 3;
	
	[Header("Золото игрока.")]
	public int PlayerGold = 500;
	
	[Header("Стамина игрока.")]
	public float PlayerHealth = 100;
	public float MaxPlayerHealth = 100;
	
	[Header("Мана игрока.")]
	public float PlayerMana = 100;
	public float MaxPlayerMana = 100;
	
	[Header("Уровень игрока.")]
	public int PlayerLevel = 1;
	public int PlayerMaxLevel = 40;
	
	public float waitTimeHpRegen = 5.0f;
	public float waitTimeMpRegen = 5.0f;
	
	public float RegenHPValue = 1.0f;
	public float RegenMPValue = 0.3f;
	
	private Image Health;
	private Image Mana;
	
	public float PlayerExpValue = 17;
	public float PlayerExpMaxValue = 100;
	
	public int critMultiple = 2;
	public int critDamage;
	public float critChangeMin = 0f;
	public float critChangeMax = 1f;
	public float critChangeStat = 0.1f;
	
	private AudioSource audioLevelUp;
	
	private Text PlayerExpText;
	
	private Text PlayerHealthText;
	private Text PlayerManaText;
	
	private Text PlayerLevelText;
	
	private GameObject PlayerDamageText,PlayerDefenceText,PlayerRegenHPText,PlayerRegenMPText,ClassCharacterText,ProfessionCharacterText,PlayerStrengthText,PlayerDexterityText,PlayerStaminaText,
	PlayerManaIntText,PlayerHealthTextObj,PlayerManaTextObj;
	
	public bool riceAgain = false;
	
	private Slider PlayerExpSlider;
	
	MenuDie _menuDie;
	
	private void Start()
	{	
		PlayerExpText = GameObject.FindWithTag("PlayerExpText").GetComponent<Text>();
		
		PlayerHealthText = GameObject.FindWithTag("PlayerHealthText").GetComponent<Text>();
		PlayerManaText = GameObject.FindWithTag("PlayerManaText").GetComponent<Text>();
		
		PlayerLevelText = GameObject.FindWithTag("PlayerLevelText").GetComponent<Text>();
		
		//PlayerDamage += PlayerStrength;
		//PlayerDefence += PlayerDexterity;
		PlayerExpSlider = GameObject.FindWithTag("PlayerExpSlider").GetComponent<Slider>(); 
		PlayerStrengthText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(0).GetChild(0).gameObject;
		PlayerDexterityText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(1).GetChild(0).gameObject;
		PlayerStaminaText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(2).GetChild(0).gameObject;
		PlayerManaIntText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(3).GetChild(0).gameObject;
		PlayerHealthTextObj = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(4).GetChild(0).gameObject;
		PlayerManaTextObj = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(5).GetChild(0).gameObject;
		Health = GameObject.FindWithTag("PlayerHealth").GetComponent<Image>();
		Mana = GameObject.FindWithTag("PlayerMana").GetComponent<Image>();
		audioLevelUp = GameObject.Find("Audios").transform.GetChild(3).GetComponent<AudioSource>();
		
		PlayerDamageText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(6).GetChild(0).gameObject;
		PlayerDefenceText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(7).GetChild(0).gameObject;
		PlayerRegenHPText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(8).GetChild(0).gameObject;
		PlayerRegenMPText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(9).GetChild(0).gameObject;
		ClassCharacterText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(10).GetChild(0).gameObject;
		ProfessionCharacterText = FindUIStatic.instance.PlayerAttributesPanel.transform.GetChild(11).GetChild(0).gameObject;
		ClassCharacterText.GetComponent<Text>().text = "Character class: " + _classCharacter.ToString();
		levelUpText = GameObject.Find("WarningObject");
		//LevelUpObj = this.gameObject.transform.GetChild(7).gameObject;
		StartCoroutine("RegenHP");
		StartCoroutine("RegenMP");
		_menuDie = GameObject.FindObjectOfType<MenuDie>();
		
		if(_classCharacter == classCharacter.Mage)
		{
		    Mana.GetComponent<Image>().color = Color.white;
		}
		else if(_classCharacter == classCharacter.Archer)
		{
		    Mana.GetComponent<Image>().color = Color.white;
		}
		else if(_classCharacter == classCharacter.Warrior)
		{
		    Mana.GetComponent<Image>().color = Color.white;
		}
	}
	
	private void UpdateStarBar()
	{
		if(FindUIStatic.instance.PlayerAttributesPanel)
		{
			Health.fillAmount = PlayerHealth / MaxPlayerHealth;
			
			Mana.fillAmount = PlayerMana / MaxPlayerMana;
			
			if(PlayerHealth > MaxPlayerHealth)
			{
				PlayerHealth = MaxPlayerHealth;
			}
			if(PlayerMana > MaxPlayerMana)
			{
				PlayerMana = MaxPlayerMana;
			}
		}
	}
	
	private void Update()
	{
		UpdateStarBar();
		
		PlayerExpSlider.value = PlayerExpValue;
		PlayerExpSlider.maxValue = PlayerExpMaxValue;
		levelUp();
		float exp = Mathf.Round((PlayerExpValue * 100)  / PlayerExpMaxValue);
		PlayerExpText.text = string.Format("{0:0.00}", exp) + "%";
		
		
		PlayerHealthText.text = Mathf.Round((PlayerHealth * 100) / MaxPlayerHealth) + " % ";
		PlayerManaText.text = Mathf.Round((PlayerMana * 100) / MaxPlayerMana) + " % ";
		
		PlayerLevelText.text = "Level: " + PlayerLevel.ToString();
		
		PlayerStrengthText.GetComponent<Text>().text = "Strength: " + PlayerStrength.ToString();
		PlayerDexterityText.GetComponent<Text>().text = "Dexterity: " + PlayerDexterity.ToString();
		PlayerStaminaText.GetComponent<Text>().text = "Stamina: " + PlayerStamina.ToString();
		if(_classCharacter == classCharacter.Mage)
		{
			PlayerManaIntText.GetComponent<Text>().text = "Mana: " + PlayerManaInt.ToString();
			ProfessionCharacterText.GetComponent<Text>().text = "Profession: " + _archer.ToString();
		}
		else if(_classCharacter == classCharacter.Warrior)
		{
			PlayerManaIntText.GetComponent<Text>().text = "Rage: " + PlayerManaInt.ToString();
			ProfessionCharacterText.GetComponent<Text>().text = "Profession: " + _warrior.ToString();
		}
		else if(_classCharacter == classCharacter.Archer)
		{
			PlayerManaIntText.GetComponent<Text>().text = "Energy: " + PlayerManaInt.ToString();
			ProfessionCharacterText.GetComponent<Text>().text = "Profession: " + _archer.ToString();
			
		}
		
		PlayerDamageText.GetComponent<Text>().text = "Damage: " + PlayerDamage.ToString();
		PlayerDefenceText.GetComponent<Text>().text = "Defence:" + PlayerDefence.ToString();
		PlayerRegenHPText.GetComponent<Text>().text = "Regen HP:" + RegenHPValue.ToString();
		PlayerRegenMPText.GetComponent<Text>().text = "Regen MP:" + RegenMPValue.ToString();
		PlayerHealthTextObj.GetComponent<Text>().text = "HP:" + MaxPlayerHealth.ToString();
		PlayerManaTextObj.GetComponent<Text>().text = "MP:" + MaxPlayerMana.ToString();
		
		if(PlayerHealth <= 0)
		{
			GetComponent<Animator>().Play("Death");
			die();
		}
		else
		{
			Invoke("timeChangeDistToPlayerSpawn", 2.0f);
		}
		
		if(riceAgain)
		{
			SpawnCharacterPlayer.instance.player.transform.position = SpawnCharacterPlayer.instance.PlayerSpawn.transform.position;
		}
	}
	
	private void timeChangeDistToPlayerSpawn()
	{
		riceAgain = false;
	}
	
	private void die()
	{
		_menuDie.menuActive();
	}
	
	private IEnumerator RegenHP()
	{
		while(livePlayer)
		{
			if(PlayerHealth < MaxPlayerHealth)
			{
				PlayerHealth += RegenHPValue;
			}
			yield return new WaitForSeconds(waitTimeHpRegen);
		}
	}
	
	private IEnumerator RegenMP()
	{
		while(livePlayer)
		{
			if(PlayerMana < MaxPlayerMana)
			{
				PlayerMana += RegenMPValue;
			}
			yield return new WaitForSeconds(waitTimeMpRegen);
		}
	}
	
	private void passiveLevelUpText()
	{
		levelUpText.transform.GetChild(0).gameObject.SetActive(false);
	}
	
	private void levelUp()
	{
		if(PlayerExpValue >= PlayerExpMaxValue && PlayerLevel < PlayerMaxLevel)
		{
			float diff = PlayerExpValue - PlayerExpMaxValue;
			PlayerExpValue = diff;
			PlayerLevel++;
			PlayerHealth = MaxPlayerHealth;
			PlayerMana = MaxPlayerMana;
			PlayerExpMaxValue *= 1.35f;
			PlayerHealth *= 1.3f;
			MaxPlayerHealth *= 1.1f;
			MaxPlayerMana *= 1.2f;
			PlayerStrength += 3;
			PlayerDexterity += 2;
			PlayerStamina += 4;
			PlayerManaInt += 1;
			GameObject obj = Instantiate(LevelUpObj);
			obj.transform.SetParent(this.transform);
			obj.transform.position = transform.position + new Vector3(0,2,0);
			Destroy(obj, 2.0f);
			PlayerDamage += 3;
			PlayerDexterity += 2;
			levelUpText.transform.GetChild(0).gameObject.SetActive(true);
			levelUpText.transform.GetChild(0).GetComponent<Text>().text = "Level up: " + PlayerLevel.ToString();
			Invoke("passiveLevelUpText", 8.0f);
			audioLevelUp.Play();
		}
	}
}
