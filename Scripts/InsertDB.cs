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

    ChangeScene change = new ChangeScene();
    string CreateUserURL = "http://clickmebabyhedspi.tk/AndroidCode/InsertAccount.php";
    // Use this for initialization
    void Start () {
		
	}
	
    public void backButtonOnClick()
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
        if (inputUsername != "" && inputConfirm == inputPassword)
        {
            WWWForm form = new WWWForm();
            form.AddField("usernamePOST", username);
            form.AddField("passwordPOST", password);

            WWW www = new WWW(CreateUserURL, form);
            yield return www;
            if (www.text == "Sucess")
            {
                change.LogIn();
            }
            else SceneManager.LoadScene("RegisterFail");
        }
        else SceneManager.LoadScene("RegisterFail");
    }

    public void createOnClick()
    {
        if (inputConfirm != inputPassword)
        {
            SceneManager.LoadScene("RegisterFail");
        }
        else
        {
            StartCoroutine(CreateUser(inputUsername, inputPassword));
        }
    }

}
