using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class InsertDB : MonoBehaviour {

    public GameObject UsernameField;
    public GameObject PasswordField;
    public GameObject ConfirmField;
    private string inputUsername;
    private string inputPassword;
    private string inputConfirm;
    private string inform;

    string CreateUserURL = "http://localhost/acount/InsertAccount.php";
    // Use this for initialization
    void Start () {
		
	}
	
    public void BackButton()
    {
        SceneManager.LoadScene("LogIn");
    }

	// Update is called once per frame
	void Update () {
        inputUsername = UsernameField.GetComponent<InputField>().text;
        inputPassword = PasswordField.GetComponent<InputField>().text;
        inputConfirm = ConfirmField.GetComponent<InputField>().text;
    }

    IEnumerator CreateUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePOST", username);
        form.AddField("passwordPOST", password);

        WWW www = new WWW(CreateUserURL, form);
        yield return www;
        if(www.text == "Sucess")
        {
            SceneManager.LoadScene("LogIn");
        }
        else
        {
            Debug.Log("This Username has been taken.");
        }
    }

    public void CreateOnClick()
    {
        if (inputConfirm != inputPassword)
        {
            Debug.Log("Password does not match.");
        }
        else
        {
            StartCoroutine(CreateUser(inputUsername, inputPassword));
        }
    }

}
