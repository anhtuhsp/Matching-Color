using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum GameState
{
    Playing,
    GameOver,
    Pause
}

public class GameManager : MonoBehaviour
{
    
    public GameObject GamePausePanel;
    public GameObject GameOverText;
    public Text GameOverScoreText;
    public GameObject GameOverPanel;
    public GameState State;
    public float ClickInterval = 0.5f;
    public float LastClickTime;
    public float nextClick = 0f;

    public bool CanClick
    {
        get
        {
            return (Time.time - LastClickTime) > ClickInterval;
        }
    }

    private Tile[,] AllTiles = new Tile[5, 5];
    private List<Tile[]> colums = new List<Tile[]>();

   
    
    // Use this for initialization
    void Start()
    {
        
        Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in AllTilesOneDim)
        {

            Tweener tweener = TileScaleDown(t);
            tweener.OnComplete(() =>
            {
                t.Number = Generate();   //sinh ra mot so ngau nhien
                TileAppear(t);
            });
            
            AllTiles[t.indRow, t.indCol] = t;
            t.OnTileClicked = HandleTileClicked;
        }
        //Make Collum to use FillEmptyTile
        colums.Add(new Tile[] { AllTiles[0, 0], AllTiles[1, 0], AllTiles[2, 0], AllTiles[3, 0], AllTiles[4, 0] });
        colums.Add(new Tile[] { AllTiles[0, 1], AllTiles[1, 1], AllTiles[2, 1], AllTiles[3, 1], AllTiles[4, 1] });
        colums.Add(new Tile[] { AllTiles[0, 2], AllTiles[1, 2], AllTiles[2, 2], AllTiles[3, 2], AllTiles[4, 2] });
        colums.Add(new Tile[] { AllTiles[0, 3], AllTiles[1, 3], AllTiles[2, 3], AllTiles[3, 3], AllTiles[4, 3] });
        colums.Add(new Tile[] { AllTiles[0, 4], AllTiles[1, 4], AllTiles[2, 4], AllTiles[3, 4], AllTiles[4, 4] });


    }

    private void GameOver()
    {
        State = GameState.GameOver;
        GameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        GameOverPanel.SetActive(true);
    }

    public void GamePause()
    {
        State = GameState.Pause;
        GamePausePanel.SetActive(true);
    }

    public void GameResume()
    {
        State = GameState.Playing;
        GamePausePanel.SetActive(false);
    }

    void HandleTileClicked(int x, int y)
    {
        Debug.Log("Hit Tile " + x + "," + y + " " + CanClick);


        if (CanClick)
        {
            LastClickTime = Time.time;

            if (AllTiles[x, y].TileImage.color == new Color32(250, 250, 251, 255) && ((x < 4 &&AllTiles[x, y].TileImage.color == AllTiles[x + 1, y].TileImage.color) || (x > 0 && AllTiles[x, y].TileImage.color == AllTiles[x - 1, y].TileImage.color )|| (y < 4 && AllTiles[x, y].TileImage.color == AllTiles[x, y + 1].TileImage.color) || ( y > 0 && AllTiles[x, y].TileImage.color == AllTiles[x, y - 1].TileImage.color)))
                StartCoroutine(SpecialTileInteract(x, y));
            else
                StartCoroutine(MakeTileInteract(x, y));
        }

        if (!CanMove())
            GameOver();


    }
//Animation
    void TileScale(int x, int y)
    {
        AllTiles[x, y].TileTransForm.DOShakeScale(1f);
    }

    void TileVibrant(int x, int y)
    {
        AllTiles[x, y].TileTransForm.DOShakePosition(0.5f, 3f);
    }


    void TileScaleUp(int x, int y)
    {
        AllTiles[x, y].TileTransForm.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.5f);
    }

    
    void TileDisappear(Tile t)
    {
        t.SetEmpty();
    }

    Tweener TileScaleDown(Tile t)
    {
        return t.TileTransForm.DOScale(0f, 0f);
    }

    Tweener TileAppear(Tile t)
    {
        return t.TileTransForm.DOBlendableScaleBy(new Vector3(1f, 1f, 1f), 0.5f);
    }
    
    void TileReform(int x, int y)
    {
        AllTiles[x, y].TileTransForm.localScale = Vector3.one;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.5f);
    }
    
    Tweener TileDestroySpecial(int x, int y)
    {
        return AllTiles[x, y].TileTransForm.DOPunchScale(new Vector3(-0.5f, -0.5f, 0), 0.5f);
    }

    Tweener TileDestroy(int x, int y)
    {
       return AllTiles[x, y].TileTransForm.DOBlendableScaleBy(new Vector3(-1, 0, 0), 0.5f);
    }
//\Animation

    IEnumerator MakeTileInteract(int i, int j)
    {
        int scoreTemp = 0;
        //Interact witch upper Tile
        if (i > 0 && AllTiles[i, j].TileImage.color == AllTiles[i - 1, j].TileImage.color)
        {
            scoreTemp += AllTiles[i - 1, j].Number;
            ScoreTracker.Instance.Score += AllTiles[i - 1, j].Number; // Get Score   
            Tweener tweener = TileDestroy(i - 1, j);
            tweener.OnComplete(() =>
            {
                AllTiles[i - 1, j].isEmpty = true; //Empty the interacted Tile   
                TileReform(i - 1, j);
            });
        }
        //Interact witch under Tile
        if (i < 4 && AllTiles[i, j].TileImage.color == AllTiles[i + 1, j].TileImage.color)
        {
            scoreTemp += AllTiles[i + 1, j].Number;
            ScoreTracker.Instance.Score += AllTiles[i + 1, j].Number; // Get Score    
            Tweener tweener = TileDestroy(i + 1, j);
            tweener.OnComplete(() =>
            {
                AllTiles[i + 1, j].isEmpty = true; //Empty the interacted Tile   
                TileReform(i + 1, j);
            });
        }
        //Interact witch left Tile
        if (j > 0 && AllTiles[i, j].TileImage.color == AllTiles[i, j - 1].TileImage.color)
        {
            scoreTemp += AllTiles[i, j - 1].Number;
            ScoreTracker.Instance.Score += AllTiles[i, j - 1].Number; // Get Score   
            Tweener tweener = TileDestroy(i, j - 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i, j - 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i, j - 1);
            });
        }
        //Interact witch right Tile
        if (j < 4 && AllTiles[i, j].TileImage.color == AllTiles[i, j + 1].TileImage.color)
        {
            scoreTemp += AllTiles[i, j + 1].Number;
            ScoreTracker.Instance.Score += AllTiles[i, j + 1].Number; // Get Score
            Tweener tweener = TileDestroy(i, j + 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i, j +1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i, j + 1);
            });
        }
        if (scoreTemp > 0)
        {
            TileScaleUp(i, j);
            AllTiles[i, j].Number += scoreTemp;
        }
        else
            TileVibrant(i, j);

        yield return new WaitForSeconds(0.5f);

        FillTheEmptyTile();
    }

    //SPECIAL EFFECT
    IEnumerator SpecialTileInteract(int i, int j)
    {
        //Midlle
        if(i >= 0 && i <= 4 && j >=0 && j <=4)
        {
            ScoreTracker.Instance.Score += AllTiles[i, j].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i, j);
            tweener.OnComplete(() =>
            {
                AllTiles[i, j].isEmpty = true; //Empty the interacted Tile   
            TileReform(i, j);
            });
        }
        //Interact witch upper Tile
        if (i > 0)
        {
            ScoreTracker.Instance.Score += AllTiles[i - 1, j].Number; // Get Score   
            Tweener tweener = TileDestroySpecial(i - 1, j);
            tweener.OnComplete(() =>
            {
                AllTiles[i - 1, j].isEmpty = true; //Empty the interacted Tile   
                TileReform(i - 1, j);
            });
        }
        //Interact witch under Tile
        if (i < 4)
        {
            ScoreTracker.Instance.Score += AllTiles[i + 1, j].Number; // Get Score    
            Tweener tweener = TileDestroySpecial(i + 1, j);
            tweener.OnComplete(() =>
            {
                AllTiles[i + 1, j].isEmpty = true; //Empty the interacted Tile   
                TileReform(i + 1, j);
            });
        }
        //Interact witch left Tile
        if (j > 0)
        {
            ScoreTracker.Instance.Score += AllTiles[i, j - 1].Number; // Get Score   
            Tweener tweener = TileDestroySpecial(i, j - 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i, j - 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i, j - 1);
            });
        }
        //Interact witch right Tile
        if (j < 4)
        {
            ScoreTracker.Instance.Score += AllTiles[i, j + 1].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i, j + 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i, j + 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i, j + 1);
            });
        }
        //Left Corner Tile
        if (i > 0 && j > 0)
        {
            ScoreTracker.Instance.Score += AllTiles[i - 1, j - 1].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i - 1, j - 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i - 1, j - 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i - 1, j - 1);
            });
        }
        if (i > 0 && j < 4)
        {
            ScoreTracker.Instance.Score += AllTiles[i - 1, j + 1].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i - 1, j + 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i - 1, j + 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i - 1, j + 1);
            });
        }
        //Right Corner Tile
        if (i < 4 && j < 4)
        {
            ScoreTracker.Instance.Score += AllTiles[i + 1, j + 1].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i + 1, j + 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i + 1, j + 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i + 1, j + 1);
            });
        }

        if (i < 4 && j > 0)
        {
            ScoreTracker.Instance.Score += AllTiles[i + 1, j - 1].Number; // Get Score
            Tweener tweener = TileDestroySpecial(i + 1, j - 1);
            tweener.OnComplete(() =>
            {
                AllTiles[i + 1, j - 1].isEmpty = true; //Empty the interacted Tile   
                TileReform(i + 1, j - 1);
            });
        }
        yield return new WaitForSeconds(0.5f);

        FillTheEmptyTile();
    }

    void FillTheEmptyTile()
    {
        for (int i = 0; i < colums.Count; i++)
        {
            while (!IsAllCollumFilled(colums[i]))
            {
                FillOneEmptyTile(colums[i]);
            }
        }
    }

    void FillOneEmptyTile(Tile[] collum)
    {
        if (collum[0].isEmpty == true)
        {
            collum[0].Number = Generate();
            collum[0].isEmpty = false;
        }
        for (int i = 0; i < colums.Count - 1; i++)
        {
            if (collum[i].isEmpty == false && collum[i + 1].isEmpty == true)
            {
                
                collum[i + 1].Number = collum[i].Number;
                collum[i + 1].isEmpty = false;            
                collum[i].isEmpty = true;


                break;
            }
        }
    }

    bool IsAllCollumFilled(Tile[] collum)
    {
        for (int i = 0; i < colums.Count; i++)
            if (collum[i].isEmpty == true)
                return false;
        return true;
    }




    public void NewGameButtonHandler()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    int Generate()
    {

        int randomNum = Random.Range(0, 10);
        if (randomNum > 8)
            return 12;
        else if (randomNum > 5)
            return 4;
        else
            return 1;
    }

     bool CanMove()
     {
        for (int i = 0; i < colums.Count; i++)
            for (int j = 0; j < colums.Count; j++)
                if (IsTileCanInteract(i,j))
                    return true;
        return false;
     }

    bool IsTileCanInteract(int x, int y)
    {
        if (x == 0 && y == 0 && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color)
            return false;
        else if (x == 0 && y == colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y - 1].TileImage.color)
            return false;
        else if (x == colums.Count - 1 && y == 0 && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color)
            return false;
        else if (x == colums.Count - 1 && y == colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y - 1].TileImage.color)
            return false;
        else if (x == 0 && y > 0 && y < colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y - 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color)
            return false;
        else if (x == colums.Count - 1 && y > 0 && y < colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y - 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color)
            return false;
        else if (y == 0 && x > 0 && x < colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color)
            return false;
        else if (y == colums.Count - 1 && x > 0 && x < colums.Count - 1 && AllTiles[x, y].TileImage.color != AllTiles[x, y - 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color)
            return false;
        else if (x > 0 && y > 0 && x < colums.Count - 1 && y < colums.Count - 1 && AllTiles[x,y].TileImage.color != AllTiles[x, y - 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x, y + 1].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x + 1, y].TileImage.color && AllTiles[x, y].TileImage.color != AllTiles[x - 1, y].TileImage.color)
            return false;

        return true;
    }
}