using UnityEngine;
using TMPro;

public class ObjectInteractor : MonoBehaviour
{
    [Header("Materials")]
    public Material highlightMaterial; // ������� ��� �����������
    public Material defaultMaterial;   // ��������� �������

    [Header("UI Elements")]
    public GameObject infoPanel;       // ������ ����������
    public TMP_Text descriptionText;   // TMP ��� �����
    public TMP_Text nameText;          // TMP ��� ���� ��'����
    public Transform modelView;        // ̳��� ��� ������ �����
    public GameObject returnButton;    // ������ ����������

    [Header("Camera Settings")]
    public Transform cameraTransform;   // ������, �� ���������������
    public float moveSpeed = 5f;        // �������� ���� ������
    public float rotationSpeed = 5f;    // �������� ��������� ������
    public Vector3 defaultCameraPosition; // ���������� ������� ������
    public Vector3 defaultCameraRotation; // ���������� �������� ������ (�������)

    private GameObject selectedObject;  // �������� ��'���
    private Vector3 targetCameraPosition; // ֳ����� ������� ������
    private Quaternion targetCameraRotation; // ֳ����� �������� ������
    private bool isMovingToObject = false; // ������ ���� ������
    private bool isRotating = false; // ������ ��������� ������

    void Start()
    {
        // ������������ ��������� ������� ������ �� ����������
        if (cameraTransform != null)
        {
            defaultCameraPosition = cameraTransform.position;
            defaultCameraRotation = cameraTransform.rotation.eulerAngles;
        }

        // ������ UI ��������
        infoPanel.SetActive(false);
        returnButton.SetActive(false);
    }

    void Update()
    {
        // �������� �� ����������� ��'����
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

        // ��� ������
        if (isMovingToObject)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, Time.deltaTime * moveSpeed);

            // ������� ������� ������ �� ��'����
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

    // ϳ���������� ��'����
    void HighlightObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null && highlightMaterial != null)
        {
            renderer.material = highlightMaterial;
            selectedObject = obj;
        }
    }

    // ������ �����������
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

    // ����� ���������� ��� ��'���
    void ShowInfo(GameObject obj)
    {
        ObjectInfo info = obj.GetComponent<ObjectInfo>();
        if (info == null) return;

        selectedObject = obj;

        // ����������, �� � � ��'���� ������-������
        if (info.cameraTarget != null)
        {
            // ������������ ������� ������� ������, ���������� ������� �������
            targetCameraPosition = info.cameraTarget.transform.position;
            targetCameraRotation = Quaternion.Euler(info.cameraRotation);
            isRotating = true;
            isMovingToObject = true;
        }
        else
        {
            // ���� ������-������ �� �������, ������������� ���������� �������
            targetCameraPosition = obj.transform.position + new Vector3(0, 2, -5); // ������� ������
            targetCameraRotation = Quaternion.Euler(info.cameraRotation);
            isRotating = true;
            isMovingToObject = true;
        }

        // �������� ����������
        infoPanel.SetActive(true);
        nameText.text = info.objectName;
        descriptionText.text = info.description;

        // ������� ������ ������
        foreach (Transform child in modelView)
        {
            Destroy(child.gameObject);
        }

        returnButton.SetActive(true);
    }

    // ���������� �� ���������� �������
    public void ReturnToDefaultPosition()
    {
        // ������������ ������� ������� ������ �� ����������
        targetCameraPosition = defaultCameraPosition;
        targetCameraRotation = Quaternion.Euler(defaultCameraRotation);
        isRotating = false;
        isMovingToObject = true;

        // ������ UI ��������
        infoPanel.SetActive(false);
        returnButton.SetActive(false);

        ClearHighlight();
    }
}
