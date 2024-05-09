using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVictory : UICanvas
{
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonNext;

    private void Start()
    {
        buttonNext.onClick.AddListener(() =>
        {
            NextButton();
        });

        buttonMainMenu.onClick.AddListener(() =>
        {
            MainMenuButton();
        });
    }
    public void NextButton()
    {
        Close(0);
        PlayManager.instance.SetEndGame(false);
        PlayManager.instance.DestroyGameLevel();
        PlayManager.instance.SetCurrentLevelIndex((int.Parse(PlayManager.instance.currentLevelIndex) + 1).ToString());
        PlayManager.instance.SpawnGameLevel(PlayManager.instance.currentLevelIndex);
    }

    public void MainMenuButton()
    {
        Close(0);
        PlayManager.instance.SetEndGame(false);
        PlayManager.instance.DestroyGameLevel();
        UIManager.Instance.OpenUI<UIMainMenu>();
        UIManager.Instance.CloseUI<UIGamePlay>(0);
    }

    public override void Setup()
    {
        base.Setup();
        if(PlayManager.instance.isNextMap())
        {
            buttonNext.interactable = true;
        }
        else
        {
            buttonNext.interactable = false;
        }
    }
}
