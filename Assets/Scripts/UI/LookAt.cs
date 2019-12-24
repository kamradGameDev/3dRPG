using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
	private GameObject mainCamera;
	
	void Start()
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	void Update()
	{
		transform.LookAt(mainCamera.transform);
	}
}
