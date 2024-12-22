using UnityEngine;

[System.Serializable]
public class ObjectInfo : MonoBehaviour
{
    [Header("Object Info")]
    public string objectName = "Default Name"; // Ім'я об'єкта
    [TextArea]
    public string description = "Default Description"; // Опис об'єкта
    public GameObject modelPrefab; // Префаб моделі для показу

    [Header("Camera Settings")]
    public Vector3 cameraFocusPosition = new Vector3(0, 2, -5); // Позиція камери для огляду
    public Vector3 cameraFocusRotation = new Vector3(10, 0, 0); // Орієнтація камери для огляду (в градусах)
}
