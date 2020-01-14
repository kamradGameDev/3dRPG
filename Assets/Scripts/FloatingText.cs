using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour 
{
	Animator anim;
	public Text popupText;
	//private Transform camera;
	
	void Start()
	{
		//camera = GameObject.FindWithTag("MainCamera").transform;
		anim = GetComponent<Animator>();
		AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
	    popupText = anim.GetComponent<Text>();
		Destroy(gameObject, clipInfo[0].clip.length);
	}

    /*void LateUpdate()
	{
		transform.LookAt(camera.transform.position);
		transform.Rotate(30, 180, 0);
	}*/		
	
	public void SetText(string text)
	{
		popupText.text = text;
	}
	
}
