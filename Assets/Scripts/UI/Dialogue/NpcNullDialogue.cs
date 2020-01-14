using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcNullDialogue : MonoBehaviour 
{
    public GameObject uiPanel;
	public Transform npcTarget;
	public string[] randomTextNpc;
	public float time = 20.0f;
	
	private void Start()
	{
	    npcTarget = this.gameObject.transform.GetChild(0);
		uiPanel = GameObject.Find("WorldCanvas");
	}
	
	private void OnMouseDown()
	{
		uiPanel.transform.GetChild(0).position = npcTarget.transform.position + new Vector3(0,1,1);
		//uiPanel.transform.SetParent(npcTarget.transform);
		uiPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = randomTextNpc[Random.Range(0, randomTextNpc.Length)];
		
		Invoke("ClosePanel", time);
	}
	
	public void ClosePanel()
	{
	    uiPanel.transform.GetChild(0).position  = new Vector3(0f, 1000f, 0f);
	}
}
