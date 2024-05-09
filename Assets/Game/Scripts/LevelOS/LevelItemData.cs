using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemData : MonoBehaviour
{

    [SerializeField] private Text textLevel;
    [SerializeField] private Button buttonLevel;

    public void OnInit(string textIndex, Action<string> buttonAction)
    {
        textLevel.text = "Level " + textIndex;
        buttonLevel.onClick.AddListener(() =>
        {
            buttonAction.Invoke(textIndex);
        });
    }
}


