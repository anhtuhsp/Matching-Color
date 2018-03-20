using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public class TileStyle
{
    private int number;
    public Sprite image;
    public Color32 TileColor;
    public Color32 TextColor;
}

public class TileStyleHolder : MonoBehaviour
{

    public static TileStyleHolder instance;
    public TileStyle[] TileStyles;

    private void Awake()
    {
        instance = this;
    }
}