using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour {

    private string[] player;
    public GameObject[] Player;

	// Use this for initialization
	IEnumerator Start () {
        WWW www = new WWW("http://localhost/acount/AccountData.php");
        yield return www;
        string playerDataString = www.text;
        player = playerDataString.Split(';');
    }
	
    string GetData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    public void BackOnClick()
    {
        SceneManager.LoadScene("Menu");
    }
	// Update is called once per frame
	void Update () {

	}
}
