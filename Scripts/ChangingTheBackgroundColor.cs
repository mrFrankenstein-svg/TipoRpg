using TMPro;
using UnityEngine;
using System.Linq;

public class ChangingTheBackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshProUGUI textField;

    public void SetColor()
    {
        if (mainCamera == null)
        {
            Debug.LogError(ErorsList.OBJECT_FIND_EROR + mainCamera);
            return;
        }
        if (textField == null)
        {
            Debug.LogError(ErorsList.OBJECT_FIND_EROR + textField);
            return;
        }


        Color newColor;
        string hex = new string(textField.text.Where(c => c == '#' || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')).ToArray());


        if (ColorUtility.TryParseHtmlString(hex, out newColor))
        {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = Color.black;
            mainCamera.backgroundColor = newColor;
        }
        else
        {
            Debug.LogError(ErorsList.INCORRECT_FORMAT + textField.text);
            //Debug.LogWarning($"Неверный формат цвета: '{textField.text}'. Используйте, например, #FF0000");
        }
    }
}
