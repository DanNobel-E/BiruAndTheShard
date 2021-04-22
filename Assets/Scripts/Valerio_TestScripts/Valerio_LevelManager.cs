using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileBaseType { Stone, Last }
public class Valerio_LevelManager : MonoBehaviour
{
    public Texture2D[] LevelTextures;
    public VAlerio_TileColorMgr TileMgr;
    [Header("LevelComponents")]
    public Transform Grid;
    public static int CurrentLevel = 1;
    public Tilemap tm;
    //----------------------------------------------------------------//
    bool playerLocated;
    bool doorLocated;

    void Start()
    {
        

        for (int i = 0; i < LevelTextures.Length; i++)
        {
            GenerateLevel(LevelTextures[i], i);

        }

    }

    private void GenerateLevel(Texture2D t, int index)
    {
        GameObject level = new GameObject();
        level.name = $"Level_{index + 1}";
        level.AddComponent<Grid>();
        tm = CreateTilemap("Grass", level);
        
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                Color tileColor = t.GetPixel(x, y);
                if(VAlerio_TileColorMgr.ColorsDictionary.ContainsKey(tileColor))
                {
                    
                    GenerateTile(VAlerio_TileColorMgr.ColorsDictionary[tileColor],x,y,level.transform);
                }
            }
        }

       

        //playerLocated = false;
        //doorLocated = false;
    }

    private void GenerateTile(TileBaseType type,int x,int y,Transform level)
    {
        TileBase tile = VAlerio_TileColorMgr.TilesDictionary[type];

        if (tile != null)
        {
            switch (type)
            {
               
                default:
                   
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    tm.SetTile(tilePos, tile);

                    break;
            }

            

        }
    }

    private Tilemap CreateTilemap(string tilemapName,GameObject gridParent,string layerName="Main")
    {
        var go = new GameObject(tilemapName);
        var tm = go.AddComponent<Tilemap>();
        var tr = go.AddComponent<TilemapRenderer>();
        var tc = go.AddComponent<TilemapCollider2D>();

        tm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        go.transform.SetParent(gridParent.transform);
        tr.sortingLayerName = layerName;

        return tm;
    }
}
