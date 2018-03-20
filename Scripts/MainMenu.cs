using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Start()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }   

    public void HighScoreOnClick()
    {
        SceneManager.LoadScene("HighScore");
    }


    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void LogIn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
