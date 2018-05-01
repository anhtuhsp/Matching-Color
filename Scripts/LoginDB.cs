using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class LoginDB : MonoBehaviour {

    public GameObject Username;
    public GameObject Password;
    private string username;
    private string password;
    private string inform;
    string CreateURL = "http://clickmebabyhedspi.tk/AndroidCode/Login.php";

    ChangeScene change = new ChangeScene();
    // Use this for initialization
    void Start () {
		
	}
	
    public void loginButtonOnClick()
    {
        StartCoroutine(LoginToDB(username, password));
    }

    public void backButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void registerButtonOnClick()
    {
        SceneManager.LoadScene("Register");
    }
	// Update is called once per frame

	void Update () {
        username = Username.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;
    }

    IEnumerator LoginToDB(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePOST", username);
        form.AddField("passwordPOST", password);
        WWW www = new WWW(CreateURL, form);
        yield return www;
        if (www.text != "Failed")
        {
            string textStream = www.text;
            string[] entries = textStream.Split(new char[] { '|' });
            int score = int.Parse(entries[1]);
            PlayerPrefs.SetString("UserName", entries[0]);
            PlayerPrefs.SetInt("HighScore", score);
            change.BackToMenu(); 
        }
        else
        {
            SceneManager.LoadScene("LoginFail");
        }
    }
}
