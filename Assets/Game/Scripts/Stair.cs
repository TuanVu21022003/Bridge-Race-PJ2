using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private GameObject brickVisual;

    private ColorBrickEnum brickVisualColor = ColorBrickEnum.GRAY;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = other.gameObject.GetComponent<Character>();


            if (player.countBrick > 0 && player.transform.forward.z > 0) 
                {
                    brickVisual.SetActive(true);
                    ColorBrickEnum colorPlayer = player.colorPlayer;
                    if (colorPlayer != brickVisualColor)
                    {
                        ChangeBrickStair(colorPlayer);
                        player.RemoveBrick();
                        MapController.Instance.GetFloorByLevel(player.floorIndexCurrent).GetComponent<FloorController>().RandomBrickByClolor(player.colorPlayer);
                    }
                }
                else if (player.colorPlayer != brickVisualColor)
                {
                    player.ChangeVelocityStair(true);
                }
            



        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = other.gameObject.GetComponent<Character>();
            player.ChangeVelocityStair(false);
        }
    }

    public void ChangeBrickStair(ColorBrickEnum playerColor)
    {
        brickVisual.GetComponent<MeshRenderer>().material = PlayManager.instance.GetMaterial(playerColor);
        brickVisualColor = playerColor;
    }
}
