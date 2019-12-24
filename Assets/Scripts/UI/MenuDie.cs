using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuDie : MonoBehaviour 
{
	public GameObject PlayerDiePanel;
	
	public void menuActive()
	{
		PlayerDiePanel.SetActive(true);
	}
	
	public void RiceAgain()
	{
		//_playerAttributes.PlayerHealth = _playerAttributes.MaxPlayerHealth;
		PlayerDiePanel.SetActive(false);
		SpawnCharacterPlayer.instance.PlayerObj.transform.position = SpawnCharacterPlayer.instance.PlayerSpawn.transform.position;
	}
}
