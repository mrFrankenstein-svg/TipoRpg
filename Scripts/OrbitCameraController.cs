using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Camera))]
public class AdvancedOrbitCamera : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float zoomSpeed = 5f;

    [Header("Дистанция")]
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 20f;

    [Header("Ограничения вращения")]
    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

    [SerializeField] private Camera cam;
    private Transform target;
    private Transform theObjectBeingFollowed;
    [SerializeField]private float normalizedDistance=0.1f;

    // Для двойного клика
    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f; // время в секундах для двойного клика

    // Орбитальные углы
    private float xRotation;
    private float yRotation;

    // Чтобы зум работал корректно с орбитой, нужно хранить дистанцию отдельно
    private float currentDistance;

    private static AdvancedOrbitCamera thisScript;

    void Start()
    {
        thisScript = this;

        if (cam == null)
            cam = Camera.main;

        theObjectBeingFollowed = new GameObject("TheObjectThatTheCameraFollows").transform;
        theObjectBeingFollowed.transform.position = new Vector3(0, 0, 0);

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Начальные углы (можно менять)
        xRotation = 45f;
        yRotation = 0f;
    }

    void Update()
    {
        if (IsPointerOverUI())
            return; // если курсор над UI — ничего не делаем

        HandleDoubleClick();
        HandleMovement();
        HandleRotation();
        HandleZoom();

        UpdateOrbitPosition();
    }

    /// <summary>
    /// Двойной клик ЛКМ = выбор объекта
    /// </summary>
    void HandleDoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Это двойной клик
                SelectTarget();
                lastClickTime = 0f; // сбрасываем, чтобы не было тройных срабатываний подряд
                return;
            }

            lastClickTime = Time.time;
        }
    }

    void SelectTarget()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            target = hit.transform;
            theObjectBeingFollowed.transform.position = target.position;
            // Опционально: можно сразу поставить дистанцию до объекта
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
    }

    /// <summary>
    /// Зажатая ЛКМ = движение камеры по горизонтали
    /// </summary>
    void HandleMovement()
    {
        if (Input.GetMouseButton(0)) // зажата левая кнопка
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");

            // Двигаем по плоскости XZ, игнорируя Y (чтобы не «летать» вверх/вниз)
            Vector3 moveDir = new Vector3(moveX, 0f, moveY).normalized;
            if (moveDir.magnitude >= 0.01f)
            {
                theObjectBeingFollowed.transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    /// <summary>
    /// Зажатая ПКМ = вращение вокруг цели
    /// </summary>
    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // зажата правая кнопка
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        }
    }

    /// <summary>
    /// Колёсико = зум
    /// </summary>
    void HandleZoom()
    {
        // Сначала обрабатываем зум для орбиты
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float delta = scroll * zoomSpeed * 10f;
            currentDistance -= delta;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            normalizedDistance= Mathf.InverseLerp(minDistance, maxDistance, currentDistance);

        }
    }

    //float GetCurrentDistance()
    //{
    //    if (target == null) return 0f;
    //    return Vector3.Distance(transform.position, target.position);
    //}

    void UpdateOrbitPosition()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 offset = rotation * Vector3.back * currentDistance;
        cam.transform.position = theObjectBeingFollowed.position + offset;
        cam.transform.LookAt(theObjectBeingFollowed.position);
    }

    bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;
        return EventSystem.current.IsPointerOverGameObject();
    }
    public static float GetNormalizedDistance()
    {
        return thisScript.normalizedDistance;
    }
}