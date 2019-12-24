using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; 
using System.Xml;
using System; 
using System.IO; 

public class DataBase_Items : MonoBehaviour 
{
	public GameObject player;
	public GameObject[] itemsObj;
	public int[] itemId;
	public int[] itemIdEquips;
	public int[] contentNumber;
	private string classCharacter;
	private string filePathInven;
	private string filePathEquip;
	
	Inventory _inventory;
	
	void Start()
	{
		_inventory = GameObject.FindObjectOfType<Inventory>();
		
		Invoke("find", 0.3f);
	}
	
	private void find()
	{
	    player = GameObject.FindGameObjectWithTag("Player");
		classCharacter = player.GetComponent<PlayerAttributes>()._classCharacter.ToString();
		filePathInven = Application.persistentDataPath + "/" + classCharacter + "/Inventory.xml";
		filePathEquip = Application.persistentDataPath + "/" + classCharacter + "/EquipInven.xml";
		FindAndCreateFileSaved();
		FirstStartEquip();
		FirstStartInven();
		LoadEquipItems();
		Load();
	}
	
	public void SaveEquipItem()
	{
		XmlDocument _xmlDoc = new XmlDocument();
		XmlNode rootNodeEquip = _xmlDoc.CreateElement("EquipItemIds");
		_xmlDoc.AppendChild(rootNodeEquip);
		
		for(int i = 0; i < FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().PlayerSlots.Length; i++)
		{
			if(FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().PlayerSlots[i].transform.childCount != 0)
			{
				XmlElement element = _xmlDoc.CreateElement("EquipItemId_" + i);
				itemIdEquips[i] = FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().PlayerSlots[i].transform.GetChild(0).GetComponent<Item>().Id;
				element.SetAttribute("value", itemIdEquips[i].ToString());
				rootNodeEquip.AppendChild(element);
				
			}
		}
		_xmlDoc.Save(filePathEquip);
	}
	
	public void SaveInven()
	{
		XmlDocument _xmlDoc = new XmlDocument();
		XmlNode rootNodeInven = _xmlDoc.CreateElement("IdItems");
        _xmlDoc.AppendChild(rootNodeInven);
		
		for(int i = 0; i < itemId.Length; i++)
		{
			if(_inventory.content[i].transform.childCount != 0)
			{
				XmlElement element = _xmlDoc.CreateElement("Item" + i);
				contentNumber[i] = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem;
				itemId[i] = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id;
				
				element.SetAttribute("value", itemId[i].ToString());
				element.SetAttribute("count", contentNumber[i].ToString());
				
				
				rootNodeInven.AppendChild(element);
			}
		}
		
		_xmlDoc.Save(filePathInven);
	}
	
	private void FindAndCreateFileSaved()
	{
	    if(!File.Exists(filePathEquip))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/" + classCharacter);
			using(FileStream fs = File.Create(filePathEquip));
		}
	}
	
	private void FirstStartEquip()
	{
	    string[] nullDocEquip = File.ReadAllLines(filePathEquip);
		if(nullDocEquip.Length == 0)
		{
		    XmlDocument _xmlDoc = new XmlDocument();
			XmlNode rootNodeInven = _xmlDoc.CreateElement("IdItems");
			_xmlDoc.AppendChild(rootNodeInven);
			
			for(int i = 0; i < itemId.Length; i++)
			{
				if(_inventory.content[i].transform.childCount > 0)
				{
					XmlElement element = _xmlDoc.CreateElement("IdItem_" + i);
					contentNumber[i] = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem;
					
					element.SetAttribute("value", itemId[i].ToString());
					element.SetAttribute("count", contentNumber[i].ToString());
					
					rootNodeInven.AppendChild(element);
				}
			}
			_xmlDoc.Save(filePathEquip);
		}
	}
	
	private void FirstStartInven()
	{
	    if(!File.Exists(filePathInven))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/" + classCharacter);
			using(FileStream fs = File.Create(filePathInven));
		}
	    string[] nullDocInven = File.ReadAllLines(filePathInven);
		if(nullDocInven.Length == 0)
		{
		    XmlDocument _xmlDoc = new XmlDocument();
			XmlNode rootNodeInven = _xmlDoc.CreateElement("IdItems");
			_xmlDoc.AppendChild(rootNodeInven);
			
			
			for(int i = 0; i < itemId.Length; i++)
			{
				if(_inventory.content[i].transform.childCount != 0)
				{
					XmlElement element = _xmlDoc.CreateElement("IdItem_" + i);
					contentNumber[i] = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().CountItem;
					itemId[i] = _inventory.content[i].transform.GetChild(0).GetComponent<Item>().Id;
					
					element.SetAttribute("value", itemId[i].ToString());
					element.SetAttribute("count", contentNumber[i].ToString());
					
					rootNodeInven.AppendChild(element);
				}
			}
			_xmlDoc.Save(filePathInven);
		}
	}
	
	public void Load()
	{
		
		XmlTextReader reader = new XmlTextReader(filePathInven);
		while(reader.Read())
		{
			for(int i = 0; i < itemsObj.Length; i++)
			{
			    itemId[i] = Convert.ToInt32(reader.GetAttribute("value"));
				contentNumber[i] = Convert.ToInt32(reader.GetAttribute("count"));
				
				if(itemsObj[i].GetComponent<Item>().Id == itemId[i])
				{
					_inventory.AddDrop_Start(itemsObj[i]);
					itemsObj[i].GetComponent<Item>().CountItem = contentNumber[i];
					break;
				}
			}
		}
	    reader.Close();
	}
	
	public void LoadEquipItems()
	{
		XmlTextReader reader = new XmlTextReader(filePathEquip);
		while(reader.Read())
		{
			for(int i = 0; i < itemsObj.Length; i++)
			{
				itemIdEquips[i] = Convert.ToInt32(reader.GetAttribute("value"));
				if(itemsObj[i].GetComponent<Item>().Id == itemIdEquips[i])
				{
					FindUIStatic.instance.DescItem.GetComponent<DescriptionSlot>().StartGameEquipment(itemsObj[i]);
				}
			}	
		}
		reader.Close();
	}
}
