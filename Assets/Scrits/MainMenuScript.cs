using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{   
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject optionCanvas;
    [SerializeField] private GameObject creditsCanvas;


    //Після натискання Тето Груші, це перекидає на сцену з Тетою
    public void onPearClicked()
    {
        SceneManager.LoadScene(1);
    }


    //Кнопка яка перекидає назад у меню(ВОНО ЛИШЕ ПОВИННО ПРАЦЮВАТИ В СЦЕНІ З ТЕТО ГРУШОЮ)
    public void onBackClikedInTetoScene()
    {
        SceneManager.LoadScene(0);
    }

    public void onBackClicked()
    {
        if(optionCanvas.activeSelf == true)
        {
            mainMenuButtons.SetActive(true);
            optionCanvas.SetActive(false);
        }
        else if(creditsCanvas.activeSelf == true)
        {
            mainMenuButtons.SetActive(true);
            creditsCanvas.SetActive(false);
        }
    }
    
    public void onOptionsClicked()
    {
        mainMenuButtons.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void onCreditsClicked()
    {
        mainMenuButtons.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    //Для паскуди яка виходе з гри
    public void onExitCliked()
    {
        Application.Quit();
    }
}
