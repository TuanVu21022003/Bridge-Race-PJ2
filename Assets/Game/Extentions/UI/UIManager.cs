using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();

    [SerializeField] private Transform parent;

    private void Awake()
    {
        UICanvas[] prefab = Resources.LoadAll<UICanvas>("UI/");
        for(int i = 0; i < prefab.Length; i ++)
        {
            canvasPrefabs.Add(prefab[i].GetType(), prefab[i]);
        }
    }

    private void Start()
    {
        OpenUI<UIMainMenu>();
    }

    public T OpenUI<T>() where T : UICanvas
    {
         T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();

        return canvas as T;
    }

    public void CloseUI<T>(float time) where T : UICanvas
    {
        if(IsOpenedUI<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }

    public void CloseDirectlyUI<T>() where T : UICanvas
    {
        if (IsOpenedUI<T>())
        {
            canvasActives[typeof(T)].CloseDirectionly();
        }
    }

    public T GetUI<T>() where T : UICanvas
    {
        if(!IsLoadedUI<T>())
        {
            T prefab = GetUIPrefb<T>();
            T canvas = Instantiate(prefab, parent);
            canvasActives[typeof(T)] = canvas;
        }
        return canvasActives[typeof(T)] as T;
    }

    public bool IsLoadedUI<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }

    public bool IsOpenedUI<T>() where T : UICanvas
    {
        return IsLoadedUI<T>() && canvasActives[typeof(T)].gameObject.activeSelf == true;
    }

    private T GetUIPrefb<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;
    }

    public void CloseAllUI()
    {
        foreach(var canvas in canvasActives)
        {
            if (canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}
