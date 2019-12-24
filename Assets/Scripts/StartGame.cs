using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
	public GameObject StartGamePanel;
	public GameObject StartGameText;
	
	void Start()
	{
		StartGamePanel.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void OnMouse()
	{
		StartGamePanel.SetActive(false);
		StartGameText.SetActive(false);
		Time.timeScale = 1;
	}
}
