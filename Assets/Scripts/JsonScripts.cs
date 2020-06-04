using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GetJsonList();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void GetJsonList()
	{
		TextAsset ass = Resources.Load("AllCard") as TextAsset;
		JsonData data = JsonMapper.ToObject(ass.text);

		JsonData list = data["cardList"];
		for (int i = 0; i < list.Count; i++)
		{
			int id = (int)list[i]["id"];
			string name = (string)list[i]["name"];
			string des = (string)list[i]["des"];
			//Debug.Log(id);
			//Debug.Log(name);
			//Debug.Log(des);
			
		}


	}
}
