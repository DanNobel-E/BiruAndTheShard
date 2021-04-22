using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Player, Gem, Grass, Stone, Enemy, Border, Door, Base, Last}
public class LevelManager : MonoBehaviour
{

    public Texture2D[] LevelTextures;
    public Transform Grid;
    public static int CurrentLevel=1;
    public TileColorMgr TileMgr;
    bool playerLocated;
    bool doorLocated;

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

        playerLocated = false;
        doorLocated = false;
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
                case TileType.Door:
                    if (!doorLocated)
                    {
                        Vector3 dPos = new Vector3(x +0.75f, y+1, 0);
                        GameObject door= Instantiate(tile, dPos, Quaternion.identity, child);

                        int currLevel = child.GetSiblingIndex() + 1;
                        if (currLevel + 1 > LevelTextures.Length)
                            door.GetComponent<DoorObj>().NextLevel = 1;
                        else
                            door.GetComponent<DoorObj>().NextLevel = currLevel+1;

                        doorLocated = true;
                    }
                    break;
                default:
                    Vector3 pos = new Vector3(x + 0.25f, y +0.5f,0);
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
        
        if(index!=0)
            CurrentLevel = index;
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
        int id;
        //Restart
        if (index == 0)
            id = CurrentLevel;
        else
            id = index;
        
        Transform nextLevel = Grid.GetChild(id - 1);

        nextLevel.gameObject.SetActive(true);

        for (int i = 0; i < nextLevel.childCount; i++)
        {
            nextLevel.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void ResetLevel()
    {
        Transform level = Grid.GetChild(CurrentLevel - 1);

        level.gameObject.SetActive(false);

        for (int i = 0; i < level.childCount; i++)
        {
            level.GetChild(i).gameObject.SetActive(false);
        }

    }
}
