using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Xml;

public class HistoryGame : MonoBehaviour 
{
	public GameObject[] activeQuests;
	public GameObject[] complyQuest;
	//DataBaseAllQuests _dataBaseAllQuests;
	//private bool OneStartGame = true;
	public Transform questPanelcontent;
	public Text DescText, NameQuest;
	//private float TimetextAnim = 0.05f;
	private bool clickText = false;
	public GameObject ContinieGameButton, FullDescPanel, quest;
	
	public Transform Panel; 
	
	private GameObject Player;
	
	/*public void ClickTextMax()
		{
		clickText = true;
		DescText.text = quest.GetComponent<Quest>().descQuest;
	}*/
	
	void Start()
	{
		//_dataBaseAllQuests = GameObject.FindObjectOfType<DataBaseAllQuests>();
		Player = GameObject.FindGameObjectWithTag("Player");
		//Player.GetComponent<MotionAndroid>().moveSpeed = 0;
		//NameQuest.text = quest.GetComponent<Quest>().nameQuest;
		//AnimatorText();
		FindQuestCells();
	}
	
	private void FindQuestCells()
	{
	    for(int i = 0; i < activeQuests.Length; i++)
		{
		    if(activeQuests[i] == null)
			{
			    activeQuests[i] = questPanelcontent.GetChild(i).gameObject;
			}
		}
	}
	
	private void Update()
	{
	    if(clickText)
		{
		    ContinieGameButton.SetActive(true);
		}
		for(int i = 0; i < activeQuests.Length; i++)
		{
			if(activeQuests[i].GetComponent<Quest>())
			{
				activeQuests[i].GetComponent<Quest>().checkStatusQuest();
			}
		}
	}
	
	/*public void AnimatorText()
		{
		if(OneStartGame == true)
		{
		StartCoroutine("TextAnimMachine");
		}
		else
		{
		PassivePanel(Panel);
		}
	}*/
	
	/*IEnumerator TextAnimMachine()
		{
		for(int i = 0; i <= quest.GetComponent<Quest>().descQuest.Length; i++)
		{
		if(!clickText)
		{
		ContinieGameButton.SetActive(false);
		
		DescText.text = quest.GetComponent<Quest>().descQuest.Substring(0, i);
		yield return new WaitForSeconds(TimetextAnim);
		}
		else
		{
		ContinieGameButton.SetActive(true);
		yield break;
		}
		}
	}*/
	
	public void PassivePanel(Transform panel)
	{
		if(panel.transform.GetChild(2).childCount > 0)
		{
		    for(int i = 0; i <= panel.transform.GetChild(2).GetChild(i).childCount; i++)
			{
				//Destroy(panel.transform.GetChild(2).GetChild(i).gameObject);
				
				break;
			}
		}
	    panel.gameObject.SetActive(false);
		
		//Player.GetComponent<MotionAndroid>().moveSpeed = 12;
	}
	
	public void FullDescPanelPassive()
	{
	    FullDescPanel.SetActive(false);
	}
	public void FullDescQuestActive()
	{
	    FullDescPanel.SetActive(true);
		FullDescPanel.transform.GetChild(0).GetComponent<Text>().text = quest.GetComponent<Quest>().nameQuest;
		FullDescPanel.transform.GetChild(1).GetComponent<Text>().text = quest.GetComponent<Quest>().descQuest;
	}
	
	public void AddQuest()
	{
		Debug.Log("Quest no progress");
		for(int j = 0; j < 10; j++)
		{
			if(questPanelcontent.GetChild(j).childCount > 0)
			{
				if(quest.GetComponent<Quest>().Id == questPanelcontent.GetChild(j).GetComponent<Quest>().Id)
				{
					Debug.Log("this quest is already taken.");
				}
			}
			
			else
			{
				GameObject questObj = Instantiate(quest);
				questObj.transform.SetParent(questPanelcontent.GetChild(j));
				questObj.transform.localScale = Vector3.one;
				questObj.transform.position = questObj.transform.parent.position;
				//questObj.transform.GetChild(3).GetComponent<Text>().text = quest.GetComponent<Quest>().descQuest;
				questObj.transform.GetChild(2).GetComponent<Text>().text = "Name: " + quest.GetComponent<Quest>().nameQuest;
				questObj.transform.GetChild(1).GetComponent<Text>().text = "Part: " +quest.GetComponent<Quest>()._partOfGame.ToString();
				questObj.GetComponent<Quest>()._statusQuest = statusQuest.InProgress;
				activeQuests[j] = questObj;
				/*for(int i = 0; i < _checkStatusQuest.DinamicQuestPanel.transform.childCount; i++)
					{
					if(string.IsNullOrEmpty(_checkStatusQuest.DinamicQuestPanel.transform.GetChild(i).GetComponent<Text>().text))
					{
					_checkStatusQuest.DinamicQuestPanel.transform.GetChild(i).GetComponent<Text>().text = quest.GetComponent<Quest>().descQuest;
					break;
					}
					
				}*/
				/*for(int i = 0; i < _dataBaseAllQuests.allQuests.Length; i++)
					{
					if(_dataBaseAllQuests.allQuests[i].GetComponent<Quest>().Id == questObj.GetComponent<Quest>().Id)
					{
					_dataBaseAllQuests.allQuests[i].GetComponent<Quest>()._statusQuest = statusQuest.InProgress;
					}
					break;
				}*/
				
				Debug.Log("Quest add.");
				break;
			}
			
		}
	}
}
