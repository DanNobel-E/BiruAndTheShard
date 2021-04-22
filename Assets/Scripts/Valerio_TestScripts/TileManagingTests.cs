using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManagingTests : MonoBehaviour
{
    public GridLayout gridLayout;
    Vector3Int cellPosition;
    public bool cellHere;
    public bool ClearTileMap;
    public bool GenerateTilemap;
    public bool refreshTilemap;
    public Tilemap tilemap;
    public float tilemapWidth, tileMapHeight;
    public TileBase tile;
    [Range(0, 1)]
    public float tileprob;

    private void Update()
    {
        #region erasingLogic
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //cellPosition = gridLayout.WorldToCell(transform.position);
        //cellHere = tilemap.GetTile(cellPosition);
        //if (cellHere) tilemap.SetTile(cellPosition, null); //erasing tile map
        #endregion
        if (ClearTileMap)
        {
            ClearTileMap = false;
            Clear();
        }
        if (GenerateTilemap)
        {
            Clear();
            RandomTileMapGenerator(tilemapWidth, tileMapHeight);
            GenerateTilemap = false;
        }
        if (refreshTilemap)
        {
            refreshTilemap = false;
            RefreshTilemap(tilemap);
        }
    }

    void Clear()
    {
        tilemap.ClearAllTiles();

    }

    void RandomTileMapGenerator(float w, float h)
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (Random.Range(0f, 1f) < tileprob) tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    void RefreshTilemap(Tilemap tm)
    {
        tm.RefreshAllTiles();
    }


}
