using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_Shop : MonoBehaviour 
{	
    Inventory _inventory;
	PlayerAttributes _playerAttributes;
	
	private GameObject Player;
	private float dist = 20.0f;
	public GameObject[] itemsShop;
	private Animator anim;
	
	public Transform ContentView;
	public Transform ContentViewShop;
	public Text PlayerGoldText;
	
	[SerializeField]
	private int capacity = 20;
	public Cell[] content;
	public Cell[] contentShop;
	
	public GameObject UiPanel;
	
	private void Start()
	{
		_inventory = GameObject.FindObjectOfType<Inventory>();
		_playerAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
		Player = GameObject.FindWithTag("Player");
		CreateCell();
		anim = UiPanel.GetComponent<Animator>();
	}
	
	private void Update()
	{
		if(PlayerGoldText.gameObject.activeSelf)
		{
			PlayerGoldText.text = "Gold: " + _playerAttributes.PlayerGold.ToString();
		}
	}
	
	private void OnMouseDown()
	{
		Vector3 offSet = Player.transform.position - transform.position;
		float sqrLen = offSet.sqrMagnitude;
		if(sqrLen <= dist * dist)
		{
			anim.SetBool("Passive", false);
		    anim.SetBool("Active", true);
			for(int j = 0; j < _inventory.content.Length; j++)
			{
				if(_inventory.content[j].transform.childCount != 0)
				{
					AddInvenItemsToShop(_inventory.content[j].transform.GetChild(0).gameObject);
				} 
			}
		}
		AddItemsShop();
	}
	
	public void CloseUI()
	{
		anim.SetBool("Passive", false);
		anim.SetBool("Active", true);
		for(int i = 0; i < content.Length; i++)
		{
			if(content[i].transform.childCount != 0)
			{
				Destroy(content[i].transform.GetChild(0).gameObject);
			}
			if(contentShop[i].transform.childCount != 0)
			{
				Destroy(contentShop[i].transform.GetChild(0).gameObject);
			}
		}
	}
	
	public void CreateCell()
	{
		for(int i = 0; i < capacity; i++)
		{
			GameObject cell = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Inventory/CellShop")) as GameObject;
			cell.transform.SetParent(ContentView);
			cell.transform.localScale = Vector3.one;
			cell.name = string.Format("Cell [{0}]", i);
			content[i] = cell.GetComponent<Cell>();
			
			GameObject cellShop = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Inventory/CellShop")) as GameObject;
			cellShop.transform.SetParent(ContentViewShop);
			cellShop.transform.localScale = Vector3.one;
			cellShop.name = string.Format("Cell [{0}]", i);
			contentShop[i] = cellShop.GetComponent<Cell>();
		}
	}
	
	private void AddItemsShop()
	{
		for(int i = 0; i < itemsShop.Length; i++)
		{
			if(contentShop[i].transform.childCount == 0)
			{
				GameObject newItem = Instantiate(itemsShop[i]);
				newItem.transform.SetParent(contentShop[i].transform);
				newItem.transform.position = newItem.transform.parent.position;
				newItem.transform.localScale = Vector3.one;
				newItem.GetComponent<Item>().cell = contentShop[i];
				newItem.GetComponent<Item>()._inventoryStates = InventoryStates.isShop;
			}
		}
	}
	
	public void AddInvenItemsToShop(GameObject items)
	{
		for(int i = 0; i < content.Length; i++)
		{
			if(content[i].transform.childCount == 0)
		    {
			    GameObject newItem = Instantiate(items);
				/*if(newItem.transform.GetChild(2).GetComponent<Text>().text == "0" && newItem.transform.GetChild(1).GetComponent<Text>().text == "0" && newItem.transform.GetChild(0).GetComponent<Text>().text == "0")
					{
					newItem.transform.GetChild(2).GetComponent<Text>().text  = newItem.GetComponent<Item>().PriceInvenItem.ToString();
					newItem.transform.GetChild(1).GetComponent<Text>().text  = newItem.GetComponent<Item>().ItemName.ToString();
					newItem.transform.GetChild(0).GetComponent<Text>().text  = newItem.GetComponent<Item>().CountItem.ToString();
				}*/
			    newItem.transform.SetParent(content[i].transform);
			    newItem.transform.localScale = Vector3.one;
			    newItem.transform.position = newItem.transform.parent.position;
			    newItem.GetComponent<Item>().cell = content[i];
				newItem.GetComponent<Item>()._inventoryStates = InventoryStates.isShopSell;
				break;
			}
		}
	}
	
}
