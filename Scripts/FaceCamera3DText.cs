using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FaceCamera3DText : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private List<GameObject> faces = new List<GameObject>();
    [SerializeField] private float minTextSize=0.02f;
    [SerializeField] private float maxTextSize=0.7f;
    private static FaceCamera3DText thisScript;

    //public static event Action<GameObject> AddNewObjectEvent;
    //public static event Action<GameObject> DeleteObjectEvent;
    //private void OnEnable()
    //{
    //    AddNewObjectEvent += AddNewObject;
    //    DeleteObjectEvent += DeleteObject;
    //}
    //private void OnDisable()
    //{
    //    AddNewObjectEvent -= AddNewObject;
    //    DeleteObjectEvent -= DeleteObject;
    //}

    void Start()
    {
        thisScript = this;
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        TextRotation();
    }
    private void TextRotation()
    {
        if (faces.Count != 0)
        {
            foreach (var item in faces)
            {
                Vector3 direction = targetCamera.transform.position - item.transform.position;
                Quaternion rot = Quaternion.LookRotation(direction * -1, Vector3.up);
                item.transform.rotation = rot;
                float i = AdvancedOrbitCamera.GetNormalizedDistance();
                i = Mathf.Lerp(minTextSize, maxTextSize, i);

                item.transform.localScale = new Vector3(i, i, i);
            }
        }
    }
    public static void AddNewObject(GameObject obj)
    {
        GameObject tmps = obj.GetComponentInChildren<TMP_Text>().gameObject;
        thisScript.faces.Add(tmps);

    }
    public static void DeleteObject(GameObject obj)
    {
        GameObject tmps = obj.GetComponentInChildren<TMP_Text>().gameObject;
        thisScript.faces.Remove(tmps);
    }
}
