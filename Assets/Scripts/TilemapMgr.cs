using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMgr : MonoBehaviour
{
    public TilemapType Type;
    public int LevelId { get; set; }

    bool active;

    private void Start()
    {
        if (LevelId == LevelGenerator.CurrentLevel)
            active = true;
    }
    private void OnEnable()
    {
        if (Type == TilemapType.Stone)
        {
            EventManager.OnButtonPressed.AddListener(OnButtonPressed);
            EventManager.OnLevelChange.AddListener(OnLevelChange);
        }
    }

   

    public void OnButtonPressed(int index, bool b)
    {
        if (LevelId == index)
        {
            gameObject.SetActive(!b);

        }
    }

    public void OnLevelChange(int index)
    {
        if (index == LevelId)
            active = true;
        else if (active && index != LevelId && index != 0)
        {
            active = false;
            EventManager.OnButtonPressed.RemoveListener(OnButtonPressed);

        }

    }
}
