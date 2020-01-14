using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUIStatic : MonoBehaviour 
{
    public static FindUIStatic instance;
	public GameObject player;
	public GameObject lootPanel;
	public GameObject invenDescPanel;
	public GameObject QuestPanel;
	public GameObject PlayerAttributesPanel;
	public GameObject InvenPanel;
	public GameObject dropPanel;
	public GameObject DescItem;
	
	private void Awake()
	{
	    if(!instance)
		{
		    instance = this;
		}
	}
}
