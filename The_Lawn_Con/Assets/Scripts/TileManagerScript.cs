using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FMODUnity;

public class TileManagerScript : MonoBehaviour
{
    public Camera cam;
    public Tilemap tileMap;
    public TileBase dirt;
    public TileBase grass;
    public TileBase moss;
    public TileBase flower;
    //public TileBase garden;
    public TileBase wild_grass;
    public TileBase path;

    public TileBase currentBase;

    public GameObject startUI;
    public GameObject dayUI;
    public GameObject feedbackUI;
   
    //public GameObject tileKind;

    //public List<Vector3> availablePlaces;


    public bool start = true;

    public int type = 0;

    private bool workableTilePlaced = false;


    public Vector3Int pos;
    // Start is called before the first frame update

    //FMOD
    [SerializeField, EventRef] public string popEvent;
    [SerializeField, EventRef] public string changeEvent;



    void Start()
    {
        currentBase = dirt;
        dayUI.SetActive(false);
        feedbackUI.SetActive(false);

        //popSound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tileMap.WorldToCell(mousePos);

            if (start)
            {
                ChangeTileType(gridPos);
            }
            else
            {
                GetTileType(gridPos);
            }

        }

        
    }

    private void ChangeTileType(Vector3Int gridPos)
    {
        if (tileMap.HasTile(gridPos))
        {
            switch (type)
            {
                case 0:
                    tileMap.SetTile(gridPos, dirt);
                    GetTileType(gridPos);

                    break;
                case 1:
                    tileMap.SetTile(gridPos, grass);
                    workableTilePlaced = true;
                    break;
                case 2:
                    tileMap.SetTile(gridPos, moss);
                    workableTilePlaced = true;
                    break;
                case 3:
                    tileMap.SetTile(gridPos, flower);
                    workableTilePlaced = true;
                    break;
               /* case 4:
                    tileMap.SetTile(gridPos, garden);
                    break;*/
                case 4:
                    tileMap.SetTile(gridPos, wild_grass);
                    workableTilePlaced = true;
                    break;
                case 5:
                    tileMap.SetTile(gridPos, path);
                    break;
                default:
                    Debug.LogError("Invalid Type Index");
                    break;
            }
            RuntimeManager.PlayOneShot(changeEvent, cam.transform.position);
            tileMap.RefreshAllTiles();
        }
    }

    public string GetTileType(Vector3Int gridPos)
    {
        if (tileMap.HasTile(gridPos))
        {
            string temp = tileMap.GetTile(gridPos).name;
            Debug.Log(temp);
            return temp;
        }

        return null;
    }

    public void ChangeTypeInt(int newType)
    {
        type = newType;
        //RuntimeManager.PlayOneShot(popEvent, cam.transform.position);
    }

    public void AssignScript()
    {
        /*
       TileBase[] tileArray = tileMap.GetTilesBlock(tileMap.cellBounds);
       for (int i = 0; i < tileArray.Length; i++)
       {
            for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
            {
                for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
                {
                    Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                    Vector3 place = tileMap.CellToWorld(localPlace);
                    if (tileMap.HasTile(localPlace))
                    {
                        //Tile at "place"
                        availablePlaces.Add(place);
                    }
                    else
                    {
                        //No tile at "place"
                    }
                }
            }

            if (tileArray[i] != null)
            {
                string thisTile = tileArray[i].name;
                //Debug.Log(thisTile);

                if(thisTile == "Dirt")
                {
                    //tileArray[i].GetTileData(availablePlaces[i], tileMap, ref TileData tileData)
                    tileArray[i].
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Grass")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Moss")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Garden")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Flower")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Wild_Grass")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
                else if(thisTile == "Tree")
                {
                    Debug.Log(thisTile);
                    Debug.Log(i);
                }
            }
            //string thisTile = tileArray[i].name;

            //Debug.Log(thisTile);

            //Debug.Log("Hello~");
       
       } */
    }

    public void Finish()
    {
        if (workableTilePlaced)
        {
            start = false;
            AssignScript();
            startUI.SetActive(false);
            dayUI.SetActive(true);
            feedbackUI.SetActive(true);
        }
        
    }

}
