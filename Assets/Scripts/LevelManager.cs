using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Player, Gem, Stone, Iron, Enemy, Border, Last}
public class LevelManager : MonoBehaviour
{

    public Texture2D[] LevelTextures;
    public Transform Grid;
    public int CurrentLevel=1;
    public TileColorMgr TileMgr;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);
        GenerateLevel(LevelTextures[CurrentLevel - 1]);
    }

    public void GenerateLevel(Texture2D t)
    {
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
              Color tileColor=  t.GetPixel(x, y);

                if (TileColorMgr.ColorsDictionary.ContainsKey(tileColor))
                {
                    GenerateTile(TileColorMgr.ColorsDictionary[tileColor],x,y);

                }

            }
        }
    }

    private void GenerateTile(TileType type, int x,int y)
    {
        GameObject tile = TileColorMgr.TilesDictionary[type];

        if (tile != null)
        {
            Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, 0);
            Instantiate(tile, pos, Quaternion.identity, Grid);
        }
    }

    public void OnLevelChange()
    {
        CurrentLevel++;
        ResetGrid();
        GenerateLevel(LevelTextures[CurrentLevel - 1]);
    }

    private void ResetGrid()
    {
        for (int i = 0; i < Grid.childCount; i++)
        {
            Destroy(Grid.GetChild(i));
        }
    }
}
