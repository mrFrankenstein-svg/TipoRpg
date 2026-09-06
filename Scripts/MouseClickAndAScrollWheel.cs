using UnityEngine;
using System;

public class MouseClickAndAScrollWheel : MonoBehaviour
{
    [Header("Скрипт отслеживающий кнопки мыши и колёсико.\n")]

    public static Action OnLMBClick;
    public static Action OnLMBHold;
    public static Action OnDoubleLMBClick;
    public static Action OnRMBClick;
    public static Action OnRMBHold;
    public static Action OnWheelRoll;
    
    // Для двойного клика
    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f; // время в секундах для двойного клика

    void Start()
    {
        
    }
    void HandleDoubleClickLMBOrOneClickLMB()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Это двойной клик
                OnDoubleLMBClick?.Invoke();
                lastClickTime = 0f; // сбрасываем, чтобы не было тройных срабатываний подряд
                return;
            }
            else
                lastClickTime = Time.time;
                OnLMBClick?.Invoke();
        }
    }
    void HandleLMBHolding()
    {
        if (Input.GetMouseButton(0)) // зажата левая кнопка
        {
            OnLMBHold?.Invoke();
        }
    }
    void HandleRMBHolding()
    {
        if (Input.GetMouseButton(1)) // зажата правая кнопка
        {
            OnRMBHold?.Invoke();
        }
    }
    void WheelRooling()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        { 
            OnWheelRoll?.Invoke();
        }
    }
    void Update()
    {
        HandleDoubleClickLMBOrOneClickLMB();
        HandleLMBHolding();
        HandleRMBHolding();
        WheelRooling();
    }
}
