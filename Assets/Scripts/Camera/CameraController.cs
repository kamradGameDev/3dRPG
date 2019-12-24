using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float smooth = 5.0f;
    public Vector3 offset = new Vector3(0, 2, -5);
	private Transform thisTransform;
	
	private void Start()
	{
		thisTransform = transform;
		Invoke("findTaget", 0.2f);
	}
	
	private void findTaget()
	{
	    target = GameObject.FindGameObjectWithTag("Player");
	}
	
    private void Update ()
	{
		if(target)
		{
			thisTransform.position = Vector3.Lerp (thisTransform.position, target.transform.position + offset, Time.deltaTime * smooth);
		}
	}
}