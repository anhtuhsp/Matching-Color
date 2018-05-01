using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Failed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void backButtonLoginOnClick()
    {
        Debug.Log("?");
        SceneManager.LoadScene("LogIn");
    }

    public void backButtonRegisterOnClick()
    {
        SceneManager.LoadScene("Register");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
