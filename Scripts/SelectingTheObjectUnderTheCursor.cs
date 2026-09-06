using UnityEngine;

public class SelectingTheObjectUnderTheCursor : MonoBehaviour
{
    public struct SelectObjectResult
    {
        public bool isHit;
        public RaycastHit hit;
    }
    public static SelectObjectResult SelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var result = new SelectObjectResult { isHit = true, hit = hit };
            return result;
        }
        else
        {
            var result = new SelectObjectResult { isHit = false, hit = default }; // hit тут не важен
            return result;
        }
    }    
}
