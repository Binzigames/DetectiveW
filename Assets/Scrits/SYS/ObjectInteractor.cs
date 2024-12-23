using UnityEngine;
using TMPro;

public class ObjectInteractor : MonoBehaviour
{
    [Header("Materials")]
    public Material highlightMaterial; // Матеріал для підсвічування
    public Material defaultMaterial;   // Звичайний матеріал

    [Header("UI Elements")]
    public GameObject infoPanel;       // Панель інформації
    public TMP_Text descriptionText;   // TMP для опису
    public TMP_Text nameText;          // TMP для імені об'єкта
    public Transform modelView;        // Місце для показу моделі
    public GameObject returnButton;    // Кнопка повернення

    [Header("Camera Settings")]
    public Transform cameraTransform;   // Камера, що використовується
    public float moveSpeed = 5f;        // Швидкість руху камери
    public float rotationSpeed = 5f;    // Швидкість обертання камери
    public Vector3 defaultCameraPosition; // Стандартна позиція камери
    public Vector3 defaultCameraRotation; // Стандартна орієнтація камери (градуси)

    private GameObject selectedObject;  // Поточний об'єкт
    private Vector3 targetCameraPosition; // Цільова позиція камери
    private Quaternion targetCameraRotation; // Цільова орієнтація камери
    private bool isMovingToObject = false; // Статус руху камери
    private bool isRotating = false; // Статус обертання камери

    void Start()
    {
        // Встановлюємо початкову позицію камери як стандартну
        if (cameraTransform != null)
        {
            defaultCameraPosition = cameraTransform.position;
            defaultCameraRotation = cameraTransform.rotation.eulerAngles;
        }

        // Ховаємо UI елементи
        infoPanel.SetActive(false);
        returnButton.SetActive(false);
    }

    void Update()
    {
        // Перевірка на підсвічування об'єкта
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != selectedObject)
            {
                ClearHighlight();
                HighlightObject(hitObject);
            }

            if (Input.GetMouseButtonDown(0))
            {
                ShowInfo(hitObject);
            }
        }
        else
        {
            ClearHighlight();
        }

        // Рух камери
        if (isMovingToObject)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, Time.deltaTime * moveSpeed);

            // Плавний поворот камери до об'єкта
            if (isRotating)
            {
                cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetCameraRotation, rotationSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(cameraTransform.position, targetCameraPosition) < 0.1f)
            {
                cameraTransform.position = targetCameraPosition;
                isMovingToObject = false;
            }
        }
    }

    // Підсвічування об'єкта
    void HighlightObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null && highlightMaterial != null)
        {
            renderer.material = highlightMaterial;
            selectedObject = obj;
        }
    }

    // Зняття підсвічування
    void ClearHighlight()
    {
        if (selectedObject != null)
        {
            Renderer renderer = selectedObject.GetComponent<Renderer>();
            if (renderer != null && defaultMaterial != null)
            {
                renderer.material = defaultMaterial;
            }
            selectedObject = null;
        }
    }

    // Показ інформації про об'єкт
    void ShowInfo(GameObject obj)
    {
        ObjectInfo info = obj.GetComponent<ObjectInfo>();
        if (info == null) return;

        selectedObject = obj;

        // Перевіряємо, чи є у об'єкта камера-таргет
        if (info.cameraTarget != null)
        {
            // Встановлюємо цільову позицію камери, враховуючи позицію таргету
            targetCameraPosition = info.cameraTarget.transform.position;
            targetCameraRotation = Quaternion.Euler(info.cameraRotation);
            isRotating = true;
            isMovingToObject = true;
        }
        else
        {
            // Якщо камера-таргет не вказано, використовуємо стандартну позицію
            targetCameraPosition = obj.transform.position + new Vector3(0, 2, -5); // Приклад відстані
            targetCameraRotation = Quaternion.Euler(info.cameraRotation);
            isRotating = true;
            isMovingToObject = true;
        }

        // Показуємо інформацію
        infoPanel.SetActive(true);
        nameText.text = info.objectName;
        descriptionText.text = info.description;

        // Очищаємо модель огляду
        foreach (Transform child in modelView)
        {
            Destroy(child.gameObject);
        }

        returnButton.SetActive(true);
    }

    // Повернення до стандартної позиції
    public void ReturnToDefaultPosition()
    {
        // Встановлюємо цільову позицію камери як стандартну
        targetCameraPosition = defaultCameraPosition;
        targetCameraRotation = Quaternion.Euler(defaultCameraRotation);
        isRotating = false;
        isMovingToObject = true;

        // Ховаємо UI елементи
        infoPanel.SetActive(false);
        returnButton.SetActive(false);

        ClearHighlight();
    }
}
