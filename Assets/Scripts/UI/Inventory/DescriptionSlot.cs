using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DescriptionSlot : MonoBehaviour
{
	Inventory _inventory;
	NPC_Shop _nPC_Shop;
	DataBase_Items _dataBase_Items;
	
	public Text TopText;
	
	[Header("Текст статов итема")]
	public Text[] DescItem = new Text[6];
	public Text PriceItemText;
	
	public Text ItemText;
	public Text DescTextShopItem;
	public Text PriceTextShopItem;
	
	public Image DescImg;
	
	
	public GameObject EquipButton, RemoveEquipmentButton, IsDropTerrain, DropEquipButton, ShopSellItem, ShopBuy;
	
	public GameObject item;
	
	public GameObject player;
	public GameObject[] PlayerSlots;
	public GameObject warningText;
	
	void Start()
	{
		Invoke("Find", 0.2f);
	}
	
	private void Find()
	{
		player = GameObject.FindWithTag("Player");
		_dataBase_Items = GameObject.FindObjectOfType<DataBase_Items>();
		
		_inventory = GameObject.FindObjectOfType<Inventory>();
		
		_nPC_Shop = GameObject.FindObjectOfType<NPC_Shop>();
		warningText = GameObject.Find("WarningObject");
	}
	
	public void StartGameEquipment(GameObject objectItem)
	{
		player.GetComponent<PlayerAttributes>().PlayerDamage += objectItem.GetComponent<Item>().Damage;
		player.GetComponent<PlayerAttributes>().PlayerDefence += objectItem.GetComponent<Item>().Defence;
		player.GetComponent<PlayerAttributes>().PlayerStrength += objectItem.GetComponent<Item>().Strength;
		player.GetComponent<PlayerAttributes>().PlayerDexterity += objectItem.GetComponent<Item>().Dexterity;
		player.GetComponent<PlayerAttributes>().PlayerStamina += objectItem.GetComponent<Item>().Stamina;
		player.GetComponent<PlayerAttributes>().PlayerManaInt += objectItem.GetComponent<Item>().Mana;
		switch(objectItem.GetComponent<Item>()._classItems)
		{
			case classItems.Weapon:
			GameObject objWeapon = Instantiate(objectItem) as GameObject;
			objWeapon.transform.SetParent(PlayerSlots[1].transform);
			objWeapon.transform.position =  PlayerSlots[1].transform.position;
			objWeapon.transform.localScale = Vector3.one;
			objWeapon.transform.position = objWeapon.transform.parent.position;
			objWeapon.GetComponent<Item>().CountItem = 1;
			objWeapon.GetComponent<Item>().CountText.gameObject.SetActive(false);
			objWeapon.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
			break;
			
			case classItems.Armor:
			GameObject objArmor = Instantiate(objectItem) as GameObject;
			objArmor.transform.SetParent(PlayerSlots[3].transform);
			objArmor.transform.position =  PlayerSlots[3].transform.position;
			objArmor.transform.localScale = Vector3.one;
			objArmor.transform.position = objArmor.transform.parent.position;
			objArmor.GetComponent<Item>().CountItem = 1;
			objArmor.GetComponent<Item>().CountText.gameObject.SetActive(false);
			objArmor.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
			print("Armor");
			break;
			
			case classItems.Head:
			GameObject objHead = Instantiate(objectItem) as GameObject;
			objHead.transform.SetParent(PlayerSlots[0].transform);
			objHead.transform.position =  PlayerSlots[0].transform.position;
			objHead.transform.localScale = Vector3.one;
			objHead.transform.position = objHead.transform.parent.position;
			objHead.GetComponent<Item>().CountItem = 1;
			objHead.GetComponent<Item>().CountText.gameObject.SetActive(false);
			objHead.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
			break;
			
			case classItems.Gloves:
			GameObject objGloves = Instantiate(objectItem) as GameObject;
			objGloves.transform.SetParent(PlayerSlots[2].transform);
			objGloves.transform.position =  PlayerSlots[2].transform.position;
			objGloves.transform.localScale = Vector3.one;
			objGloves.transform.position = objGloves.transform.parent.position;
			objGloves.GetComponent<Item>().CountItem = 1;
			objGloves.GetComponent<Item>().CountText.gameObject.SetActive(false);
			objGloves.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
			break;
			
			case classItems.Foot:
			GameObject objFoot = Instantiate(objectItem) as GameObject;
			objFoot.transform.SetParent(PlayerSlots[4].transform);
			objFoot.transform.position =  PlayerSlots[4].transform.position;
			objFoot.transform.localScale = Vector3.one;
			objFoot.transform.position = objFoot.transform.parent.position;
			objFoot.GetComponent<Item>().CountItem = 1;
			objFoot.GetComponent<Item>().CountText.gameObject.SetActive(false);
			objFoot.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
			break;
		}
	}
	
	public void Equipment()
	{	
	    string classCharacter = player.GetComponent<PlayerAttributes>()._classCharacter.ToString();
		string classItemsCharacter = item.GetComponent<Item>()._classItemsCharacter.ToString();
	    if(classCharacter == classItemsCharacter && player.GetComponent<PlayerAttributes>().PlayerLevel >= item.GetComponent<Item>().MinLevelLimit)
		{
			player.GetComponent<PlayerAttributes>().PlayerDamage += item.GetComponent<Item>().Damage;
			player.GetComponent<PlayerAttributes>().PlayerDefence += item.GetComponent<Item>().Defence;
			player.GetComponent<PlayerAttributes>().PlayerStrength += item.GetComponent<Item>().Strength;
			player.GetComponent<PlayerAttributes>().PlayerDexterity += item.GetComponent<Item>().Dexterity;
			player.GetComponent<PlayerAttributes>().PlayerStamina += item.GetComponent<Item>().Stamina;
			player.GetComponent<PlayerAttributes>().PlayerManaInt += item.GetComponent<Item>().Mana;
		    switch(item.GetComponent<Item>()._classItems)
			{
				case classItems.Weapon:
				if(item.GetComponent<Item>().CountItem > 1 && PlayerSlots[1].transform.childCount == 0)
				{
					GameObject objWeapon = Instantiate(item) as GameObject;
					objWeapon.transform.SetParent(PlayerSlots[1].transform);
					objWeapon.transform.position =  PlayerSlots[1].transform.position;
					objWeapon.transform.localScale = Vector3.one;
					objWeapon.transform.position = objWeapon.transform.parent.position;
					objWeapon.GetComponent<Item>().CountItem = 1;
					objWeapon.GetComponent<Item>().CountText.gameObject.SetActive(false);
					objWeapon.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					item.GetComponent<Item>().CountItem -= 1;
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
					    _dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				
				else if(item.GetComponent<Item>().CountItem == 1 && PlayerSlots[1].transform.childCount == 0)
				{
					GameObject objWeapon = Instantiate(item) as GameObject;
					objWeapon.transform.SetParent(PlayerSlots[1].transform);
					objWeapon.transform.position = PlayerSlots[1].transform.position;
					objWeapon.transform.localScale = Vector3.one;
					objWeapon.transform.position = objWeapon.transform.parent.position;
					objWeapon.GetComponent<Item>().CountItem = 1;
					objWeapon.GetComponent<Item>().CountText.gameObject.SetActive(false);
					
					objWeapon.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					FindUIStatic.instance.invenDescPanel.SetActive(false);
					Destroy(item);
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				break;
				
				case classItems.Armor:
				if(item.GetComponent<Item>().CountItem > 1 &&  PlayerSlots[3].transform.childCount == 0)
				{
					GameObject objArmor = Instantiate(item) as GameObject;
					objArmor.transform.SetParent(PlayerSlots[3].transform);
					objArmor.transform.position = PlayerSlots[3].transform.position;
					objArmor.transform.localScale = Vector3.one;
					objArmor.transform.position = objArmor.transform.parent.position;
					objArmor.GetComponent<Item>().CountItem = 1;
					objArmor.GetComponent<Item>().CountText.gameObject.SetActive(false);
					objArmor.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					
					item.GetComponent<Item>().CountItem -= 1;
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				
				else if(item.GetComponent<Item>().CountItem == 1 && PlayerSlots[3].transform.childCount == 0)
				{
					GameObject objArmor = Instantiate(item) as GameObject;
					objArmor.transform.SetParent(PlayerSlots[3].transform);
					objArmor.transform.position = PlayerSlots[3].transform.position;
					objArmor.transform.localScale = Vector3.one;
					objArmor.transform.position = objArmor.transform.parent.position;
					objArmor.GetComponent<Item>().CountItem = 1;
					objArmor.GetComponent<Item>().CountText.gameObject.SetActive(false);
					
					objArmor.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					FindUIStatic.instance.invenDescPanel.SetActive(false);
					Destroy(item);
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				break;
				
				case classItems.Head:
				if(item.GetComponent<Item>().CountItem > 1 && PlayerSlots[0].transform.childCount == 0)
				{
					GameObject objHead = Instantiate(item) as GameObject;
					objHead.transform.SetParent(PlayerSlots[0].transform);
					objHead.transform.position = PlayerSlots[0].transform.position;
					objHead.transform.localScale = Vector3.one;
					objHead.transform.position = objHead.transform.parent.position;
					objHead.GetComponent<Item>().CountItem = 1;
					objHead.GetComponent<Item>().CountText.gameObject.SetActive(false);
					objHead.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					
					item.GetComponent<Item>().CountItem -= 1;
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				
				else if(item.GetComponent<Item>().CountItem == 1 && PlayerSlots[0].transform.childCount == 0)
				{
					GameObject objHead = Instantiate(item) as GameObject;
					objHead.transform.SetParent(PlayerSlots[0].transform);
					objHead.transform.position = PlayerSlots[0].transform.position;
					objHead.transform.localScale = Vector3.one;
					objHead.transform.position = objHead.transform.parent.position;
					objHead.GetComponent<Item>().CountItem = 1;
					objHead.GetComponent<Item>().CountText.gameObject.SetActive(false);
					
					objHead.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					FindUIStatic.instance.invenDescPanel.SetActive(false);
					Destroy(item);
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				break;
				
				case classItems.Gloves:
				if(item.GetComponent<Item>().CountItem > 1 && PlayerSlots[2].transform.childCount == 0)
				{
					GameObject objGloves = Instantiate(item) as GameObject;
					objGloves.transform.SetParent(PlayerSlots[2].transform);
					objGloves.transform.position = PlayerSlots[2].transform.position;
					objGloves.transform.localScale = Vector3.one;
					objGloves.transform.position = objGloves.transform.parent.position;
					objGloves.GetComponent<Item>().CountItem = 1;
					objGloves.GetComponent<Item>().CountText.gameObject.SetActive(false);
					objGloves.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					
					item.GetComponent<Item>().CountItem -= 1;
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				
				else if(item.GetComponent<Item>().CountItem == 1 && PlayerSlots[2].transform.childCount == 0)
				{
					GameObject objGloves = Instantiate(item) as GameObject;
					objGloves.transform.SetParent(PlayerSlots[2].transform);
					objGloves.transform.position = PlayerSlots[2].transform.position;
					objGloves.transform.localScale = Vector3.one;
					objGloves.transform.position = objGloves.transform.parent.position;
					objGloves.GetComponent<Item>().CountItem = 1;
					objGloves.GetComponent<Item>().CountText.gameObject.SetActive(false);
					
					objGloves.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					FindUIStatic.instance.invenDescPanel.SetActive(false);
					Destroy(item);
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				break;
				
				case classItems.Foot:
				if(item.GetComponent<Item>().CountItem > 1 && PlayerSlots[4].transform.childCount == 0)
				{
					GameObject objFoot = Instantiate(item) as GameObject;
					objFoot.transform.SetParent(PlayerSlots[4].transform);
					objFoot.transform.position =  PlayerSlots[4].transform.position;
					objFoot.transform.localScale = Vector3.one;
					objFoot.transform.position = objFoot.transform.parent.position;
					objFoot.GetComponent<Item>().CountItem = 1;
					objFoot.GetComponent<Item>().CountText.gameObject.SetActive(false);
					objFoot.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					
					item.GetComponent<Item>().CountItem -= 1;
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}
				}
				
				else if(item.GetComponent<Item>().CountItem == 1 && PlayerSlots[4].transform.childCount == 0)
				{
					GameObject objFoot = Instantiate(item) as GameObject;
					objFoot.transform.SetParent(PlayerSlots[4].transform);
					objFoot.transform.position =  PlayerSlots[4].transform.position;
					objFoot.transform.localScale = Vector3.one;
					objFoot.transform.position = objFoot.transform.parent.position;
					objFoot.GetComponent<Item>().CountItem = 1;
					objFoot.GetComponent<Item>().CountText.gameObject.SetActive(false);
					
					objFoot.GetComponent<Item>()._inventoryStates = InventoryStates.IsEquipItem;
					FindUIStatic.instance.invenDescPanel.SetActive(false);
					Destroy(item);   
					for(int i = 0; i < _dataBase_Items.itemId.Length; i++)
					{
						_dataBase_Items.itemId[i] = item.GetComponent<Item>().Id;
					}					
				}
				break;
				
			}
		}
		
		else
		{
		    if(classCharacter != classItemsCharacter)
			{
		        warningText.transform.SetParent(FindUIStatic.instance.invenDescPanel.transform);
			    warningText.transform.GetChild(0).gameObject.SetActive(true);
			    warningText.transform.GetChild(0).GetComponent<Text>().text = "Player class mismatch";
			    Invoke("passiveWarningText", 2.0f);
			}
		    else if(player.GetComponent<PlayerAttributes>().PlayerLevel < item.GetComponent<Item>().MinLevelLimit)
			{
		        warningText.transform.SetParent(FindUIStatic.instance.invenDescPanel.transform);
			    warningText.transform.GetChild(0).gameObject.SetActive(true);
			    warningText.transform.GetChild(0).GetComponent<Text>().text = "Low level player";
			    Invoke("passiveWarningText", 2.0f);
			}
		}
		
	}
	
	private void passiveWarningText()
	{
		warningText.transform.GetChild(0).gameObject.SetActive(false);
	}
	
	public void CloseUI()
	{
		FindUIStatic.instance.invenDescPanel.SetActive(false);
	}
	
	public void RemoveEquipment()
	{
		_inventory.AddDrop(item, item.GetComponent<Item>().CountItem);
		player.GetComponent<PlayerAttributes>().PlayerDamage -= item.GetComponent<Item>().Damage;
		player.GetComponent<PlayerAttributes>().PlayerDefence -= item.GetComponent<Item>().Defence;
		player.GetComponent<PlayerAttributes>().PlayerStrength -= item.GetComponent<Item>().Strength;
		player.GetComponent<PlayerAttributes>().PlayerDexterity -= item.GetComponent<Item>().Dexterity;
		player.GetComponent<PlayerAttributes>().PlayerStamina -= item.GetComponent<Item>().Stamina;
		player.GetComponent<PlayerAttributes>().PlayerManaInt -= item.GetComponent<Item>().Mana;
		Destroy(item);
		FindUIStatic.instance.invenDescPanel.SetActive(false);
	}
	
	public void IsOnDrop()
	{
		if(item.GetComponent<Item>().CountItem > 0)
		{
			for(int i = 0; i < FindUIStatic.instance.lootPanel.GetComponent<LootList>().capacity; i ++)
			{
				if(FindUIStatic.instance.lootPanel.GetComponent<LootList>().lootObj.GetComponent<ItemObject>().itemsObj[i] != null)
				{
					if(FindUIStatic.instance.lootPanel.GetComponent<LootList>().lootObj.GetComponent<ItemObject>().itemsObj[i].GetComponent<Item>().Id == item.GetComponent<Item>().Id)
					{
						_inventory.AddDrop(item, item.GetComponent<Item>().CountItem);
						FindUIStatic.instance.lootPanel.GetComponent<LootList>().lootObj.GetComponent<ItemObject>().itemsObj[i] = null;
						item.GetComponent<Item>().CountItem -= item.GetComponent<Item>().CountItem;
						item.GetComponent<Item>()._inventoryStates = InventoryStates.IsInventory;
						if(item.GetComponent<Item>().CountItem == 0)
						{
							Destroy(item);
							FindUIStatic.instance.invenDescPanel.SetActive(false);
							Debug.Log("Test");
						}
					}
				}
			}
		}
	}
	
	public void ShopSell()
	{
		if(item.GetComponent<Item>().CountItem > 1)
		{
			for(int i = 0; i < _inventory.content.Length; i++)
			{
				if(_inventory.content[i].transform.childCount > 0)
				{
					if(item.GetComponent<Item>().Id == _inventory.content[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Item>().Id)
					{
						item.GetComponent<Item>().CountItem -= 1;
						_inventory.content[i].transform.GetChild(0).gameObject.GetComponent<Item>().CountItem -= 1;
						player.GetComponent<PlayerAttributes>().PlayerGold += item.GetComponent<Item>().PriceInvenItem;
						CloseUI();
					}
				}
				
				//break;
			}
		}
		
		else
		{
			for(int i = 0; i < _inventory.content.Length; i++)
			{
				if(_inventory.content[i].transform.childCount > 0)
				{
					if(item.GetComponent<Item>().Id == _inventory.content[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Item>().Id)
					{
						Destroy(item.gameObject);
						Destroy(_inventory.content[i].transform.GetChild(0).gameObject);
						player.GetComponent<PlayerAttributes>().PlayerGold += item.GetComponent<Item>().PriceInvenItem;
						_nPC_Shop.AddInvenItemsToShop(item);
						CloseUI();
						
					}
				}
			}
			
		}
	}
	
	public void shopBuy()
	{
		if(player.GetComponent<PlayerAttributes>().PlayerGold >= item.GetComponent<Item>().PriceItem)
		{
			for(int i = 0; i < _inventory.content.Length; i++)
			{
				if(_inventory.content[i].transform.childCount > 0)
				{
					if(item.GetComponent<Item>().Id == _inventory.content[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Item>().Id)
					{
						//item.GetComponent<Item>().CountItem ++;
						_inventory.content[i].transform.GetChild(0).gameObject.GetComponent<Item>().CountItem ++;
						player.GetComponent<PlayerAttributes>().PlayerGold -= item.GetComponent<Item>().PriceItem;
						Debug.Log("Предмет куплен.1");
						break;
					}
				}
				else
				{
					_inventory.AddDrop(item, item.GetComponent<Item>().CountItem);
					player.GetComponent<PlayerAttributes>().PlayerGold -= item.GetComponent<Item>().PriceItem;
					Debug.Log("Предмет куплен.2");
					break;
				}
			}
		}
		
		else
		{
			Debug.Log("Недостаточно денег для покупки или нет места в инвентаре.");
		}
	}
	
}
