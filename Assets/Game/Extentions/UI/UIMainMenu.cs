using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonSetting;

    private void Start()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            PlayButton();
        });

        buttonResume.onClick.AddListener(() =>
        {
            ResumeButton();
        });
    }

    private void ResumeButton()
    {
        Close(0);
        PlayManager.instance.SpawnGameLevel(PlayerPrefs.GetString(KeyConstants.KEY_LEVEL_USER));
    }

    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<UISelectionMenu>();
    }

    public override void Setup()
    {
        base.Setup();
        if (PlayerPrefs.HasKey(KeyConstants.KEY_LEVEL_USER))
        {
            buttonResume.interactable = true;
            
        }
        else
        {
            buttonResume.interactable = false;
        }
    }
}
