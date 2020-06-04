using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextScripts : MonoBehaviour, IPointerDownHandler
{
    public Text idtext;
    public Text nametext;
    public Text destext;
    public Text downText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
    }
    public void SetIndex(string index)
    {
        idtext.text = index.ToString();
    }
    public void SetName(string index)
    {
        nametext.text = index.ToString();
    }

    public void SetDes(string index)
    {
        destext.text = index.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        downText = this.transform.Find("nameText").GetComponent<Text>();
        string id = downText.text;
        //Debug.Log(id);
        TextAsset ass = Resources.Load("AllCard") as TextAsset;
        JsonData data = JsonMapper.ToObject(ass.text);

        JsonData list = data["cardList"];
        for (int i = 0; i < list.Count; i++)
        {
            if (id == (string)list[i]["name"]) {
                Debug.Log(list[i]["id"]);
                Debug.Log(list[i]["name"]);
                Debug.Log(list[i]["des"]);
                Debug.Log(list[i]["isEquip"]);
                Debug.Log(list[i]["color"]);
                Debug.Log(list[i]["file"]);
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
