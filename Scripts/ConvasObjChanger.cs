using System;
using System.Collections.Generic;
using UnityEngine;

public class ConvasObjChanger : MonoBehaviour
{
    [Header("этот скрипт должен менять экраны (Canvas) с кнопками на экране.\n" +
        "Крч это менеджер конвасов и все конвасы должны работать через него.\n")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainTab;

    private List<GameObject> openedObjList = new List<GameObject>();

    private void Start()
    {
        openedObjList.Add(mainTab);

        if (canvas == null | mainTab == null)
        {
            throw new NullReferenceException("canvas или mainTab не назначен");
        }
    }
    public void OpenNewObj(GameObject newObj)
    {
        openedObjList[openedObjList.Count - 1].gameObject.SetActive(false);
        openedObjList.Add(newObj);
        newObj.SetActive(true);
    }
    public void OpenLastObj()
    {
        openedObjList[openedObjList.Count - 1].gameObject.SetActive(false);
        openedObjList.Remove(openedObjList[openedObjList.Count - 1]);
        openedObjList[openedObjList.Count - 1].gameObject.SetActive(true);
        //obj.SetActive(false);
        //lastObjList[lastObjList.Count - 1].gameObject.SetActive(true);
    }
    public void CloseAllAndOpenMainTab() 
    {
        for (int i = openedObjList.Count-1; i > 1; i--)
        {
            openedObjList[i].SetActive(false);
            openedObjList.Remove(openedObjList[i]);
        }
        openedObjList[openedObjList.Count - 1].gameObject.SetActive(true);

        //foreach (var item in openedObjList)
        //{
        //    item.SetActive(false);
        //}
        //openedObjList.Clear();

    }
}
