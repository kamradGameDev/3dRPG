using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeEnemyStatsPanel : MonoBehaviour 
{
	public GameObject enemyStatsPanel;
	
	void Start()
	{
		enemyStatsPanel = GameObject.Find("EnemyStatsToPanel");
	}
	
	public void CloseUI()
	{
		enemyStatsPanel.GetComponent<Animator>().SetTrigger("Start");
		
	}
}
