using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum classQuest
{
	FindTarget,
	FindEnemy,
	FindResources,
	KillEnemy
}

public enum statusQuest
{
	NoStartProgress,
	InProgress,
	EndProgress
}

public class Quest : MonoBehaviour 
{
    public int Id, IdQuestKill, QuestRewardGold, killEnemy, QuestKillEnemy;
	public float QuestRewardExp;
    public string nameQuest, descQuest, StrTarget;
	public GameObject Target, Player, quest, WarningText;
	public Text NameText, ClassQuestText, DescText;
	public classQuest _classQuest;
	public statusQuest _statusQuest;
	public GameObject canvas;
	PlayerAttributes _playerAttributes;
	HistoryGame _historyGame;
	DataBaseAllQuests _dataBaseAllQuests;
	
	private void Start()
	{
		_playerAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
		_historyGame = GameObject.FindObjectOfType<HistoryGame>();
		NameText.text = nameQuest;
		ClassQuestText.text = _classQuest.ToString();
		DescText.text = descQuest;
		canvas = GameObject.Find("Canvas");
		WarningText = GameObject.Find("WarningObject");
		_dataBaseAllQuests = GameObject.FindObjectOfType<DataBaseAllQuests>();
	}
	
	public void checkStatusQuest()
	{
	    string classQ = _classQuest.ToString();
		string status = _statusQuest.ToString();
		if(classQ == "FindTarget")
		{
			if(status == "InProgress")
			{
				Target = GameObject.Find(StrTarget);
				Player = GameObject.FindWithTag("Player");
				if(Player != null)
				{
					Vector3 diff = Player.transform.position - Target.transform.position;
					float curDistance = diff.sqrMagnitude;
					if(curDistance < 10.0f * 10.0f)
					{
						Debug.Log("Quest comply");
						_statusQuest = statusQuest.EndProgress;
						Player.GetComponent<PlayerAttributes>().PlayerGold += QuestRewardGold;
						Player.GetComponent<PlayerAttributes>().PlayerExpValue += QuestRewardExp;
						if(_statusQuest == statusQuest.EndProgress)
						{
							for(int i = 0; i < _historyGame.activeQuests.Length; i++)
							{
								if(_historyGame.activeQuests[i].transform.childCount > 0)
								{ 
								    if(_historyGame.activeQuests[i].transform.GetChild(0).GetComponent<Quest>().Id == Id)
									{
										/*if(_activeQuests.Quests[i].transform.GetChild(0).GetComponent<Quest>().descQuest == descQuest)
											{
											_checkStatusQuest.DinamicQuestPanel.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
											//break;
										}*/
										//Destroy(_historyGame.questPanelcontent.GetChild(i).GetChild(0).gameObject);
									}
									
								}
							}
							
							for(int j = 0; j < _dataBaseAllQuests.allQuests.Length; j++)
							{
								if(_dataBaseAllQuests.allQuests[j].GetComponent<Quest>().Id == Id)
								{
									_dataBaseAllQuests.allQuests[j].GetComponent<Quest>()._statusQuest = statusQuest.EndProgress;
									//Destroy(_activeQuests.Quests[j].transform.GetChild(0).gameObject);
									
									/*if(_checkStatusQuest.DinamicQuestPanel.transform.childCount > 0)
										{
										_checkStatusQuest.DinamicQuestPanel.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<Text>().text = "";
									}*/
									//break;
								}
							}
						}
					}
				}
			}
		}
		
		if(classQ == "KillEnemy")
		{
			if(status == "InProgress")
			{
				if(killEnemy >= QuestKillEnemy)
				{	
					WarningText.transform.SetParent(canvas.transform);
					WarningText.transform.GetChild(0).gameObject.SetActive(true);
					WarningText.transform.GetChild(0).GetComponent<Text>().text = "Quest comply";
					Invoke("passiveWarningTextText", 2.0f);
					_statusQuest = statusQuest.EndProgress;
					Player = GameObject.FindWithTag("Player");
					Player.GetComponent<PlayerAttributes>().GetComponent<PlayerAttributes>().PlayerGold += QuestRewardGold;
					Player.GetComponent<PlayerAttributes>().GetComponent<PlayerAttributes>().PlayerExpValue += QuestRewardExp;
					Debug.Log(Player.GetComponent<PlayerAttributes>().GetComponent<PlayerAttributes>().PlayerExpValue += QuestRewardExp);
					/*for(int i = 0; i < _historyGame.questPanelcontent.childCount; i++)
					{
						if(_historyGame.questPanelcontent.GetChild(i).childCount > 0)
						{
							if(_historyGame.questPanelcontent.GetChild(i).GetChild(0).GetComponent<Quest>().Id == Id)
							{
								Destroy(_historyGame.questPanelcontent.GetChild(i).GetChild(0));
							}
						}
					}*/
				}
			}
			
			/*if(_statusQuest == statusQuest.EndProgress)
				{
				//_checkStatusQuest.activeQuest[i] = null;
				//Destroy(_historyGame.Panel.GetChild(i).GetChild(0).gameObject);
				for(int j = 0; j < _dataBaseAllQuests.allQuests.Length; j++)
				{
				if(_dataBaseAllQuests.allQuests[j].GetComponent<Quest>().Id == Id)
				{
				_dataBaseAllQuests.allQuests[j].GetComponent<Quest>()._statusQuest = statusQuest.EndProgress;
				Debug.Log("comply quest 1");
				}
				}
			}*/
		}
	}
	
	private void passiveWarningTextText()
	{
	WarningText.gameObject.SetActive(false);
	}
	
	public void ActiveFullPanel()
	{
		_historyGame.quest = quest;
		_historyGame.FullDescQuestActive();
	}
	
	public enum PartOfGame
	{
		Prologue, One, Two, Three, Four, Epilogue
	}
	public PartOfGame _partOfGame;
}
