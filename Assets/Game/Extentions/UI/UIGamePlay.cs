using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Button buttonSetting;
    [SerializeField] private Button buttonRetry;
    [SerializeField] private Button buttonExit;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Start()
    {
        buttonRetry.onClick.AddListener(() =>
        {
            RetryButton();
        });

        buttonExit.onClick.AddListener(() =>
        {
            ExitButton();
        });

    }
    public void RetryButton()
    {
        PlayManager.instance.DestroyGameLevel();
        PlayManager.instance.SpawnGameLevel(PlayManager.instance.currentLevelIndex);
    }

    public void ExitButton()
    {
        PlayManager.instance.DestroyGameLevel();
        Close(0);
        UIManager.Instance.OpenUI<UIMainMenu>();
    }

    public override void Setup()
    {
        base.Setup();
        levelText.text = $"Level {PlayManager.instance.currentLevelIndex}";
    }

}
