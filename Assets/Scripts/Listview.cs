using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Listview : MonoBehaviour
{
    [SerializeField]
    private int cellCount;           //计划生成的List数量

    [SerializeField]
    public RectTransform ListPrefabRT;  //List预设 的 RectTransform

    [SerializeField]
    public float paddingBottom = 10;      //上边界宽度

    [SerializeField]
    public float paddingDown = 10;     //下边界宽度

    [SerializeField]
    public float spacingY = 10;         //Y向间距

   

    private ScrollRect scrollRect;      //ScrollRect
    private RectTransform contentRT;    //Content 的 RectTransform
    private RectTransform viewPortRT;   //viewPort 的 RectTransform

    private List<GameObject> lists;     //所有List列表     
    private float contentWidth;         //Content的总宽度
    private float pivotOffsetY;         //由list的pivot决定的起始偏移值

    JsonData jonlist, data;

    private void Awake()
    {
        TextAsset ass = Resources.Load("AllCard") as TextAsset;
        data = JsonMapper.ToObject(ass.text);

        jonlist = data["cardList"];
        lists = new List<GameObject>();

        //依赖的组件
        scrollRect = GetComponent<ScrollRect>();
        contentRT = scrollRect.content;
        scrollRect.horizontal = false;
        viewPortRT = scrollRect.viewport;
        cellCount = jonlist.Count;
        //计算和设置Content总宽度
        //当listsCount小于等于0时，Content总宽度 = 0
        //当listsCount大于0时，Content总宽度 = 上边界间隙 + 所有Cell的高度总和 + 相邻间距总和 + 下边界间隙
        contentWidth = cellCount <= 0 ? 0 : paddingBottom + ListPrefabRT.rect.height * cellCount + spacingY * (cellCount - 1) + paddingDown;
        contentRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentWidth);
        //contentRT.SetSizeWithCurrentAnchors(contentWidth, RectTransform.Axis.Horizontal);

        //计算由lists的pivot决定的起始偏移值
        pivotOffsetY = ListPrefabRT.pivot.y * ListPrefabRT.rect.height;
    }

    private void Start()
    {
        CreateCells();
        LayoutCells();
    }
    
    private void CreateCells()
    {
        for (int i = 0; i < cellCount; i++)
        {
            GameObject list = GameObject.Instantiate<GameObject>(ListPrefabRT.gameObject);
            RectTransform cellRT = list.GetComponent<RectTransform>();
            cellRT.SetParent(contentRT, false);
            //强制设置lists的anchor
            cellRT.anchorMin = new Vector2(cellRT.anchorMin.x, 0);
            cellRT.anchorMax = new Vector2(cellRT.anchorMax.x, 0);
            lists.Add(list);
            int id = (int)jonlist[i]["id"];
            string name = (string)jonlist[i]["name"];
            string des = (string)jonlist[i]["des"];
            //Debug.Log(id);
            //Debug.Log(name);
            //Debug.Log(des);
            //idtext.text = id.ToString();
            //nametext.text = name.ToString();
            //destext.text = des.ToString();
            cellRT.GetComponent<TextScripts>().SetIndex(id.ToString());
            cellRT.GetComponent<TextScripts>().SetName(name.ToString());
            cellRT.GetComponent<TextScripts>().SetDes(des.ToString());
        }
    }

    private void LayoutCells()
    {
        //OnPointerDown(PointerEventData eventData);
        for (int index = 0; index < lists.Count; index++)
        {
            GameObject list = lists[lists.Count - index - 1];
            //计算和设置list的位置
            //Y = 左边界间隙 + 由Cell的pivot决定的起始偏移值 + 前面已有Cell的宽度总和 + 前面已有的间距总和
            float y = paddingBottom + pivotOffsetY + ListPrefabRT.rect.height * index + spacingY * index;
            RectTransform ListRT = list.GetComponent<RectTransform>();
            ListRT.anchoredPosition = new Vector2(ListRT.anchoredPosition.x, y);
        }
    }

    

}
