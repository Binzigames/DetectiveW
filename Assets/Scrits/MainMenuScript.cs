using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{   
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

    //Для паскуди яка виходе з гри
    public void onExitCliked()
    {
        Application.Quit();
    }
}
