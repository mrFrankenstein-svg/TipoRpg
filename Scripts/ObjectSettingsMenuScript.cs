using UnityEngine;
using UnityEngine.UI;
using static SelectingTheObjectUnderTheCursor;

public class ObjectSettingsMenuScript : MonoBehaviour
{
    [SerializeField] private bool itUsedNow=false;

    private void OnEnable()
    {
        MouseClickAndAScrollWheel.OnLMBClick += СursorPositionRecognition;
    }
    private void OnDisable()
    {
        MouseClickAndAScrollWheel.OnLMBClick -= СursorPositionRecognition;
    }

    private void СursorPositionRecognition()
    {
        if (itUsedNow==true)
        {
            SelectObjectResult hitResult = SelectingTheObjectUnderTheCursor.SelectObject();
            if (hitResult.isHit==true)
            {
                if (hitResult.hit.transform.name == KeyNamesOfObjects.gearWheelName)
                {
                    var button = hitResult.hit.transform.GetComponent<Button>();
                    if (button != null)
                        button.onClick.Invoke();
                }
            }
        }
    }
    

}
