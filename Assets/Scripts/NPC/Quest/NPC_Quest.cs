using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public  class NPC_Quest : MonoBehaviour 
{
	//HistoryGame _historyGame;
	public GameObject questPanel;
	public GameObject[] AvailableQuest;
	public bool touchNPc = false;
	//public Image pointerImage;
	HistoryGame _historyGame;
	//private Vector3 pointerPosition;
	//private Transform player;
	
	private void Start()
	{
		_historyGame = GameObject.FindObjectOfType<HistoryGame>();
		//player = GameObject.FindWithTag("Player").transform;
	}
	
	/*private void Update()
		{
	    pointerPosition = Camera.main.WorldToScreenPoint(this.transform.position);
		pointerPosition.x = Mathf.Clamp(pointerPosition.x, 0.0f, Screen.width);
		pointerPosition.y = Mathf.Clamp(pointerPosition.y, 0.0f, Screen.height);
		pointerImage.transform.position = pointerPosition;
	}*/
	
	/*private void OnMouseDown()
		{
		for(int i = 0; i < quest.Length; i++)
		{
		if(quest[i].GetComponent<Quest>().ProgressQuest == false)
		{
		_historyGame.quest = quest[i];
		_historyGame.NameQuest.text = quest[i].GetComponent<Quest>().nameQuest;
		_historyGame.DescText.text = quest[i].GetComponent<Quest>().descQuest;
		questPanel.SetActive(true);
		//quest[i].GetComponent<Text>().text = DinamicQuestPanel.transform.GetChild(0).GetComponent<Quest>().nameQuest;
		}
		else
		{
		
		}
		}
		for(int i = 0; i < quest.Length; i++)
		{
		if(quest[i])
		{
		if(quest[i].GetComponent<Quest>().ProgressQuest == false)
		{
		GameObject Player = GameObject.FindWithTag("Player");
		quest[i].GetComponent<Quest>().Player = Player;
		quest[i].GetComponent<Quest>().CheckStatusQuest();
		}
		}
		break;
		}
	}*/
	
	private void OnMouseDown()
	{
		if(!touchNPc)
		{
			questPanel.SetActive(true);
	        ChangeToPanelAvailableQuest();
		    changeToPanelComlyQuest();
			touchNPc = true;	
		}
	}
	
	private void changeToPanelComlyQuest()
	{
		for(int i = 0; i < _historyGame.activeQuests.Length; i++)
		{
			if(_historyGame.complyQuest[i])
			{
				if(_historyGame.complyQuest[i].GetComponent<Quest>()._statusQuest == statusQuest.EndProgress)
				{
					GameObject obj = Instantiate(_historyGame.complyQuest[i]);
					obj.transform.SetParent(questPanel.transform.GetChild(3));
					obj.transform.localScale = Vector3.one;
					obj.transform.position = obj.transform.parent.position;
				}
			}	
		}
	}
	
	public void passivePanel()
	{
		questPanel.gameObject.SetActive(false);
		touchNPc = false;
		for(int i = 0; i < AvailableQuest.Length; i++)
		{
	        if(questPanel.transform.GetChild(2).childCount > 0)
			{
				Destroy(questPanel.transform.GetChild(2).GetChild(i).gameObject);
			}
			if(questPanel.transform.GetChild(3).childCount > 0)
			{
				Destroy(questPanel.transform.GetChild(3).GetChild(i).gameObject);
			}
		}
	}
	
	private void ChangeToPanelAvailableQuest()
	{
		for(int i = 0; i < AvailableQuest.Length; i++)
		{
			if(AvailableQuest[i])
			{
				if(AvailableQuest[i].GetComponent<Quest>()._statusQuest == statusQuest.NoStartProgress)
				{
					GameObject obj = Instantiate(AvailableQuest[i]);
					obj.transform.SetParent(questPanel.transform.GetChild(2));
					obj.transform.localScale = Vector3.one;
					obj.transform.position = obj.transform.parent.position;
				}
				}
				//break;
		}
	}
}
