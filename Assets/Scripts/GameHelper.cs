using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHelper : MonoBehaviour 
{
	public GameObject[] EnemyObj;
	public GameObject[] BossObj;
	
	public Transform[] EnemySpawn;
	public Transform[] BossSpawn;
	
	
	private bool yesEnemyAllSpawn;
	private bool yesBossSpawn;
	
	public GameObject SpawnParticle;
	
	//public GameObject ContentPanel;
	public GameObject PopupTextObj;
	
	public int countSpawn = 4;
	
	//public ScrollRect _scrollRect;
	
	void Start()
	{
		InvokeRepeating("monsterInstanceStartGame", 2, 30);
		InvokeRepeating("bossInstanceStartGame", 2, 30);
		
	}
	
	private void monsterInstanceStartGame()
	{
		for(int i = 0; i < EnemySpawn.Length; i++)
		{
			string enemyObjType = EnemyObj[i].GetComponent<EnemyAttributes>()._typeMonster.ToString();
			if(EnemySpawn[i].childCount == 0)
		    {   
			    if(enemyObjType == "Common")
			    {
				    GameObject enemy = Instantiate(EnemyObj[i], EnemySpawn[i].position, EnemySpawn[i].rotation) as GameObject;
			        enemy.GetComponent<EnemyMotion>().EnemySpawn = EnemySpawn[i];
			        enemy.transform.SetParent(EnemySpawn[i].transform);
			        enemy.transform.position = EnemySpawn[i].position;
					startSpawnParticle(EnemySpawn[i]);
					countSpawn++;
		        }
			}
		}
	}
	
	private void startSpawnParticle(Transform enemySpawn)
	{
		GameObject obj = Instantiate(SpawnParticle) as GameObject;
		obj.transform.position = enemySpawn.transform.position;
		obj.transform.position = enemySpawn.transform.position + new Vector3(0,1,0);
		obj.transform.SetParent(enemySpawn);
		Destroy(obj, 1.0f);
	}
	
	private void bossInstanceStartGame()
	{
		for(int i = 0; i < BossSpawn.Length; i++)
		{
			//string EnemyBossType = BossObj[i].GetComponent<EnemyAttributes>()._typeMonster.ToString();
			if(BossSpawn[0].childCount == 0)
		    {
			    GameObject bossObj = Instantiate(BossObj[i],BossSpawn[0].position, BossSpawn[0].rotation) as GameObject;
		        bossObj.GetComponent<EnemyMotion>().EnemySpawn = BossSpawn[0];
		        bossObj.transform.position = BossSpawn[0].position;
		        bossObj.transform.SetParent(BossSpawn[0]);
				//startSpawnParticle(BossSpawn[0]);
		    }
		}
	}
}