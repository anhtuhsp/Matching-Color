using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class MainMenu : MonoBehaviour {

    public Text username;

    public void Start()
    {
        username.text = PlayerPrefs.GetString("UserName");
    }

    public void Awake()
    {
       
    }

    public void playButtonOnClick()
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

    public void highscoreButtonOnClick()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void optionsButtonOnClick()
    {
        SceneManager.LoadScene("Option");
    }

    public void quitButtonOnClick()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void loginButtonOnClick()
    {
        SceneManager.LoadScene("LogIn");
    }

}
