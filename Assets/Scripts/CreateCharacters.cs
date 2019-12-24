using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System;
using System.IO;

public class CreateCharacters : MonoBehaviour
{
    LoadGame _loadGame;
    public GameObject character;
    public string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/" + "Characters.xml";
        _loadGame = GameObject.FindObjectOfType<LoadGame>();
    }

    public void CreateCharacter()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        if (xmlDoc.SelectSingleNode("Characters") == null)
        {
            XmlNode rootNode = xmlDoc.CreateElement("Characters");
            xmlDoc.AppendChild(rootNode);
            xmlDoc.Save(filePath);
        }

        else if (xmlDoc.SelectSingleNode("Characters") != null)
        {
            xmlDoc.Load(_loadGame.filePath);
            if (xmlDoc.SelectSingleNode(character.name) == null)
            {
                XmlNode rootNode = xmlDoc.DocumentElement;
                XmlElement element = xmlDoc.CreateElement(character.name);
				if(character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._warrior == Warrior.Nothing)
				{
				    element.SetAttribute("Profession", character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._warrior.ToString());
				}
				else if(character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._archer == Archer.Nothing)
				{
				    element.SetAttribute("Profession", character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._archer.ToString());
				}
				else if(character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._mage == Mage.Nothing)
				{
				    element.SetAttribute("Profession", character.GetComponent<LoadCharacter>().character.GetComponent<PlayerAttributes>()._mage.ToString());
				}
                rootNode.AppendChild(element);
                xmlDoc.Save(_loadGame.filePath);
            }
        }

        for (int i = 0; i < _loadGame.characterChangeObj.Length; i++)
        {
            if (_loadGame.characterChangeObj[i].transform.childCount == 0)
            {
                GameObject obj = Instantiate(character);
                obj.transform.SetParent(_loadGame.characterChangeObj[i].transform);
                obj.transform.localScale = Vector3.one;
                obj.transform.position = _loadGame.characterChangeObj[i].transform.position;
                break;
            }
        }
    }
}