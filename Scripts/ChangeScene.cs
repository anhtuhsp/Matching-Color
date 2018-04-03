using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class ChangeScene : MonoBehaviour {


    public void BackToMenu()
    {
        var wind = new WindTransition()
        {
            nextScene = 0,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

    public void ToRegister()
    {
        var wind = new WindTransition()
        {
            nextScene = 4,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

    public void StartGame()
    {
        var slices = new VerticalSlicesTransition()
        {
            nextScene = 2,
            divisions = Random.Range(3, 20)
        };
        TransitionKit.instance.transitionWithDelegate(slices);
    }

    public void Start()
    {

    }

    public void PlayGame()
    {
        var wind = new WindTransition()
        {
            nextScene = 1,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

    public void HighScoreOnClick()
    {
        var wind = new WindTransition()
        {
            nextScene = 5,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

    public void Option()
    {
        var wind = new WindTransition()
        {
            nextScene = 6,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void LogIn()
    {
        var wind = new WindTransition()
        {
            nextScene = 3,
            duration = 0.5f,
            size = 0.3f
        };
        TransitionKit.instance.transitionWithDelegate(wind);
    }

}

