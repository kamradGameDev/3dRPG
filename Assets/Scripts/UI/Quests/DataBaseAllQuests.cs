using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class DataBaseAllQuests : MonoBehaviour 
{
	public GameObject[] allQuests;
	public GameObject[] QuestNPC;
	private string QuestInProgress;
	private GameObject Player;
	private string classCharacter;
	
	HistoryGame _historyGame;
	
	private void Start()
	{
	    Player = GameObject.FindGameObjectWithTag("Player");
        classCharacter = Player.GetComponent<PlayerAttributes>()._classCharacter.ToString();
	    QuestInProgress = Application.persistentDataPath + "/" + classCharacter + "/questsInProgress.xml";
		QuestNPC = GameObject.FindGameObjectsWithTag("QuestNPC");
		_historyGame = GameObject.FindObjectOfType<HistoryGame>();
		LoadProgressQuest();
	}
	
	public void SaveProgressQuests()
	{
	    XmlDocument _xmlDoc = new XmlDocument();
		XmlNode rootNode = _xmlDoc.CreateElement("AllQuests");
		_xmlDoc.AppendChild(rootNode);
		
		for(int i = 0; i < allQuests.Length; i++)
		{
			if(allQuests[i] != null)
			{
			    XmlElement element = _xmlDoc.CreateElement(allQuests[i].name);
				element.SetAttribute("value", allQuests[i].GetComponent<Quest>()._statusQuest.ToString());
				rootNode.AppendChild(element);
			}
		}
		_xmlDoc.Save(QuestInProgress);
	}
	
	public void LoadProgressQuest()
	{
	    if(File.Exists(QuestInProgress))
		{
		    XmlTextReader reader = new XmlTextReader(QuestInProgress);
			while(reader.Read())
			{
			    for(int i = 0; i < allQuests.Length; i++)
				{
					if(allQuests[i] != null)
					{
						if(reader.Name == allQuests[i].name)
						{
							//Debug.Log(allQuests[i].name);
							if(reader.GetAttribute("value") == "InProgress")
							{
								allQuests[i].GetComponent<Quest>()._statusQuest = statusQuest.InProgress;
								StartQuest();
								/* if(QuestNPC[i].GetComponent<NPC_Quest>().AvailableQuest[i] != null)
									{
									if(QuestNPC[i].GetComponent<NPC_Quest>().AvailableQuest[i].name == allQuests[i].name)
									{
									QuestNPC[i].GetComponent<NPC_Quest>().AvailableQuest[i].GetComponent<Quest>()._statusQuest = statusQuest.InProgress;
									}
								}*/
							}
							else if(reader.GetAttribute("value") == "EndProgress")
							{
								allQuests[i].GetComponent<Quest>()._statusQuest = statusQuest.EndProgress;
								
								if(allQuests[i].GetComponent<Quest>()._statusQuest == statusQuest.EndProgress)
								{
									//QuestNPC[i].GetComponent<NPC_Quest>().AvailableQuest[i].SetActive(false);
									allQuests[i] = null;
								}
								//break;
							}
						}
						//break;
					}
					//break;
				}
			}
			reader.Close();
		}
	}
	
	private void StartQuest()
	{
		for(int i = 0; i < allQuests.Length; i++)
		{
			if(allQuests[i] != null)
			{
				if(allQuests[i].GetComponent<Quest>()._statusQuest == statusQuest.InProgress)
				{
					if(_historyGame.activeQuests[i].transform.childCount == 0)
					{
						int index = i;
						GameObject obj = Instantiate(allQuests[i]);
						obj.transform.SetParent(_historyGame.activeQuests[index - 1].transform.transform);
						obj.transform.localScale = Vector3.one;
						obj.transform.position = _historyGame.activeQuests[index - 1].transform.transform.position;
						//changeIndexPanelQuests(index);
					}
					//break;
				}
				/*if(HistoryPogress.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).gameObject.activeSelf == false)
					{
					Destroy(HistoryPogress.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).gameObject);
				}*/
				//break;
			}
		}
	}
}
