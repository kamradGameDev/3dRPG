using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System;
using System.IO;

public class LoadGame : MonoBehaviour
{
    public GameObject LoadText, ButttonLoad, SliderLoad;
    public Slider loadSliderBar;
    public GameObject[] characterChangeObj;
    public GameObject[] objCharacter;
    public string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/" + "Characters.xml";
        FileSaveCreate();

        XmlTextReader reader = new XmlTextReader(filePath);

        while (reader.Read())
        {
            if (reader.Name == "Warrior")
            {
                for (int i = 0; i < objCharacter.Length; i++)
                {
                    if (objCharacter[i].name == "Warrior")
                    {
                        CreateCharactersToPanel(objCharacter[i]);
                        //break;
                    }
                }
            }
            if (reader.Name == "Archer")
            {
                for (int i = 0; i < objCharacter.Length; i++)
                {
                    if (objCharacter[i].name == "Archer")
                    {
                        CreateCharactersToPanel(objCharacter[i]);
                        //break;
                    }
                }
            }
            if (reader.Name == "Mage")
            {
                for (int i = 0; i < objCharacter.Length; i++)
                {
                    if (objCharacter[i].name == "Mage")
                    {
                        CreateCharactersToPanel(objCharacter[i]);
                        //break;
                    }
                }
            }
        }
        reader.Close();
    }

    private void CreateCharactersToPanel(GameObject character)
    {
        for (int i = 0; i < characterChangeObj.Length; i++)
        {
            if (characterChangeObj[i].transform.childCount != 0)
            {
                if (characterChangeObj[i].transform.GetChild(0) != null)
                {
                    if (character.name != characterChangeObj[i].transform.GetChild(0).gameObject.name.Replace("(Clone)", ""))
                    {
                        //break;
                    }
                }
            }

            else
            {
                GameObject obj = Instantiate(character) as GameObject;
                obj.transform.SetParent(characterChangeObj[i].transform);
                obj.transform.localScale = Vector3.one;
                obj.transform.position = characterChangeObj[i].transform.position;
                break;
            }
        }
    }

    public void FileSaveCreate()
    {
        if (File.Exists(filePath))
        {
        }
        else if (!File.Exists(filePath))
        {
            using(FileStream fs = File.Create(filePath));
        }

        
		string[] nullDoc = File.ReadAllLines(filePath);
        if (nullDoc.Length == 0)
        {
		    XmlDocument xmlDoc = new XmlDocument();
            XmlNode element = xmlDoc.CreateElement("Characters");
            xmlDoc.AppendChild(element);
            xmlDoc.Save(filePath);
        }
    }

    public void LoadScene(int SceneIndex)
    {
        StartCoroutine(LoadAsync(SceneIndex));
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        ButttonLoad.SetActive(false);
        LoadText.SetActive(true);
        SliderLoad.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSliderBar.value = progress;
            yield return null;
        }
    }
}