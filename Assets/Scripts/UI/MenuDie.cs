using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuDie : MonoBehaviour 
{
	public GameObject PlayerDiePanel;
	
	void Awake()
	{
		PlayerDiePanel.SetActive(false);
	}
	
	public void menuActive()
	{
		PlayerDiePanel.SetActive(true);
		SpawnCharacterPlayer.instance.player.GetComponent<MotionAndroid>().moveSpeed = 0f;
	}
	
	public void RiceAgain()
	{
		SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().PlayerHealth = SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().MaxPlayerHealth / 2;
		SpawnCharacterPlayer.instance.player.GetComponent<PlayerAttributes>().riceAgain = true;
		SpawnCharacterPlayer.instance.player.GetComponent<Animator>().Play("Idle");
		SpawnCharacterPlayer.instance.player.GetComponent<MotionAndroid>().moveSpeed = 12f;
		PlayerDiePanel.SetActive(false);
	}
}