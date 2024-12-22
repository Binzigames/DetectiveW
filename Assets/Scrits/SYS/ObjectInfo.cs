using UnityEngine;

[System.Serializable]
public class ObjectInfo : MonoBehaviour
{
    [Header("Object Info")]
    public string objectName = "Default Name"; // ��'� ��'����
    [TextArea]
    public string description = "Default Description"; // ���� ��'����
    public GameObject modelPrefab; // ������ ����� ��� ������

    [Header("Camera Settings")]
    public Vector3 cameraFocusPosition = new Vector3(0, 2, -5); // ������� ������ ��� ������
    public Vector3 cameraFocusRotation = new Vector3(10, 0, 0); // �������� ������ ��� ������ (� ��������)
}
