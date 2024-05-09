using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClose;
    [SerializeField] private int doorLevel;
    void Start()
    {
        doorOpen.SetActive(false);
        doorClose.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = other.gameObject.GetComponent<Character>();
            if(player.floorIndexCurrent < doorLevel)
            {
                player.SetFloorIndexCurrent();
                player.GoToDoor(transform.position);
                doorOpen.SetActive(true);
                doorClose.SetActive(false);
                doorOpen.GetComponent<MeshRenderer>().material = PlayManager.instance.GetMaterial(player.colorPlayer);
                MapController.Instance.GetFloorByLevel(doorLevel - 1).GetComponent<FloorController>().ClearByColor(player.colorPlayer);
                MapController.Instance.GetFloorByLevel(doorLevel).GetComponent<FloorController>().AutoInstantiateBrick(player.colorPlayer);
                player.ActionNext();
            }
            else
            {
                player.ChangeVelocityDoor(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = other.gameObject.GetComponent<Character>();
            player.ChangeVelocityDoor(false);
        }
    }
}
