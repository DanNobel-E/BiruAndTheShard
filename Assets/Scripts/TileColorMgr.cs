using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorMgr :MonoBehaviour
{
    public Color PlayerColor= new Color(0,1,0,1);
    public Color GemColor = new Color(0, 0, 1, 1);
    public Color StoneColor = new Color(1, 1, 0, 1);
    public Color IronColor = new Color(0, 1, 1, 1);
    public Color EnemyColor = new Color(1, 0, 0, 1);
    public Color BorderColor = new Color(0, 0, 0, 1);


    public GameObject[] TilePrefab;

    public static Dictionary<Color, TileType> ColorsDictionary = new Dictionary<Color, TileType>();
    public static Dictionary<TileType, GameObject> TilesDictionary = new Dictionary<TileType, GameObject>();
    
    void Awake()
    {
        ColorsDictionary[PlayerColor] = TileType.Player;
        ColorsDictionary[GemColor] = TileType.Gem;
        ColorsDictionary[StoneColor] = TileType.Stone;
        ColorsDictionary[IronColor] = TileType.Iron;
        ColorsDictionary[EnemyColor] = TileType.Enemy;
        ColorsDictionary[BorderColor] = TileType.Border;


        for (int i = 0; i < (int)TileType.Last; i++)
        {
            TilesDictionary[(TileType)i] = TilePrefab[i];
        }

    }

}
