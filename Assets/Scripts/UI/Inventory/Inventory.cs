using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	DataBase_Items _dataBase_Items;
	PlayerAttributes _playerAttributes;
	
	public Transform ContentView;
	
	public Text PlayerGoldText;
	
	[SerializeField]
	private int capacity = 20;
	
	public Cell[] content;
	
	private void Start()
	{
		_dataBase_Items = GameObject.FindObjectOfType<DataBase_Items>();
		_playerAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
		content = new Cell[capacity];
		CreateCell();
	}
	
	void Update()
	{
		PlayerGoldText.text = "Gold: " + _playerAttributes.PlayerGold.ToString();
	}
	
	/*private void CreateSlotMoney()
		{
		GameObject moneyObj = Instantiate(moneyObjEquip);
		moneyObj.transform.SetParent(content[35].transform);
		moneyObj.transform.localScale = Vector3.one;
		moneyObj.transform.position = moneyObj.transform.parent.position;
		moneyObj.GetComponent<MoneyInven>().cell = content[35];
		_dataBase_Items.itemName[35] = moneyObj.name;
		content[35].transform.GetChild(0).GetComponent<MoneyInven>().StatusInven = true;
		}
	*/
	public void CreateCell()
	{
		for(int i = 0; i < capacity; i++)
		{
			GameObject cell = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Inventory/Cell")) as GameObject;
			cell.transform.SetParent(ContentView);
			cell.transform.localScale = Vector3.one;
			cell.name = string.Format("Cell [{0}]", i);
			content[i] = cell.GetComponent<Cell>();
		}
	}
	
	public bool AddDrop(GameObject item, int count)
	{
		for(int i = 0; i < content.Length; i++)
		{	
			if(content[i].transform.childCount != 0)
			{
				if(item.GetComponent<Item>().Id == content[i].transform.GetChild(0).GetComponent<Item>().Id)
				{
					content[i].transform.GetChild(0).GetComponent<Item>().CountItem += count;
					content[i].transform.GetChild(0).GetComponent<Item>()._inventoryStates = InventoryStates.IsInventory;
					content[i].transform.GetChild(0).GetComponent<Item>().CountText.gameObject.SetActive(true);
					return true;
				} 
			}
			else
			{
				GameObject newItem = Instantiate(item);
				newItem.GetComponent<Item>().CountItem = count;
				newItem.transform.SetParent(content[i].transform);
				newItem.transform.localScale = Vector3.one;
				newItem.transform.position = newItem.transform.parent.position;
				newItem.GetComponent<Item>().cell = content[i];
				
				_dataBase_Items.itemId[i] = newItem.GetComponent<Item>().Id;
				content[i].transform.GetChild(0).GetComponent<Item>()._inventoryStates = InventoryStates.IsInventory;
				
				return true;
			}
		}
		return false;
	}
	
	public bool AddDrop_Start(GameObject item)
	{
		for(int i = 0; i < content.Length; i++)
		{
			if(content[i].transform.childCount == 0 && item.GetComponent<Item>().CountItem > 0)
			{
				GameObject newItem = Instantiate(item);
				newItem.transform.SetParent(content[i].transform);
				newItem.transform.localScale = Vector3.one;
				newItem.transform.position = newItem.transform.parent.position;
				newItem.GetComponent<Item>().cell = content[i];
				newItem.GetComponent<Item>()._inventoryStates = InventoryStates.IsInventory;
				_dataBase_Items.itemId[i]= newItem.GetComponent<Item>().Id;
				return true;
			}
		}
		return false;
	}
}
