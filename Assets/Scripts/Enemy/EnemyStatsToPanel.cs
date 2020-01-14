using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsToPanel : MonoBehaviour 
{
	public GameObject[] textStats = new GameObject[4];
	public GameObject enemyStatsPanel;
	
	void Start()
	{
		enemyStatsPanel = GameObject.Find("EnemyStatsToPanel");
	}
	
	void OnMouseDown()
	{
		enemyStatsPanel.GetComponent<Animator>().SetTrigger("DescActive");
		//textStats[1] = enemyStatsPanel.transform.GetChild(1).GetChild(0).gameObject;
		//textStats[1].GetComponent<Text>().text = "Name: " + this.gameObject.GetComponent<EnemyAttributes>().nameMonster;
		
		textStats[2] = enemyStatsPanel.transform.GetChild(2).GetChild(0).gameObject;
		textStats[2].GetComponent<Text>().text = "Range Monster: " + this.gameObject.GetComponent<EnemyAttributes>()._rangeMonster.ToString();
		
		textStats[3] = enemyStatsPanel.transform.GetChild(3).GetChild(0).gameObject;
		textStats[3].GetComponent<Text>().text = "Type Monster: " + this.gameObject.GetComponent<EnemyAttributes>()._typeMonster.ToString();
		
		textStats[4] = enemyStatsPanel.transform.GetChild(4).GetChild(0).gameObject;
		//textStats[4].GetComponent<Text>().text = "Class Monster: " + this.gameObject.GetComponent<EnemyAttributes>()._classMonster.ToString();
		
		textStats[5] = enemyStatsPanel.transform.GetChild(5).GetChild(0).gameObject;
		textStats[5].GetComponent<Text>().text = "Level: " + this.gameObject.GetComponent<EnemyAttributes>().monsterLevel;
		
		textStats[6] = enemyStatsPanel.transform.GetChild(6).GetChild(0).gameObject;
		textStats[6].GetComponent<Text>().text = "MaxHP: " + this.gameObject.GetComponent<EnemyAttributes>().MaxHealth;
		
		textStats[7] = enemyStatsPanel.transform.GetChild(7).GetChild(0).gameObject;
		textStats[7].GetComponent<Text>().text = "Damage: " + this.gameObject.GetComponent<EnemyAttributes>().EnemyDamage;
		
		textStats[8] = enemyStatsPanel.transform.GetChild(8).GetChild(0).gameObject;
		textStats[8].GetComponent<Text>().text = "Distance Attack: " + this.gameObject.GetComponent<EnemyAttributes>().distAttack;
		
		
	}
}
