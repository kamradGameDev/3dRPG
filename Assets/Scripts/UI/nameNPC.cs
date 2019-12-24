using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameNPC : MonoBehaviour 
{
    public GameObject[] npcObj;
	public GameObject[] nameObj;
	public GameObject[] nameEnemyObj;
	public GameObject[] enemyObj;
	public GameObject nameNpc;
	public GameObject canvas;
	
	private void Start()
	{
	    npcObj = GameObject.FindGameObjectsWithTag("NPC");
		canvas = GameObject.Find("WorldCanvas");
		changeNameNpc();
	}
	
	private void changeNameNpc()
	{
	    for(int i = 0; i < npcObj.Length; i++)
		{
			if(npcObj != null)
			{
			    GameObject obj = Instantiate(nameNpc);
				obj.transform.GetChild(0).GetComponent<Text>().text = npcObj[i].name;
				obj.transform.GetChild(0).GetComponent<Text>().color = Color.green;
				obj.transform.SetParent(canvas.transform);
				obj.transform.position = npcObj[i].transform.position + new Vector3(0, 5.0f, 0);
				nameObj[i] = obj;
			}
		}
	}
	
	private void Update()
	{
	    for(int i = 0; i < nameObj.Length; i++)
		{
		    if(nameObj[i] != null)
			{
				if(nameObj[i].transform.position != npcObj[i].transform.position)
				{
					nameObj[i].transform.position = npcObj[i].transform.position + new Vector3(0, 5.0f, 0);
				}
			}
		}
		
		for(int i = 0; i < nameEnemyObj.Length; i++)
		{
		    if(enemyObj[i] != null)
			{
			    if(nameEnemyObj[i] != null)
				{
					if(enemyObj[i].GetComponent<EnemyAttributes>()._typeMonster == typeMonster.Common)
					{
						if(nameEnemyObj[i].transform.position != enemyObj[i].transform.position)
						{
							nameEnemyObj[i].transform.position = enemyObj[i].transform.position + new Vector3(1,4.3f,0);
						}
					}
					else
					{
					    if(nameEnemyObj[i].transform.position != enemyObj[i].transform.position)
						{
							nameEnemyObj[i].transform.position = enemyObj[i].transform.position + new Vector3(0,6,0);
						}
					}
				}
			}
		}
	}
	
	public void changeNameEnemy(GameObject _enemyObj, string name, int level)
	{
		for(int i = 0; i < 100; i++)
		{
		    if(enemyObj[i] == null)
			{
			    GameObject obj = Instantiate(nameNpc);
				obj.transform.GetChild(0).GetComponent<Text>().text = name;
				obj.transform.GetChild(0).GetComponent<Text>().color = Color.red;
				obj.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = level.ToString();
				obj.transform.SetParent(canvas.transform);
				nameEnemyObj[i] = obj;
				enemyObj[i] = _enemyObj;
				break;
			}
		}
	}
}
