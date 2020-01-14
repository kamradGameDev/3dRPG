using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetQuest : MonoBehaviour 
{
    public Transform target;
    public RectTransform marker;
	
	public Sprite arrowSprite;
	public Sprite upDownSprite;
	public Sprite targetSprite;
	
	public float minSize = 25;
	public float maxSize = 50;
	
	private Camera camera;
	private Vector3 newPos;
	private float upDown;
	
	void Awake()
	{
		camera = Camera.main;
	}
	
	private bool Behind(Vector3 point)
	{
		bool result = false;
		Vector3 forward = camera.transform.TransformDirection(Vector3.forward);
		Vector3 toOther = point - camera.transform.position;
		if(Vector3.Dot(forward, toOther) < 0)
		{
			result = true;
		}
		return result;
	}
	
	void LateUpdate()
	{
		Vector3 position = camera.WorldToScreenPoint(target.position);
		Rect rect = new Rect(0,0,Screen.width, Screen.height);
		newPos = position;
		upDown = 1;
		
		if(!Behind(target.position))
		{
			if(rect.Contains(position))
			{
				marker.GetComponent<Image>().sprite = targetSprite;
			}
			else
			{
				marker.GetComponent<Image>().sprite = arrowSprite;
			}
		}
		else
		{
			position = -position;
			if(camera.transform.position.y > target.position.y)
			{
				newPos = new Vector3(position.x,0,0);
			}
			else
			{
				upDown = -1;
				newPos = new Vector3(position.x,  Screen.height, 0);
			}
			marker.GetComponent<Image>().sprite = upDownSprite;
		}
		
		float size = marker.sizeDelta.x / 2;
		newPos.x = Mathf.Clamp(newPos.x, size, Screen.width - size);
		newPos.y = Mathf.Clamp(newPos.y, size, Screen.height - size);

		// находим угол вращения к цели
		Vector3 pos = position - newPos;
		float angle  = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		marker.rotation = Quaternion.AngleAxis(angle * upDown, Vector3.forward);

		// изменение размера, относительно расставания
		float dis = Vector3.Distance(camera.transform.position, target.position);
		float scale = maxSize - dis / 4;
		scale = Mathf.Clamp(scale, minSize, maxSize);
		marker.sizeDelta = new Vector2(scale, marker.sizeDelta.y);

		marker.anchoredPosition = newPos;
	}
}
