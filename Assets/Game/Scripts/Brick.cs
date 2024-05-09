using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private ColorBrickEnum _colorBrick;
    public ColorBrickEnum colorBrick => _colorBrick;



    public void OnInit(ColorBrickEnum colorBrick)
    {
        this._colorBrick = colorBrick;
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
    public void ChangeColor(ColorBrickEnum color)
    {
        Material materialChange = PlayManager.instance.GetMaterial(color);
        gameObject.GetComponent<MeshRenderer>().material = materialChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        ColorBrickEnum colorPlayer = other.gameObject.GetComponent<Character>().colorPlayer; 
        if (other.gameObject.CompareTag(KeyConstants.Tag_Player) && _colorBrick == colorPlayer)
        {
            OnDespawn();
            other.gameObject.GetComponent<Character>().AddBrick();
        }
    }

    public void ActiveBrick()
    {
        gameObject.SetActive(true);
    }

    public void DeActiveBrick()
    {
        gameObject.SetActive(true);
    }
}
