using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorBrickOS", menuName = "ScriptableObjects/ColorBrickOS")]

public class ColorBrickOS : ScriptableObject
{
    public List<Material> list = new List<Material>();
}
