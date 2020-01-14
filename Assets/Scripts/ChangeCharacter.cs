using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; 
using System.Xml;
using System;
using System.IO;


public class ChangeCharacter : MonoBehaviour 
{
	public GameObject panelObj;
	private string classCharacter;
	
	private void Start()
	{
		panelObj = GameObject.Find("ChangeCharacterPanel");
	}
	
	public void LoadCharacter()
	{
		panelObj.transform.GetChild(0).GetComponent<Image>().sprite = GetComponent<Image>().sprite;
		panelObj.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Base class: " + gameObject.name.Replace("(Clone)", "");
		LoadStatsPlayerOnFile();
		LoadPlayerFromFile();
		PlayerPrefs.SetString("CharacterBufer", GetComponent<LoadCharacter>().character.name.Replace("(Clone)", ""));
		PlayerPrefs.Save();
	}
	
	private void LoadStatsPlayerOnFile()
	{
	    classCharacter = GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._classCharacter.ToString();
		
	    string filePath = Application.persistentDataPath + "/" + classCharacter + "/PlayerStats.xml";
		if(File.Exists(filePath))
		{
			XmlTextReader reader = new XmlTextReader(filePath);
			while(reader.Read())
			{
			    Debug.Log("yes");
			    if(reader.Name == "PlayerLevel")
				{
				    panelObj.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Level: " + reader.GetAttribute("value");
				}
			}
			reader.Close();
		}
		else
		{
			panelObj.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Level: 1";
		}
	}
	
	private void LoadPlayerFromFile()
	{
		string filePath = Application.persistentDataPath + "/Characters.xml";
		if(File.Exists(filePath))
		{
			XmlTextReader reader = new XmlTextReader(filePath);
			while(reader.Read())
			{
			    if(reader.Name == GetComponent<LoadCharacter>().character.name)
				{
				    panelObj.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "Profession: " + reader.GetAttribute("Profession");
				}
			}
			reader.Close();
		}
	}
}
