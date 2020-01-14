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
		var targetPosition = mainCamera.transform.position;
		targetPosition.y = transform.position.y;
		transform.LookAt(mainCamera.transform);
	}
}
