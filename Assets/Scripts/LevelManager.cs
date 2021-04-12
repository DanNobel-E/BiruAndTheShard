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
    bool playerLocated;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);

        for (int i = 0; i < LevelTextures.Length; i++)
        {
            GenerateLevel(LevelTextures[i], i);

        }
      
    }

    public void GenerateLevel(Texture2D t, int index)
    {
        GameObject child = new GameObject();
        child.transform.position = Grid.position;
        child.transform.SetParent(Grid);
        child.name = $"Level_{index + 1}";


        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
              Color tileColor=  t.GetPixel(x, y);

                if (TileColorMgr.ColorsDictionary.ContainsKey(tileColor))
                {
                    GenerateTile(TileColorMgr.ColorsDictionary[tileColor],x,y,child.transform);

                }

            }
        }

        if (index + 1 != CurrentLevel)
            child.SetActive(false);
    }

    private void GenerateTile(TileType type, int x,int y, Transform child)
    {
        GameObject tile = TileColorMgr.TilesDictionary[type];

        if (tile != null)
        {
            switch (type)
            {
                case TileType.Player:
                    if (!playerLocated)
                    {
                        Vector3 pPos = new Vector3(x + 0.5f, y + 0.5f, 0);
                        Instantiate(tile, pPos, Quaternion.identity, child);
                        playerLocated = true;
                    }
                    break;
                case TileType.Enemy:
                    break;
                default:
                    Vector3 pos = new Vector3(x + 0.25f, y + 0.25f, 0);
                    Instantiate(tile, pos, Quaternion.identity, child);
                    break;
            }

            tile.SetActive(true);

        }
    }

    public void OnLevelChange(int index)
    {
        ResetLevel();
        ChangeLevel(index);
    }

    private void EraseLevel()
    {
        for (int i = 0; i < Grid.childCount; i++)
        {
            Destroy(Grid.GetChild(i));
        }
    }


    private void ChangeLevel(int index)
    {
        
        Transform nextLevel = Grid.GetChild(index - 1);

        nextLevel.gameObject.SetActive(true);
    }
    private void ResetLevel()
    {
        Transform level = Grid.GetChild(CurrentLevel - 1);

        level.gameObject.SetActive(false);


    }
}
