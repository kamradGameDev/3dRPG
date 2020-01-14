using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour 
{
	public static DropItem instance;
    public Image img;
	public Text inputF;
	public GameObject itemObj;
	
	public string _text;
	
	public Text NameItem, CountItem;
	
	private void Awake()
	{
		if(!instance)
		{
			instance = this;
		}
	}
	
	public void startChangeItem(GameObject obj)
	{
	    itemObj = obj;
		img.sprite = itemObj.GetComponent<Item>().ItemImg.sprite;
		NameItem.text = itemObj.GetComponent<Item>().ItemName;
		CountItem.text = itemObj.GetComponent<Item>().CountItem.ToString();
		//this.gameObject.transform.parent.gameObject.SetActive(true);
		
	}
	
	public void MinusItemsOrDestroy()
	{
		int count;
		int.TryParse(_text, out count);
		if(count == itemObj.GetComponent<Item>().CountItem)
		{
		    Destroy(itemObj);
			FindUIStatic.instance.dropPanel.SetActive(false);
		}
		else
		{
		    itemObj.GetComponent<Item>().CountItem -= count;
		}
	}
	
	public void changeNumeral(GameObject _obj)
	{
	    //_obj = this.gameObject;
		
		//for(int i = 0; ; i++)
		//{
		    if(string.IsNullOrEmpty(inputF.text))
			{
			    _text = _obj.transform.GetChild(0).GetComponent<Text>().text;
				inputF.text = _text;
			}
			else
			{
			    string s = inputF.text;
				string a = _obj.transform.GetChild(0).GetComponent<Text>().text;
				_text = s + a;
				int count;
				int.TryParse(_text, out count);
				if(itemObj.GetComponent<Item>().CountItem < count)
				{
				    _text = itemObj.GetComponent<Item>().CountItem.ToString();
				    count = itemObj.GetComponent<Item>().CountItem;
				    inputF.text = count.ToString();
				}
			    //inputF.text = _text;
			}
			//break;
		//}
		
	}
	
}
