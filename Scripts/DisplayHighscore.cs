using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscore : MonoBehaviour
{
    public Text[] highscoreName;
    public Text[] highscoreScore;
    Highscore[] highscoreList;

    private void Start()
    {
        StartCoroutine(DownloadHighscore());
    }

    IEnumerator DownloadHighscore()
    {
        WWW www = new WWW("http://clickmebabyhedspi.tk/AndroidCode/AccountData.php");
        yield return www;
        Debug.Log(www.text);
        FormatHighscore(www.text);
        StartCoroutine(PrintHighscore(highscoreList));
    }

    public void FormatHighscore(string textStream)
    {
        string[] entries = textStream.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        for(int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            string score = entryInfo[1];
            highscoreList[i] = new Highscore(username, score);
        }
    }

    IEnumerator PrintHighscore(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreList.Length; i++)
        {
            highscoreName[i].text = i + 1 + ". Fletching....";
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < highscoreList.Length; i++)
        {
            Debug.Log(highscoreList[i].username + ": " + highscoreList[i].score);
            highscoreName[i].text = i + 1 + ". ";
            highscoreName[i].text += highscoreList[i].username;
            highscoreScore[i].text = highscoreList[i].score;
        }
    }
}

public struct Highscore
{
    public string username;
    public string score;

    public Highscore(string _username, string _score)
    {
        username = _username;
        score = _score;
    }
}