using UnityEngine;
using System.Collections;

public class QuestPanel : MonoBehaviour 
{	
	public void ActivePanel()
	{
		FindUIStatic.instance.QuestPanel.SetActive(true);
	}
	
	public void CloseUI()
	{
	    FindUIStatic.instance.QuestPanel.SetActive(false);
	}
}
