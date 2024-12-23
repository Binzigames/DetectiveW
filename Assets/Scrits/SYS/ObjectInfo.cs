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
    public GameObject cameraTarget; // Об'єкт, відносно якого буде позиціонуватись камера
    public Vector3 cameraRotation = new Vector3(10, 0, 0); // Орієнтація камери
}
