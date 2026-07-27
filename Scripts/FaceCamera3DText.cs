using System.Collections.Generic;
using UnityEngine;

public class FaceCamera3DText : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private List<GameObject> faces = new List<GameObject>();
    [SerializeField] private float minTextSize=0.02f;
    [SerializeField] private float maxTextSize=0.7f;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        TextRotation();
    }
    private void TextRotation()
    {
        foreach (var item in faces)
        {
            Vector3 direction = targetCamera.transform.position - item.transform.position;
            Quaternion rot = Quaternion.LookRotation(direction * -1, Vector3.up);
            item.transform.rotation = rot;
            float i = AdvancedOrbitCamera.GetNormalizedDistance();
            i = Mathf.Lerp(minTextSize, maxTextSize, i);

            item.transform.localScale =new Vector3 (i,i,i);
        }
    }
}
