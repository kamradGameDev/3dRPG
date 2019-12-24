using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemObject : MonoBehaviour 
{
	public GameObject Player;
	private float dist = 10.0f;
	
	public int IdLootObj;
	
	[Header("Панель с информацией лута")]
	public GameObject UIPanel;
	
	[Header("Список  предметов лута")]
	public GameObject[] itemsObj;
	public int[] itemCount;
	public bool isTouch = false;
	
	/*void Update()
		{
		Vector3 offSet = Player.transform.position - transform.position;
		float SqrLen = offSet.sqrMagnitude;
		if(SqrLen >= dist * dist)
		{
		UIPanel.GetComponent<Animator>().SetTrigger("Passive");
		}
		
	}*/
	
	private void OnMouseDown()
	{
	    if(!isTouch)
		{
			Player = GameObject.FindWithTag("Player");
			Vector3 offSet = Player.transform.position - transform.position;
			float SqrLen = offSet.sqrMagnitude;
			if(SqrLen <= dist * dist)
			{
			    UIPanel = FindUIStatic.instance.lootPanel;
				UIPanel.SetActive(true);
				UIPanel.GetComponent<LootList>().lootObj = this.gameObject;
				UIPanel.GetComponent<LootList>().CreateCell();
				AddListItem();
				isTouch = true;
				
				//Time.timeScale = 0.1f;
			}
		}
	}
	
	public void AddListItem()
	{
		for(int i = 0; i < itemCount.Length; i++)
		{
			if(itemsObj[i] != null)
			{
			    if(UIPanel.GetComponent<LootList>().content[i].transform.childCount == 0)
				{
					GameObject lootItem = Instantiate(itemsObj[i]) as GameObject;
					lootItem.transform.SetParent(UIPanel.GetComponent<LootList>().content[i].transform);
					lootItem.transform.localScale = Vector3.one;
					lootItem.transform.position = lootItem.transform.parent.position;
					lootItem.GetComponent<Item>().cell = UIPanel.GetComponent<LootList>().content[i];
					lootItem.GetComponent<Item>().CountItem = itemCount[i];
					//itemsObj[i].GetComponent<Item>()._inventoryStates = InventoryStates.IsInventory;
				}
				
			}
		}
		//UIPanel.GetComponent<LootList>().idArray = 
	}
}
