using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTarget : MonoBehaviour 
{
	public GameObject target, explosion;
	public float speed = 5.0f;
	private bool contactEnemy = false;
	
	public float damage;
	public float skillDamage;
	public Color _color;
	public bool crit = false;
	
	PopupText _popupText;
	
	private void Start()
	{
		_popupText = GameObject.FindObjectOfType<PopupText>();
		//Player = GameObject.FindWithTag("Player");
	}
	
	
	private void Update()
	{
		if(target.transform.GetChild(2))
		{
			transform.position = Vector3.Lerp(transform.position, target.transform.GetChild(2).position, Time.deltaTime * speed);
			//transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			//transform.LookAt(target.transform.position);
			transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z));
			Vector3 diff = target.transform.GetChild(2).position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < 1f * 1f)
			{
				if(!contactEnemy)
				{
					Invoke("attackEnemy", 0.001f);
					GameObject obj = Instantiate(explosion);
					obj.transform.position = this.transform.position;
					Destroy(obj, 0.5f);
					Destroy(gameObject, 0.001f);
					contactEnemy = true;
				}
			}
		}
		else if(target.transform.GetChild(2) == null)
		{
		    Destroy(gameObject);
		}
	}
	
	public void attackEnemy()
	{
		char c = '-';
		float _damage = damage + skillDamage;
		if(target.gameObject.tag == "Player")
		{
			target.GetComponent<PlayerAttributes>().PlayerHealth -= _damage;
		    //target.GetComponent<EnemyMotion>().playerAttackEnemy = true;
		    if(crit)
		    {
			    _popupText.instancePopupText(target.transform.position, "Crit: ", (int)_damage, _color, c);
			}
		    else
		    {
				_popupText.instancePopupText(target.transform.position, "DMG: ", (int)_damage, _color, c);
			}
		}
		else
		{
			target.GetComponent<EnemyAttributes>().Health -= _damage;
			
		    target.GetComponent<EnemyMotion>().playerAttackEnemy = true;
			if(crit)
			{
				_popupText.instancePopupText(target.transform.position, "Crit: ", (int)_damage, _color, c);
			}
			else
			{
				_popupText.instancePopupText(target.transform.position, "DMG: ", (int)_damage, _color, c);
			}
		}
	}
}
