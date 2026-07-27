using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvasObjChanger : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private List<GameObject> lastObjList = new List<GameObject>();
    public void AddToLastObj(GameObject obj)
    {
        lastObjList.Add(obj);
        obj.SetActive(false);
    }
    public void OpenNewObj(GameObject newObj)
    {
        newObj.SetActive(true);
    }
    public void OpenLastObj(GameObject obj)
    {
        obj.SetActive(false);
        lastObjList[lastObjList.Count - 1].gameObject.SetActive(true);
    }
}
