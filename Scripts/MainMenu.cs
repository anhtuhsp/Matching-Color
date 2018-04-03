using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class MainMenu : MonoBehaviour {

    public void Start()
    {

    }

    public void PlayGame()
    {
        var ripple = new RippleTransition()
        {
            nextScene = 5,
            duration = 10.0f,
            amplitude = 1500f,
            speed = 20f
        };
        TransitionKit.instance.transitionWithDelegate(ripple);
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
