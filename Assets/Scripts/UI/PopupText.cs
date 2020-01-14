using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupText : MonoBehaviour 
{
	public  GameObject PopupTextObj;
    public Transform worldCanvas;
	
	public void instancePopupText(Vector3 direct, string text, int damage, Color _color, char c)
	{
		
		GameObject obj = Instantiate(PopupTextObj) as GameObject;
		worldCanvas.transform.GetChild(1).position = direct + new Vector3(0,4,0);
		obj.transform.SetParent(worldCanvas.transform.GetChild(1).transform);
		obj.transform.position = direct + new Vector3(0,3,0);
		
		obj.GetComponent<Text>().color = _color;
		
		obj.GetComponent<Text>().text = text + c + damage.ToString();
		//_scrollRect.verticalNormalizedPosition = 0f;
		Debug.Log(obj.transform.position);
	}
}
