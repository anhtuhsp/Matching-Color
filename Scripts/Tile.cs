using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public GameObject gameobj;
    public int indRow;
    public int indCol;

    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            if (number >= 1 && number < 4)
                ApplyStyle(0, number);
            else if (number >= 4 && number < 12)
                ApplyStyle(1, number);
            else if (number >= 12 && number < 36)
                ApplyStyle(2, number);
            else if (number >= 36 && number < 108)
                ApplyStyle(3, number);
            else if (number >= 108 && number < 432)
                ApplyStyle(4, number);
            else if (number >= 432)
                ApplyStyle(5, number);
            else if (number == 0)
                ApplyStyle(6, number);
        
        }
    }


    public RectTransform TileTransForm;
    private int number;   
    private bool isempty = false;
    public bool isEmpty
    {
        get
        {
            return isempty;
        }
        set
        {
            isempty = value;
            if (isempty == false)
            {             
                SetVisible();
            }
            else if (isempty == true)
            {     
                SetEmpty();
            }
        }
    }

    public Text TileText;
    public Image TileImage;
    public Action<int, int> OnTileClicked;

    private void Awake()
    {
        TileTransForm = GetComponent<RectTransform>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (OnTileClicked != null)
            {
                OnTileClicked(indRow, indCol);
            }
        });
        TileImage = transform.Find("Panel").GetComponent<Image>();
        TileText = GetComponentInChildren<Text>();      
    }

    void ApplyStyleFromHolder(int index, int number)
    {
        if (number > 0)
        {
            TileText.text = number.ToString();
            TileText.color = TileStyleHolder.instance.TileStyles[index].TextColor;
        }
        TileImage.color = TileStyleHolder.instance.TileStyles[index].TileColor;
        TileImage.sprite = TileStyleHolder.instance.TileStyles[index].image;
    }


    void ApplyStyle(int index, int number)
    {
        switch (index)
        {
            case 0:
                ApplyStyleFromHolder(0, number);
                break;
            case 1:
                ApplyStyleFromHolder(1, number);
                break;
            case 2:
                ApplyStyleFromHolder(2, number);
                break;
            case 3:
                ApplyStyleFromHolder(3, number);
                break;
            case 4:
                ApplyStyleFromHolder(4, number);
                break;
            case 5:
                ApplyStyleFromHolder(5, number);
                break;
            case 6:
                ApplyStyleFromHolder(6, number);
                break;
            default:
                Debug.LogError("Check the number that you pass to ApplyStyle");
                break;

        }
    }

    private void SetVisible()
    {
        TileImage.enabled = true;
        if (TileImage.color == new Color32(250, 250, 251, 255) || Number >= 432 || Number == 0 || TileImage.color == new Color32(252, 252, 252, 252))
            TileText.enabled = false;
        else
            TileText.enabled = true;
    }

    public void SetEmpty()
    {
        TileImage.enabled = false;
        TileText.enabled = false;
    }

    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
