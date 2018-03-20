using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;


public class Login : MonoBehaviour {

    public InputField Username;
    public InputField Password;
    private string username;
    private string password;

    private string LoginURL = "www.clickmebabyhedspi.tk/Login.php";
    
    // Use this for initialization
    void Start () {
		
	}
    
    public void BackMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }


    // Update is called once per frame
    void Update () {

    }

    public void LoginButton()
    {
        username = Username.text;
        password = Password.text;
        StartCoroutine(LoginToDB(username, password));
        Debug.Log("hello");
    }

    IEnumerator LoginToDB (string Username, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("passwordPost", Password);

        WWW www = new WWW(LoginURL, form);
        yield return www;
        if (www.text == "Login success")
        {
           // PlayerPrefs.SetString("username", Username);
            SceneManager.LoadScene("Menu");
        }
    }
}
