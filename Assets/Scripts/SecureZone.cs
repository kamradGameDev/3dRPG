using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecureZone : MonoBehaviour 
{
	/*public GameObject TextSecureZone;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Player")
		{
			TextSecureZone.SetActive(true);
			TextSecureZone.GetComponent<Text>().color = Color.green;
			TextSecureZone.GetComponent<Text>().text = "Зашли в безопасную зону, помощь от охранников максимальная!";
			Invoke("TimeTextPassive", 5.0f);
			//PlayerIsSecureZone = true;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		TextSecureZone.SetActive(true);
		TextSecureZone.GetComponent<Text>().color = Color.red;
        TextSecureZone.GetComponent<Text>().text = "Вышли из безопасной зоны но будьте осторожны так как чем дальше от лагеря тем меньше шансов получить помощь.";		
		Invoke("TimeTextPassive", 5.0f);
		PlayerIsSecureZone = false;
	}
	
	private void TimeTextPassive()
	{
		//TextSecureZone.SetActive(false);
	}*/
}
