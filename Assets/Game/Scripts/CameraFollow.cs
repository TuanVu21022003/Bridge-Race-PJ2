using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject playerPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 posStart;
    // Start is called before the first frame update
    void Start()
    {
        posStart = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayManager.instance.endGame)
        {
            transform.position = Vector3.Lerp(transform.position, offset + PlayManager.instance.characterWin.transform.position, speed * Time.deltaTime);
            return;
        }
        if(playerPrefab != null)
        {
            transform.position = Vector3.Lerp(transform.position, offset + playerPrefab.transform.position, speed * Time.deltaTime);

        }
    }

    public void SetPlayer(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
        offset = posStart - playerPrefab.transform.position;
    }

}
