using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LootList : MonoBehaviour 
{	
	public GameObject Cell;
	
	public Transform ContentView;
	
	public int capacity;
	
	public Cell[] content;
	
	public GameObject lootObj;
	
	void Awake()
	{
		content = new Cell[capacity];
	}
	
	public void CreateCell()
	{
		for(int i = 0; i < lootObj.GetComponent<ItemObject>().itemCount.Length; i++)
		{
			if(lootObj.GetComponent<ItemObject>().itemsObj[i] != null)
			{
			    GameObject cell = Instantiate(Cell);
				//itemsObj[i].transform.GetChild(1).GetComponent<Text>().text = itemsObj[i].name;
				cell.transform.SetParent(ContentView);
				cell.transform.localScale = Vector3.one;
				cell.name = string.Format("Cell [{0}]", i);
				content[i] = cell.GetComponent<Cell>();
			}
		}
	}
	
	public void CloseUI()
	{
	    lootObj.GetComponent<ItemObject>().isTouch = false;
		FindUIStatic.instance.lootPanel.SetActive(false);
		for(int j = 0; j < ContentView.childCount; j++)
		{
			if(ContentView.childCount > 0)
			{
				if(ContentView.GetChild(j) != null)
				{
					Destroy(ContentView.GetChild(j).gameObject);
				}
			}
		}
	}
	
	public void ActiveDestPanel(Transform DescPanel)
	{
		DescPanel.gameObject.SetActive(true);
	}
}
