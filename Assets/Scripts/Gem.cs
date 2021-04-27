using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.Events;


public class Gem : MonoBehaviour, IPointerClickHandler
{
    #region Valerio
    public Tilemap erasableTilemap;
    public Tilemap notErasableTilemap;

    #endregion
    public Vector3 Offset = Vector3.zero;
    public float LerpFactor = 1;
    public int LevelId { get; set; }
    bool active;
    bool draggable;
    bool movable;
    bool doorActive;
    Vector3 startPos;
    Vector3 borderPos;

    static Transform screenHandlerLD, screenHandlerUR;

    private void Start()
    {
        startPos = transform.position;

        screenHandlerLD = Camera.main.transform.GetChild(0);
        screenHandlerUR = Camera.main.transform.GetChild(1);

    }

    private void OnEnable()
    {
        EventManager.OnDoorActivation.AddListener(OnDoorActivation);
        EventManager.OnLevelChange.AddListener(OnLevelChange);

    }


    private void OnDisable()
    {
        EventManager.OnDoorActivation.RemoveListener(OnDoorActivation);
        

    }

    public void OnLevelChange(int index)
    {
       
        if (active)
        {
            //EventManager.OnLevelChange.RemoveListener(OnLevelChange);
            if (index == 0)
                transform.position = startPos;

            //Manage tile restoring on level change
            erasableTilemap.ClearAllTiles();
            List<Vector3Int> pos = LevelGenerator.TilemapDic[LevelId].Item1;
            List<TileBase> tiles = LevelGenerator.TilemapDic[LevelId].Item2;

            erasableTilemap.SetTiles(pos.ToArray(), tiles.ToArray());

            draggable = false;
            doorActive = false;
            active = false;

        }
        else
        {
            if (LevelId == index)
                gameObject.SetActive(true);

           
        }
    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!draggable)
        {
            draggable = true;
            movable = true;
            active = true;
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            if (!doorActive)
            {
                ResetGem();

            }

        }
    }

    public void ResetGem()
    {
        draggable = false;
        transform.position = startPos;
    }

    private void Update()
    {
        if (draggable && movable)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsInsideBounds(mousePos,transform.localScale))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 truePos = new Vector3(pos.x, pos.y, 0);
                transform.position = Vector3.Lerp(transform.position, truePos, Time.deltaTime * LerpFactor) + Offset;

                Vector3Int gemPosE = erasableTilemap.WorldToCell(transform.position);
                if (erasableTilemap.GetTile(gemPosE) != null)
                    erasableTilemap.SetTile(gemPosE, null);

                //Managing not erasable tilemap collisions
                if (notErasableTilemap.isActiveAndEnabled)
                {
                    Vector3Int gemPosNE = notErasableTilemap.WorldToCell(transform.position);
                    if (notErasableTilemap.GetTile(gemPosNE) != null)
                        ResetGem();

                }


            }
        }
        

        
    }

    public static bool IsInsideBounds(Vector3 pos, Vector3 scale)
    {
        bool result = false;
        if (pos.x < screenHandlerUR.position.x-(scale.x*0.5f)&& pos.x > screenHandlerLD.position.x+ (scale.x * 0.5f)
                && pos.y < screenHandlerUR.position.y- (scale.y * 0.5f) && pos.y > screenHandlerLD.position.y+ (scale.y * 0.5f))
        {
            result = true;
        }

        return result;
    }

    public void OnDoorActivation()
    {
        transform.position = startPos;
        gameObject.SetActive(false);
        Cursor.visible = true;

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //--------------------testing------------------//
        //if (collision.gameObject.CompareTag("Erasable"))
        //{
        //    collision.gameObject.GetComponent<ErasableObj>().Erase();
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BorderTile") ||
            collision.gameObject.CompareTag("Border"))
        {
            borderPos = transform.position;
            movable = false;
        } else if (collision.gameObject.CompareTag("Slot"))
        {
            doorActive = true;
            collision.GetComponent<ActivableObj>().IsGemInside(doorActive);

        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("BorderTile") ||
    //        collision.gameObject.CompareTag("Border"))
    //    {
    //        transform.position = borderPos;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slot"))
        {
            doorActive = false;
            collision.GetComponent<ActivableObj>().IsGemInside(doorActive);


        }

    }

}
