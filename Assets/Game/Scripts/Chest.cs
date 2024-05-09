using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] int doorLevel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = other.gameObject.GetComponent<Character>();
            player.SetFloorIndexCurrent();
            MapController.Instance.GetFloorByLevel(doorLevel - 1).GetComponent<FloorController>().ClearByColor(player.colorPlayer);
            PlayManager.instance.SetCharacterWin(player.gameObject);
            player.Win();
        }
    }
}
