using UnityEngine;
using System.Collections;

public class EnemyDropItems : MonoBehaviour 
{
    public GameObject lootObj;
	public GameObject[] itemsObj;
	public float[] dropRate;
	public float _dropChange;
	public float[] random;
	public int[] randomCount;
	
	private void dropChange(GameObject obj)
	{
	    for(int i = 0; i < itemsObj.Length; i++)
		{
		    random[i] = Random.Range(0.1f,_dropChange);
			if(random[i] <= _dropChange && random[i] <= dropRate[i])
			{
				if(obj.GetComponent<ItemObject>().itemsObj[i] == null)
				{
					obj.GetComponent<ItemObject>().itemsObj[i] = itemsObj[i];
					if(itemsObj[i].GetComponent<Item>()._classItems == classItems.AnotherItems)
					{
						randomCount[i] = Random.Range(1, 3);
						obj.GetComponent<ItemObject>().itemCount[i] = randomCount[i];
					}
					else if(itemsObj[i].GetComponent<Item>()._classItems != classItems.AnotherItems)
					{
						Debug.Log(i);
						obj.GetComponent<ItemObject>().itemCount[i] = 1;
						
					}
				}
			}
			
		}
	}
	
	public void instanceItem()
	{
		GameObject _lootObj = Instantiate(lootObj) as GameObject;
		_lootObj.transform.position = transform.position + new Vector3(0, 1.5f, 0);
		dropChange(_lootObj);
		Destroy(_lootObj, 100.0f);
	}
}
