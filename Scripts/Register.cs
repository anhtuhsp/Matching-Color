using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{

    public GameObject username;
    public GameObject password;
    public GameObject confirm;
    private string Username;
    private string Password;
    private string Confirm;
    private string form;

    // Use this for initialization
    void Start()
    {

    }

    public void RegisterButton()
    {
        bool UN = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(@"E:/UnityTestFolder/" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username taken.");
            }
        }
        else
        {
            Debug.LogWarning("Username Field is Empty.");
        }

        if (Password != "")
        {
            if (Password.Length > 5 && Password.Length < 10)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password must between 6-10 characters long");
            }
        }
        else
        {
            Debug.LogWarning("Password Field is Empty");
        }

        if (Confirm != "")
        {
            if (Confirm == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Confirm does not match.");
            }
        }
        else
        {
            Debug.LogWarning("Confirm Field is Empty.");
        }

        if(UN == true && PW == true && CPW == true)
        {
            form = (Username + Environment.NewLine + Password);
            System.IO.File.WriteAllText(@"E:/UnityTestFolder/" + Username + ".txt", form);
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confirm.GetComponent<InputField>().text = "";
            print("Registration Complete.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confirm.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Username != "")
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        Confirm = confirm.GetComponent<InputField>().text;
    }
}
