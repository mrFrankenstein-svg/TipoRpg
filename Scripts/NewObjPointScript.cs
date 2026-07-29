using System.Collections;
using TMPro;
using UnityEngine;

public class NewObjPointScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartCoroutine());
    }
    private void OnDestroy()
    {
        FaceCamera3DText.DeleteObject(gameObject);
    }
    IEnumerator StartCoroutine()
    {
        yield return null;
        FaceCamera3DText.AddNewObject(gameObject);
    }
}
