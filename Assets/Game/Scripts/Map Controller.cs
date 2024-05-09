using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    [SerializeField] private List<GameObject> listFloor = new List<GameObject>();
    public int countFloor => listFloor.Count;
    public GameObject GetFloorByLevel(int levelIndex)
    {
        return listFloor[levelIndex - 1];
    }

    public void OnInit()
    {
        for(int i = 0; i < listFloor.Count -1; i++)
        {
            listFloor[i].GetComponent<FloorController>().OnInit();
        }
    }
}
