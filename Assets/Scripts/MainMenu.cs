using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        //{
        //    SceneManager.LoadScene("SampleScene");
        //}
        //else
        //{
           SceneManager.LoadScene("HouseScene");
        //}
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
