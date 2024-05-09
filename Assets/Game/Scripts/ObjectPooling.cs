using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> poolingObjects = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetObject(GameObject objectPrefab)
    {
        if(poolingObjects.ContainsKey(objectPrefab))
        {
            foreach(GameObject obj in poolingObjects[objectPrefab])
            {
                if(obj.activeSelf == false)
                {
                    return obj;
                }
            }
            GameObject g = Instantiate(objectPrefab, this.transform.position, this.transform.rotation);
            g.SetActive(false);
            poolingObjects[objectPrefab].Add(g);
            return g;
        }
        List<GameObject> list = new List<GameObject>();
        GameObject g2 = Instantiate(objectPrefab, this.transform.position, this.transform.rotation);
        g2.SetActive(false);
        list.Add(g2);
        poolingObjects.Add(g2, list);
        return g2;
    }
}
