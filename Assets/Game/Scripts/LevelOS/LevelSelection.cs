using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Transform parentPosition;
    [SerializeField] private LevelItemData buttonItemPrefab;
    [SerializeField] private LevelOS levelData;

    private void Start()
    {
        SetLevelList();
    }

    public void SetLevelItem(string textIndex)
    {
        LevelItemData button = Instantiate(buttonItemPrefab, parentPosition);
        button.OnInit(textIndex, OnHandleButtonLevel);
    }

    public void SetLevelList()
    {
        for(int i = 0; i < levelData.list.Count; i++)
        {
            SetLevelItem(levelData.list[i].levelIndex.ToString());
        }
    }

    public void OnHandleButtonLevel(string textIndex)
    {
        UIManager.Instance.CloseUI<UISelectionMenu>(0);
        PlayManager.instance.SetCurrentLevelIndex(textIndex);
        UIManager.Instance.OpenUI<UIGamePlay>();
        PlayManager.instance.SpawnGameLevel(textIndex);


    }
}
