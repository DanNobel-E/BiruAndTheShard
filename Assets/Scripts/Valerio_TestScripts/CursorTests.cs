using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTests : MonoBehaviour
{
    public Texture2D t;

    public void Start()
    {
        Cursor.SetCursor(t, Vector2.zero, CursorMode.Auto);
        
        
    }
}
