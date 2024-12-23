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
    public GameObject cameraTarget; // ��'���, ������� ����� ���� ��������������� ������
    public Vector3 cameraRotation = new Vector3(10, 0, 0); // �������� ������
}
