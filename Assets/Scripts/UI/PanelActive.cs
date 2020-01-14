using UnityEngine;
using System.Collections;

public class PanelActive : MonoBehaviour 
{
	public void ActivePan()
	{
		FindUIStatic.instance.PlayerAttributesPanel.SetActive(true);
	}
	
	public void PassivePan()
	{
		FindUIStatic.instance.PlayerAttributesPanel.SetActive(false);
	}
	
	public void ActiveInven()
	{
	    FindUIStatic.instance.InvenPanel.SetActive(true);
	}
	
	public void PassiveInven()
	{
	    FindUIStatic.instance.InvenPanel.SetActive(false);
	}
	
	public void ActiveDropPanel()
	{
	    FindUIStatic.instance.dropPanel.SetActive(true);
	    FindUIStatic.instance.invenDescPanel.SetActive(false);
	}
	
	public void PassiveDropPanel()
	{
	    FindUIStatic.instance.dropPanel.SetActive(false);
	} 
	
}
