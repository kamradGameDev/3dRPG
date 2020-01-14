using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public enum InventoryStates
{
	IsInventory,
	IsDropTerrain,
	IsEquipItem,
	isShop,
	isShopSell
}

public enum classItems
{
	Weapon,
	Armor,
	Head,
	Gloves,
	Foot,
	AnotherItems
}

public enum classItemsCharacter
{
    Warrior,
	Archer,
	Mage,
	All
}

public enum RateItems
{
	Low,
	Normal,
	Epic,
	AnotherItems
}

public class Item : MonoBehaviour
{
	PlayerAttributes _playerAttributes;
	
	[HideInInspector]
	public Cell cell;
	private Transform canvas;
	public Text CountText;
	
	public Image ItemImg;
	
	[Header("Статы итема")]
	public int Damage = 20;
	public int Defence = 3;
	
	public int Strength = 3;
	public int Dexterity = 2;
	public int Stamina = 5;
	public int Mana = 1;
	
	public int PriceInvenItem;
	public int PriceItem;
	
	public int MinLevelLimit = 1;
	
	
	public int PlayerRegenHealthOrMana;
	public string ItemName = "Простой меч.";
	public string ItemDesc = "Простая банка жизни, лечит на: ";
	
	[Header("Количество предметов")]
	public int CountItem = 1;
	
	[Header("ID итема")]
	public int Id = 0;
	
	public InventoryStates _inventoryStates;
	public classItems _classItems;
	
	public RateItems _rateItems;
	public classItemsCharacter _classItemsCharacter;
	
	void Start()
	{
		_playerAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
		ItemImg = this.gameObject.GetComponent<Image>();
		if(_inventoryStates == InventoryStates.isShopSell)
		{
			PriceInvenItem = PriceItem / 2;
		}
		else
		{
			PriceInvenItem = PriceItem;
		}
	}
	
	void Update()
	{
		changeStatusText();
	}
	
	private void changeStatusText()
	{	
	    switch (_inventoryStates)
		{
			case InventoryStates.IsInventory:
			    this.gameObject.transform.GetChild(2).gameObject.SetActive(false); 
			    this.gameObject.transform.GetChild(1).gameObject.SetActive(false); 
				this.gameObject.transform.GetChild(0).gameObject.SetActive(true); 
			    CountText.text = CountItem.ToString();
			break;
			
			case InventoryStates.isShopSell:
			    this.gameObject.transform.GetChild(2).gameObject.SetActive(true); 
			    this.gameObject.transform.GetChild(1).gameObject.SetActive(true); 
			    this.gameObject.transform.GetChild(0).gameObject.SetActive(true); 
			    this.gameObject.transform.GetChild(2).GetComponent<Text>().text = PriceInvenItem.ToString();
			    this.gameObject.transform.GetChild(1).GetComponent<Text>().text = ItemName;
				 CountText.text = CountItem.ToString();
			break;
			
			case InventoryStates.IsDropTerrain:
			    this.gameObject.transform.GetChild(2).gameObject.SetActive(false); 
			    this.gameObject.transform.GetChild(1).gameObject.SetActive(false); 
			    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			    CountText.text = CountItem.ToString();
			break;
			
			case InventoryStates.IsEquipItem:
			    this.gameObject.transform.GetChild(2).gameObject.SetActive(false); 
			    this.gameObject.transform.GetChild(1).gameObject.SetActive(false); 
			    this.gameObject.transform.GetChild(0).gameObject.SetActive(false); 
			break;
			
			case InventoryStates.isShop:
			    this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
			    this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
			    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			    this.gameObject.transform.GetChild(2).GetComponent<Text>().text = PriceInvenItem.ToString();
			    this.gameObject.transform.GetChild(1).GetComponent<Text>().text = ItemName;
			break;
		}
	}
	
	public void SeeEquip()
	{
		FindUIStatic.instance.invenDescPanel.SetActive(true);
		
		FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().item = this.gameObject;
		FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescImg.sprite = ItemImg.sprite;
		
		switch(_inventoryStates)
		{
			case InventoryStates.IsInventory:
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().EquipButton.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().RemoveEquipmentButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DropEquipButton.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().IsDropTerrain.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopSellItem.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopBuy.SetActive(false);
			DropItem.instance.startChangeItem(this.gameObject);
			break;
			
			case InventoryStates.IsDropTerrain:
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().EquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().RemoveEquipmentButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DropEquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().IsDropTerrain.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopSellItem.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopBuy.SetActive(false);
			break;
			
			case InventoryStates.IsEquipItem:
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().EquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().RemoveEquipmentButton.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DropEquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().IsDropTerrain.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopSellItem.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopBuy.SetActive(false);
			break;
			
			case InventoryStates.isShop:
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescImg.sprite = ItemImg.sprite;
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().EquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().RemoveEquipmentButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DropEquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().IsDropTerrain.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopSellItem.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopBuy.SetActive(true);
			break;
			
			case InventoryStates.isShopSell:
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().EquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().RemoveEquipmentButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DropEquipButton.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().IsDropTerrain.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopSellItem.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ShopBuy.SetActive(false);
			break;
			
		}
		if(this.gameObject.GetComponent<Item>()._classItems == classItems.Weapon || this.gameObject.GetComponent<Item>()._classItems == classItems.Armor || this.gameObject.GetComponent<Item>()._classItems == classItems.Head || this.gameObject.GetComponent<Item>()._classItems == classItems.Gloves || this.gameObject.GetComponent<Item>()._classItems == classItems.Foot)
		{
			for(int i = 0; i < FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem.Length; i++)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[i].gameObject.SetActive(true);
			}
			
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ItemText.gameObject.SetActive(false);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[0].text = "Damage: " + _playerAttributes.PlayerDamage.ToString() + "+" + Damage.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[1].text = "Defence: " +_playerAttributes.PlayerDefence.ToString() + "+" + Defence.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[2].text = "Strength: " +  _playerAttributes.PlayerStrength.ToString() + "+" + Strength.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[3].text = "Dexterity: " + _playerAttributes.PlayerDexterity.ToString() + "+" + Dexterity.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[4].text = "Health: " + _playerAttributes.PlayerStamina.ToString() + "+" + Stamina.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[5].text = "Mana: " + _playerAttributes.PlayerManaInt.ToString() + "+" + Mana.ToString();
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[6].text = "Class Character: " + _classItemsCharacter.ToString();
			if(_classItemsCharacter.ToString() != _playerAttributes._classCharacter.ToString())
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[6].color = Color.red;
			}
			else
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[6].color = Color.green;
			}
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[7].text = "Min Level: " + this.gameObject.GetComponent<Item>().MinLevelLimit.ToString();
			if(_playerAttributes.PlayerLevel < this.gameObject.GetComponent<Item>().MinLevelLimit)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[7].color = Color.red;
			}
			else
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[7].color = Color.green;
			}
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().PriceItemText.text = "Price this item: " + PriceInvenItem.ToString();
			if(_rateItems == RateItems.Low)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.color = Color.yellow;
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.text = ItemName;
			} 
			
			if(_rateItems == RateItems.Normal)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.color = Color.blue;
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.text = ItemName;
			}
			
			if(_rateItems == RateItems.Epic)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.color = Color.red;
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.text = ItemName;
			} 
		}
		
		else if(this.gameObject.GetComponent<Item>()._classItems == classItems.AnotherItems)
		{
			for(int i = 0; i < FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem.Length; i++)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().DescItem[i].gameObject.SetActive(false);
			}
			
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ItemText.gameObject.SetActive(true);
			FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().ItemText.text = ItemDesc;
			
			if(_rateItems == RateItems.AnotherItems)
			{
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.color = Color.green;
				FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().TopText.text = ItemName;
			} 
		}
	}
}
