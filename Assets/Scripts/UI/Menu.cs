using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public GameObject MenuPanel;
	public void PreviousInGame()
	{
		MenuPanel.SetActive(false);
		Time.timeScale = 1f;
	}
	public void SetQuitInMenu()
	{
		SceneManager.LoadScene("Menu");
	}
	
	public void SetActivePanel()
	{
		MenuPanel.SetActive(true);
		Time.timeScale = 0f;
	}
}
