using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WineChoice : MonoBehaviour
{
    public Button showChoiceButton; // Кнопка, яка буде показувати/скривати варіанти
    public Button choice1Button;
    public Button choice2Button;
    public Button choice3Button;
    public GameObject choicePanel; // Панель, що містить варіанти вибору
    public GameObject deathMenu;
    public string correctChoiceSceneName;  // Назва сцени, яку треба завантажити при правильному виборі
    public int correctChoice; // Правильний вибір (можна налаштувати в інспекторі)

    private bool isChoicesVisible = false;

    private void Start()
    {
        showChoiceButton.onClick.AddListener(ToggleChoices); // Прив'язка кнопки до функції перемикання видимості
        choice1Button.onClick.AddListener(() => CheckChoice(1));
        choice2Button.onClick.AddListener(() => CheckChoice(2));
        choice3Button.onClick.AddListener(() => CheckChoice(3));
    }

    // Функція для перемикання видимості варіантів вибору
    void ToggleChoices()
    {
        isChoicesVisible = !isChoicesVisible; // Зміна стану видимості
        choicePanel.SetActive(isChoicesVisible); // Встановлення видимості панелі
    }

    void CheckChoice(int choice)
    {
        if (choice == correctChoice) // Якщо вибір правильний
        {
            SceneManager.LoadScene(correctChoiceSceneName);  // Завантажити правильну сцену
        }
        else
        {
            ShowDeathMenu();
        }
    }

    void ShowDeathMenu()
    {
        deathMenu.SetActive(true);  // Показуємо меню смерті
        Invoke("RestartScene", 2f);  // Викликаємо перезапуск сцени через 2 секунди
    }

    void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name; // Отримуємо поточну назву сцени
        SceneManager.LoadScene(currentSceneName);  // Перезапускаємо поточну сцену
    }
}
