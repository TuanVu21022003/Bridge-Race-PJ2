using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class FloorController : MonoBehaviour
{
    [SerializeField] private GameObject fenceCorner;
    [SerializeField] private Transform parentBrick;
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private LayerMask layerFloor;
    [SerializeField] private Transform startRayCast;
    [SerializeField] private int levelFloor;
    [SerializeField] private List<GameObject> listDoorNext;
    private int sizeGroundXMax = -1, sizeGroundZMax = -1, sizeGroundXMin = 1000, sizeGroundZMin = 1000;
    private List<Tuple<int,int>> listPosBricks = new List<Tuple<int,int>>();
    private int countTotalBrick; 
    Dictionary<ColorBrickEnum, List<GameObject>> dicBrickByColor = new Dictionary<ColorBrickEnum, List<GameObject>>();
    
    private void Start()
    {
        
    }

    public void OnInit()
    {
        GetSizeGround();
        SetListPosBrick();
        SetCountTotalBrick();
    }
    
    public GameObject GetDoor()
    {
        int randomIndex = Random.Range(0, listDoorNext.Count);
        return listDoorNext[randomIndex];
    }

    public void SetCountTotalBrick()
    {
        countTotalBrick = listPosBricks.Count;
    }
    
    public void GetSizeGround()
    {
        foreach(Transform child in fenceCorner.transform)
        {
            sizeGroundXMax = Mathf.Max(sizeGroundXMax, (int)child.localPosition.x);
            sizeGroundXMin = Mathf.Min(sizeGroundXMin, (int)child.localPosition.x);

            sizeGroundZMax = Mathf.Max(sizeGroundZMax, (int)child.localPosition.z);
            sizeGroundZMin = Mathf.Min(sizeGroundZMin, (int)child.localPosition.z);

        }
        //Debug.Log("Min: " + sizeGroundXMin + ", " + sizeGroundZMin);
        //Debug.Log("Max: " + sizeGroundXMax + ", " + sizeGroundZMax);
    }

    public void SetListPosBrick()
    {
        //startRayCast.localPosition = new Vector3(15, 1, 15);
        //Ray ray = new Ray(startRayCast.position, Vector3.right);
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20f, Color.red, 1000f);

        //RaycastHit[] hitInfos = Physics.RaycastAll(ray, 20f);

        //if (hitInfos.Length > 0)
        //{
        //    foreach (RaycastHit hitInfo in hitInfos)
        //    {
        //        Debug.Log(hitInfo.collider.gameObject.name);
        //    }
        //}
        //else
        //{

        //}
        for (int i = sizeGroundZMin + 1; i <= sizeGroundZMax; i += 2)
        {
            for (int j = sizeGroundXMin + 1; j <= sizeGroundXMax; j += 2)
            {
                startRayCast.localPosition = new Vector3(j - 0.1f, 1.1f, i - 0.1f);
                Ray ray = new Ray(startRayCast.position, Vector3.right);
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 40f, Color.red, 1000f);

                RaycastHit[] hitInfos = Physics.RaycastAll(ray, 40f, layerFloor);

                if (hitInfos.Length > 0 && hitInfos.Length % 2 == 1)
                {
                    listPosBricks.Add(new Tuple<int, int>(j, i));
                }
                else
                {
                }
            }
        }
    }

    public void ShowListPosBrick()
    {
        foreach (Tuple<int, int> tuple in listPosBricks)
        {
            Debug.Log(tuple.Item1 + " va " + tuple.Item2);
        }
    }

    public void AutoInstantiateBrick(ColorBrickEnum colorBrick)
    {
        List<GameObject> bricks = new List<GameObject>();


        // Thử truy cập một khóa không tồn tại trong từ điển
        int value = UnityEngine.Random.Range((int)countTotalBrick / 4, (int)countTotalBrick / 3);
        while (value > 0)
        {
            if(listPosBricks.Count > 0)
            {
                int randomPos = UnityEngine.Random.Range(0, listPosBricks.Count);
                Brick brick = Instantiate(brickPrefab, parentBrick);
                brick.transform.localPosition = new Vector3(listPosBricks[randomPos].Item1, 0, listPosBricks[randomPos].Item2);
                brick.ChangeColor(colorBrick);
                brick.OnInit(colorBrick);
                listPosBricks.RemoveAt(randomPos);
                bricks.Add(brick.gameObject);
            }
            value -= 1;
        }

        dicBrickByColor.Add(colorBrick, bricks);
        Debug.Log("Da sinh gach");
        
    }
    [ContextMenu("ClearByColor")]

    public void ClearByColor(ColorBrickEnum colorPlayer)
    {
        if (dicBrickByColor.ContainsKey(colorPlayer))
        {
            List<GameObject> brickByColor = dicBrickByColor[colorPlayer];
            foreach (GameObject brick in brickByColor)
            {
                brick.SetActive(false);
            }
        }
    }

    public void RandomBrickByClolor(ColorBrickEnum colorPlayer)
    {
        if (dicBrickByColor.ContainsKey(colorPlayer))
        {
            List<GameObject> brickByColor = dicBrickByColor[colorPlayer];
            foreach (GameObject brick in brickByColor)
            {
                if(brick.activeSelf == false)
                {
                    brick.SetActive(true);
                    break;
                }
            }
        }
    }

    public GameObject RandomBrickBot(ColorBrickEnum colorPlayer)
    {
        GameObject brickBot = null;
        if (dicBrickByColor.ContainsKey(colorPlayer))
        {
            List<GameObject> brickByColor = dicBrickByColor[colorPlayer];
            List<GameObject> tmp = new List<GameObject>();
            foreach (GameObject brick in brickByColor)
            {
                if (brick.activeSelf == true)
                {
                    tmp.Add(brick);
                }
            }
            if(tmp.Count > 0)
            {
                int randomBrick = Random.Range(0, tmp.Count);
                brickBot = tmp[randomBrick];
            }
            
        }
        return brickBot;
    }

    public void InstantiateBrickGray(int countBrick, Vector3 positionCharacter)
    {
        Debug.Log("Sinh gach xam : " + countBrick);
        int countRow = (int)Math.Sqrt(countBrick);
        countRow = countRow * countRow == countBrick ? countRow : countRow + 1;
        Debug.Log("Sinh gach xam nua: " + countRow);
        while (countBrick > 0)
        {
            for(int i = 0; i < countRow; i++)
            {
                for(int j = 0; j < countRow; j++)
                {
                    GameObject g = ObjectPooling.Instance.GetObject(brickPrefab.gameObject);
                    g.transform.position = positionCharacter + new Vector3(-countRow + i, positionCharacter.y - 1, -countRow + j);
                    g.transform.rotation = Quaternion.identity;
                    g.GetComponent<Brick>().ChangeColor(ColorBrickEnum.GRAY);
                    g.GetComponent<Brick>().OnInit(ColorBrickEnum.GRAY);
                    g.SetActive(true);
                    countBrick--;
                }
            }
        }
    }

}
