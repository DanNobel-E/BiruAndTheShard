using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilemapType { Grass, Stone, Base, Last } //added new types to tilemapTypes and PrefabTypes
public enum PrefabType { Player, Gem, Door, Button, Border, Last }
public class LevelGenerator : MonoBehaviour
{
    public Texture2D[] LevelTextures;
    public static Dictionary<int, Tuple<List<Vector3Int>, List<TileBase>>> TilemapDic = new Dictionary<int, Tuple<List<Vector3Int>, List<TileBase>>>();
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

        TilemapDic[index + 1] = new Tuple<List<Vector3Int>, List<TileBase>>(new List<Vector3Int>(), new List<TileBase>());

        for (int i = 0; i < (int)TilemapType.Last; i++)
        {
            tilemaps[i] = CreateTilemap((TilemapType)i, newLevel.transform);
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
                            case TilemapType.Grass:
                                Vector3Int pos = new Vector3Int(x, y, 0);
                                tilemaps[0].SetTile(pos, item.TileBase);
                                TilemapDic[index + 1].Item1.Add(pos);
                                TilemapDic[index + 1].Item2.Add(item.TileBase);
                                break;
                            case TilemapType.Stone: //added stone tilemap
                                tilemaps[1].SetTile(new Vector3Int(x, y, 0), item.TileBase);
                                break;
                            case TilemapType.Base:
                                tilemaps[2].SetTile(new Vector3Int(x, y, 0), item.TileBase);
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
                            case PrefabType.Gem:
                                GameObject gem = Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                Gem g = gem.GetComponentInChildren<Gem>();
                                g.erasableTilemap = tilemaps[0];
                                g.notErasableTilemap = tilemaps[1]; //added not erasable tilemap to gem
                                g.LevelId = index + 1;
                                break;
                            case PrefabType.Door:
                                GameObject door = Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                DoorObj d= door.GetComponent<DoorObj>();
                                d.LevelId = index + 1;
                                int currLevel = newLevel.transform.GetSiblingIndex() + 1;
                                if (currLevel + 1 > LevelTextures.Length)
                                    d.NextLevel = 1;
                                else
                                    d.NextLevel = currLevel + 1;
                                break;
                            case PrefabType.Player:
                                GameObject player= Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                PlayerMgr p = player.GetComponent<PlayerMgr>();
                                p.LevelId = index + 1;
                                break;
                            case PrefabType.Button: //managed buttons spawning
                                GameObject button = Instantiate(item.Prefab, new Vector3(x, y, 0), Quaternion.identity, newLevel.transform);
                                ButtonLogic b = button.GetComponent<ButtonLogic>();
                                b.LevelId = index + 1; 
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

    private Tilemap CreateTilemap(TilemapType type, Transform gridParent, string layerName = "GroundTile")
    {
        var go = new GameObject($"tm_{type}");
        go.layer = LayerMask.NameToLayer(layerName);
        var tm = go.AddComponent<Tilemap>();
        var tr = go.AddComponent<TilemapRenderer>();
        go.AddComponent<TilemapCollider2D>();

        //added tile manager script to tilemaps game objects
        var tmgr = go.AddComponent<TilemapMgr>();
        tmgr.Type= type;
        tmgr.enabled = false;
        tmgr.enabled = true;
        tmgr.LevelId = gridParent.GetSiblingIndex() + 1;



        tm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        go.transform.SetParent(gridParent.transform);
        tr.sortingLayerName = layerName;

        return tm;
    }

}
