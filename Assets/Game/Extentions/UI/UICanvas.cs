using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;
    //goi truoc khi duoc active

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if(ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;

            leftBottom.y = 0f;
            rightTop.y = -100f;
            rect.offsetMin = leftBottom;
            rect.offsetMax = rightTop;
        }
    }
    public virtual void Setup()
    {

    }

    //goi sau khi duoc active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectionly), time);
    }

    public virtual void CloseDirectionly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
