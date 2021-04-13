using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManagingTests : MonoBehaviour
{
    public GridLayout gridLayout;
    Vector3Int cellPosition;
    public bool cellHere;
    public Tilemap tilemap;
    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        cellPosition = gridLayout.WorldToCell(transform.position);
        cellHere = tilemap.GetTile(cellPosition);
        if (cellHere) tilemap.SetTile(cellPosition, null);

    }

    
}
