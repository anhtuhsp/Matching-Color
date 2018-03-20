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
    string CreateURL = "http://localhost/acount/Login.php";

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoginOnClick()
    {
        StartCoroutine(LoginToDB(username, password));
        if(inform == "Login sucess")
        {
            PlayerPrefs.SetString("Username_now", username);
            SceneManager.LoadScene("Menu");
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToRegister()
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
        inform = www.text;
    }
}
