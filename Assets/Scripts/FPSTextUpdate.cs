using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSTextUpdate : MonoBehaviour 
{
	private Text FPSText;
	
	private void Start()
	{
		FPSText = this.gameObject.GetComponent<Text>();
		StartCoroutine(FPSUpdate());
	}
	
	private IEnumerator FPSUpdate()
	{
		while(true)
		{
			float fps = 1f/Time.deltaTime;
		    FPSText.text = "FPS: " + Math.Round(fps).ToString();
			yield return new WaitForSeconds(1);
		}
		
	}
}
