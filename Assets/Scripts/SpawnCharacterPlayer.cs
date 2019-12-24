using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnCharacterPlayer : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject[] charactersObj;
    public GameObject PlayerSpawn;
	
	public static SpawnCharacterPlayer instance;
	
	void Awake()
	{
		if(!instance)
		{
			instance = this;
		}
	}

    void Start()
    {
	    Time.timeScale = 1.0f;
        string s = PlayerPrefs.GetString("CharacterBufer");

        for (int i = 0; i < charactersObj.Length; i++)
        {
            if (s == charactersObj[i].name)
            {
                PlayerObj = charactersObj[i];
				FindUIStatic.instance.player = PlayerObj;
            }
        }
        PlayerSpawn = GameObject.FindWithTag("PlayerSpawn");

        string LevelGame = SceneManager.GetActiveScene().name;

        if (LevelGame == "Main")
        {
            playerSpawn();
        }
        else
        {
        }
    }

    private void playerSpawn()
    {
        Instantiate(PlayerObj, PlayerSpawn.transform.position, Quaternion.identity);
        //player.transform.position = PlayerSpawn.transform.position;
    }
}