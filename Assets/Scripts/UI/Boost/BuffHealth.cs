using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffHealth : MonoBehaviour 
{
	public int plusHealth = 10;
	PlayerAttributes _playerAttributes;
	activeBoost _activeBoost;
	public GameObject activeBoostObj;
	
	public Image img;
	public bool isStatus = false;
	public float waitTime = 2.0f;
	
	private void Start()
	{
		_playerAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
		_activeBoost = GameObject.FindObjectOfType<activeBoost>();
		img = transform.GetChild(0).GetComponent<Image>();
	}
	
	void Update()
	{
		if(isStatus)
		{
			img.fillAmount += 0.2f / waitTime * Time.deltaTime;
			if(img.fillAmount == 1.0f)
			{
				isStatus = false;
				Destroy(GameObject.Find("HealthBoost(Clone)"));
				_playerAttributes.MaxPlayerHealth -= plusHealth;
			}
		}
	}
	
	public void startBoost()
	{
		if(!isStatus)
		{
			isStatus = true;
			img.fillAmount = 0f;
			for(int i = 0; i < _activeBoost.boosts.Length; i++)
			{
				if(_activeBoost.boosts[i].transform.childCount == 0)
				{
					GameObject obj = Instantiate(activeBoostObj);
				    obj.transform.SetParent(_activeBoost.boosts[i].transform);
					obj.transform.localScale = Vector3.one;
					obj.transform.position = obj.transform.parent.position;
					break;
				}
			}
			_playerAttributes.MaxPlayerHealth += plusHealth;
		}
	}
}
