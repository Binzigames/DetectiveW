using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WineChoice : MonoBehaviour
{
    public Button showChoiceButton; // ������, ��� ���� ����������/�������� �������
    public Button choice1Button;
    public Button choice2Button;
    public Button choice3Button;
    public GameObject choicePanel; // ������, �� ������ ������� ������
    public GameObject deathMenu;
    public string correctChoiceSceneName;  // ����� �����, ��� ����� ����������� ��� ����������� �����
    public int correctChoice; // ���������� ���� (����� ����������� � ���������)

    private bool isChoicesVisible = false;

    private void Start()
    {
        showChoiceButton.onClick.AddListener(ToggleChoices); // ����'���� ������ �� ������� ����������� ��������
        choice1Button.onClick.AddListener(() => CheckChoice(1));
        choice2Button.onClick.AddListener(() => CheckChoice(2));
        choice3Button.onClick.AddListener(() => CheckChoice(3));
    }

    // ������� ��� ����������� �������� ������� ������
    void ToggleChoices()
    {
        isChoicesVisible = !isChoicesVisible; // ���� ����� ��������
        choicePanel.SetActive(isChoicesVisible); // ������������ �������� �����
    }

    void CheckChoice(int choice)
    {
        if (choice == correctChoice) // ���� ���� ����������
        {
            SceneManager.LoadScene(correctChoiceSceneName);  // ����������� ��������� �����
        }
        else
        {
            ShowDeathMenu();
        }
    }

    void ShowDeathMenu()
    {
        deathMenu.SetActive(true);  // �������� ���� �����
        Invoke("RestartScene", 2f);  // ��������� ���������� ����� ����� 2 �������
    }

    void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name; // �������� ������� ����� �����
        SceneManager.LoadScene(currentSceneName);  // ������������� ������� �����
    }
}
