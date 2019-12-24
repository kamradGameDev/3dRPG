using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; 
using System.Xml;
using System;
using System.IO;

public class DataBase_PlayerStats : MonoBehaviour 
{
	public GameObject player;
	private string classCharacter;
	private string filePath;
	
	private void Start()
	{
	    Invoke("findTaget", 0.2f);
		
	}
	
	private void findTaget()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		classCharacter = player.GetComponent<PlayerAttributes>()._classCharacter.ToString();
		filePath = Application.persistentDataPath + "/" + classCharacter + "/PlayerStats.xml";
		FindAndCreateFileSaved();
		NullDoc();
		Load();
	}
	
	public void Save()
	{
		XmlDocument _xmlDoc = new XmlDocument();
		XmlNode rootNodePlayerStats = _xmlDoc.CreateElement("PlayerStats");
		_xmlDoc.AppendChild(rootNodePlayerStats);
		
		XmlElement PlayerDamage = _xmlDoc.CreateElement("PlayerDamage");
		PlayerDamage.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDamage.ToString());
		rootNodePlayerStats.AppendChild(PlayerDamage);
		
		XmlElement PlayerDefence = _xmlDoc.CreateElement("PlayerDefence");
		PlayerDefence.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDefence.ToString());
		rootNodePlayerStats.AppendChild(PlayerDefence);
		
		XmlElement PlayerGold = _xmlDoc.CreateElement("PlayerGold");
		PlayerGold.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerGold.ToString());
		rootNodePlayerStats.AppendChild(PlayerGold);
		
		XmlElement PlayerStrength = _xmlDoc.CreateElement("PlayerStrength");
		PlayerStrength.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerStrength.ToString());
		rootNodePlayerStats.AppendChild(PlayerStrength);
		
		XmlElement PlayerDexterity = _xmlDoc.CreateElement("PlayerDexterity");
		PlayerDexterity.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDexterity.ToString());
		rootNodePlayerStats.AppendChild(PlayerDexterity);
		
		XmlElement PlayerStamina = _xmlDoc.CreateElement("PlayerStamina");
		PlayerStamina.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerStamina.ToString());
		rootNodePlayerStats.AppendChild(PlayerStamina);
		
		XmlElement PlayerManaInt = _xmlDoc.CreateElement("PlayerManaInt");
		PlayerManaInt.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerManaInt.ToString());
		rootNodePlayerStats.AppendChild(PlayerManaInt);
		
		XmlElement PlayerHealth = _xmlDoc.CreateElement("PlayerHealth");
		PlayerHealth.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerHealth.ToString());
		rootNodePlayerStats.AppendChild(PlayerHealth);
		
		XmlElement MaxPlayerHealth = _xmlDoc.CreateElement("MaxPlayerHealth");
		MaxPlayerHealth.SetAttribute("value", player.GetComponent<PlayerAttributes>().MaxPlayerHealth.ToString());
		rootNodePlayerStats.AppendChild(MaxPlayerHealth);
		
		XmlElement PlayerMana = _xmlDoc.CreateElement("PlayerMana");
		PlayerMana.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerMana.ToString());
		rootNodePlayerStats.AppendChild(PlayerMana);
		
		XmlElement MaxPlayerMana = _xmlDoc.CreateElement("MaxPlayerMana");
		MaxPlayerMana.SetAttribute("value", player.GetComponent<PlayerAttributes>().MaxPlayerMana.ToString());
		rootNodePlayerStats.AppendChild(MaxPlayerMana);
		
		XmlElement PlayerLevel = _xmlDoc.CreateElement("PlayerLevel");
		PlayerLevel.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerLevel.ToString());
		rootNodePlayerStats.AppendChild(PlayerLevel);
		
		XmlElement PlayerExpValue= _xmlDoc.CreateElement("PlayerExpValue");
		PlayerExpValue.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerExpValue.ToString());
		rootNodePlayerStats.AppendChild(PlayerExpValue);
		
		XmlElement PlayerExpMaxValue= _xmlDoc.CreateElement("PlayerExpMaxValue");
		PlayerExpMaxValue.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerExpMaxValue.ToString());
		rootNodePlayerStats.AppendChild(PlayerExpMaxValue);
		
		_xmlDoc.Save(filePath);
	}
	
	private void FindAndCreateFileSaved()
	{
	    if(!File.Exists(filePath))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/" + classCharacter);
			using(FileStream fs = File.Create(filePath));
		}
	}
	
	private void NullDoc()
	{
	    if(File.Exists(filePath))
		{
		    string[] nullDoc = File.ReadAllLines(filePath);
			if(nullDoc.Length == 0)
			{
				XmlDocument _xmlDoc = new XmlDocument();
				XmlNode rootNodePlayerStats = _xmlDoc.CreateElement("PlayerStats");
				_xmlDoc.AppendChild(rootNodePlayerStats);
				
				XmlElement PlayerDamage = _xmlDoc.CreateElement("PlayerDamage");
				PlayerDamage.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDamage.ToString());
				rootNodePlayerStats.AppendChild(PlayerDamage);
				
				XmlElement PlayerDefence = _xmlDoc.CreateElement("PlayerDefence");
				PlayerDefence.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDefence.ToString());
				rootNodePlayerStats.AppendChild(PlayerDefence);
				
				XmlElement PlayerGold = _xmlDoc.CreateElement("PlayerGold");
				PlayerGold.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerGold.ToString());
				rootNodePlayerStats.AppendChild(PlayerGold);
				
				XmlElement PlayerStrength = _xmlDoc.CreateElement("PlayerStrength");
				PlayerStrength.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerStrength.ToString());
				rootNodePlayerStats.AppendChild(PlayerStrength);
				
				XmlElement PlayerDexterity = _xmlDoc.CreateElement("PlayerDexterity");
				PlayerDexterity.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerDexterity.ToString());
				rootNodePlayerStats.AppendChild(PlayerDexterity);
				
				XmlElement PlayerStamina = _xmlDoc.CreateElement("PlayerStamina");
				PlayerStamina.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerStamina.ToString());
				rootNodePlayerStats.AppendChild(PlayerStamina);
				
				XmlElement PlayerManaInt = _xmlDoc.CreateElement("PlayerManaInt");
				PlayerManaInt.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerManaInt.ToString());
				rootNodePlayerStats.AppendChild(PlayerManaInt);
				
				XmlElement PlayerHealth = _xmlDoc.CreateElement("PlayerHealth");
				PlayerHealth.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerHealth.ToString());
				rootNodePlayerStats.AppendChild(PlayerHealth);
				
				XmlElement MaxPlayerHealth = _xmlDoc.CreateElement("MaxPlayerHealth");
				MaxPlayerHealth.SetAttribute("value", player.GetComponent<PlayerAttributes>().MaxPlayerHealth.ToString());
				rootNodePlayerStats.AppendChild(MaxPlayerHealth);
				
				XmlElement PlayerMana = _xmlDoc.CreateElement("PlayerMana");
				PlayerMana.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerMana.ToString());
				rootNodePlayerStats.AppendChild(PlayerMana);
				
				XmlElement MaxPlayerMana = _xmlDoc.CreateElement("MaxPlayerMana");
				MaxPlayerMana.SetAttribute("value", player.GetComponent<PlayerAttributes>().MaxPlayerMana.ToString());
				rootNodePlayerStats.AppendChild(MaxPlayerMana);
				
				XmlElement PlayerLevel = _xmlDoc.CreateElement("PlayerLevel");
				PlayerLevel.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerLevel.ToString());
				rootNodePlayerStats.AppendChild(PlayerLevel);
				
				XmlElement PlayerExpValue= _xmlDoc.CreateElement("PlayerExpValue");
				PlayerExpValue.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerExpValue.ToString());
				rootNodePlayerStats.AppendChild(PlayerExpValue);
				
				XmlElement PlayerExpMaxValue= _xmlDoc.CreateElement("PlayerExpMaxValue");
				PlayerExpMaxValue.SetAttribute("value", player.GetComponent<PlayerAttributes>().PlayerExpMaxValue.ToString());
				rootNodePlayerStats.AppendChild(PlayerExpMaxValue);
				_xmlDoc.Save(filePath);
			}
		}
	}
	
	private void Load()
	{
		if(File.Exists(filePath))
		{
			XmlTextReader reader = new XmlTextReader(filePath);
			while(reader.Read())
			{
				if(reader.Name == "PlayerDamage")
				{
					player.GetComponent<PlayerAttributes>().PlayerDamage = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerExpValue")
				{
					player.GetComponent<PlayerAttributes>().PlayerExpValue = Convert.ToSingle(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerDefence")
				{
					player.GetComponent<PlayerAttributes>().PlayerDefence = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerStamina")
				{
					player.GetComponent<PlayerAttributes>().PlayerStamina = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerManaInt")
				{
					player.GetComponent<PlayerAttributes>().PlayerManaInt = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerGold")
				{
					player.GetComponent<PlayerAttributes>().PlayerGold = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerStrength")
				{
					player.GetComponent<PlayerAttributes>().PlayerStrength = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerDexterity")
				{
					player.GetComponent<PlayerAttributes>().PlayerDexterity = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerMana")
				{
					player.GetComponent<PlayerAttributes>().PlayerMana = Convert.ToSingle(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "MaxPlayerMana")
				{
					player.GetComponent<PlayerAttributes>().MaxPlayerMana = Convert.ToSingle(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerHealth")
				{
					player.GetComponent<PlayerAttributes>().PlayerHealth = Convert.ToSingle(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "MaxPlayerHealth")
				{
					player.GetComponent<PlayerAttributes>().MaxPlayerHealth = Convert.ToSingle(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerLevel")
				{
					player.GetComponent<PlayerAttributes>().PlayerLevel = Convert.ToInt32(reader.GetAttribute("value"));
				}
				
				if(reader.Name == "PlayerExpMaxValue")
				{
					player.GetComponent<PlayerAttributes>().PlayerExpMaxValue = Convert.ToSingle(reader.GetAttribute("value"));
				}
			}	
			reader.Close();
		}
	}
}
