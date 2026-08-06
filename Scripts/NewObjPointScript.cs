using System.Collections;
using TMPro;
using UnityEngine;

public class NewObjPointScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartCoroutine());
        //FaceCamera3DText.AddNewObject(gameObject);
    }

    //private void OnEnable()
    //{
    //    FaceCamera3DText.AddNewObject(gameObject);
    //}
    //private void OnDisable()
    //{
    //    FaceCamera3DText.DeleteObject(gameObject);
    //}
    private void OnDestroy()
    {
        FaceCamera3DText.DeleteObject(gameObject);
    }
    IEnumerator StartCoroutine()
    {
        for (; ;)
        {
            yield return null;
            try
            {
                FaceCamera3DText.AddNewObject(gameObject);
                break;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(gameObject.name + " " + ex);
            }
        }
        //yield return null;
        //FaceCamera3DText.AddNewObject(gameObject);
    }
}
