using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VAlerio_TileColorMgr : MonoBehaviour
{
    public TileBase[] Tiles;

    public static Dictionary<Color, TileBaseType> ColorsDictionary = new Dictionary<Color, TileBaseType>();
    public static Dictionary<TileBaseType, TileBase> TilesDictionary = new Dictionary<TileBaseType, TileBase>();

    [Header("ColorToTileBase")]
    public Color GrassColor = new Color(1,1,0,1);

    void Awake()
    {
        
        ColorsDictionary[GrassColor] = TileBaseType.Stone;
       




        for (int i = 0; i < (int)TileType.Last; i++)
        {
            TilesDictionary[(TileBaseType)i] = Tiles[i];
        }

    }

}
