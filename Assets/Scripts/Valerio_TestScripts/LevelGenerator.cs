using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum tmType { Grass, Stone, Last }
public enum goType { Player, Gem, Door, Last }
public class LevelGenerator : MonoBehaviour
{
    public Texture2D[] LevelTextures;

    public prefabParser[] colorToPrefabs;
    public tilebaseParser[] colorToTileBases;
    public List<Tilemap> tilemaps;
    public static int CurrentLevel = 1;
    void Start()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);

        for (int i = 0; i < LevelTextures.Length; i++)
        {
            GenerateLevel(LevelTextures[i],i);
        }
       
       
    }

   
    
    private void GenerateLevel(Texture2D texture2D, int index)
    {
        GameObject newLevel = new GameObject();
     
        var Lgrid = newLevel.AddComponent<Grid>();
        newLevel.transform.SetParent(this.transform);
        newLevel.name = $"Level_{index + 1}";
        for (int i = 0; i < (int)tmType.Last; i++)
        {
            tilemaps[i] = CreateTilemap($"tm_{(tmType)i}", newLevel.transform);
        }

        for (int x = 0; x < texture2D.width; x++)
        {
            for (int y = 0; y < texture2D.height; y++)
            {
                Color tileColor = texture2D.GetPixel(x, y);



                foreach (tilebaseParser item in colorToTileBases)
                {
                    if (item.Color.Equals(tileColor))
                    {
                        switch (item.tmType)
                        {
                            case tmType.Grass:
                                tilemaps[0].SetTile(new Vector3Int(x, y, 0), item.TileBase);
                                break;
                            case tmType.Stone:
                                tilemaps[1].SetTile(new Vector3Int(x, y, 0), item.TileBase);
                                break;
                            
                            default:
                                break;
                        }
                        
                    }
                }

                foreach (prefabParser item in colorToPrefabs)
                {
                    if(item.Color.Equals(tileColor))
                    {
                        switch (item.goType)
                        {
                            case goType.Gem:
                                GameObject gem = Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                gem.GetComponentInChildren<Gem>().erasableTilemap = tilemaps[0];
                                
                                break;
                            case goType.Door:
                                GameObject door = Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                int currLevel = newLevel.transform.GetSiblingIndex() + 1;
                                if (currLevel + 1 > LevelTextures.Length)
                                    door.GetComponent<DoorObj>().NextLevel = 1;
                                else
                                    door.GetComponent<DoorObj>().NextLevel = currLevel + 1;
                                break;
                           
                               
                            default:
                                Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                break;
                        }

                       
                    }
                }


                
            }
        }

        if (index + 1 != CurrentLevel)
            newLevel.SetActive(false);
    }

    public void OnLevelChange(int index)
    {
        ResetLevel();
        ChangeLevel(index);

        if (index != 0)
            CurrentLevel = index;
    }

    private void ResetLevel()
    {
        Transform level = transform.GetChild(CurrentLevel - 1);

        level.gameObject.SetActive(false);
        

        for (int i = 0; i < level.childCount; i++)
        {
            level.GetChild(i).gameObject.SetActive(false);
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

        Transform nextLevel = transform.GetChild(id - 1);

        nextLevel.gameObject.SetActive(true);

        for (int i = 0; i < nextLevel.childCount; i++)
        {
            nextLevel.GetChild(i).gameObject.SetActive(true);
        }
    }

    private Tilemap CreateTilemap(string tilemapName, Transform gridParent, string layerName = "GroundTile")
    {
        var go = new GameObject(tilemapName);
        go.layer = LayerMask.NameToLayer(layerName);
       
        var tm = go.AddComponent<Tilemap>();
        var tr = go.AddComponent<TilemapRenderer>();
        var tmc = go.AddComponent<TilemapCollider2D>();
        tmc.usedByComposite = true;
       
        var tmrb = go.AddComponent<Rigidbody2D>();
        tmrb.bodyType = RigidbodyType2D.Static;
        var tmcc = go.AddComponent<CompositeCollider2D>();
        



        tm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        go.transform.SetParent(gridParent.transform);
        tr.sortingLayerName = "Middleground";

        return tm;
    }

    

}
