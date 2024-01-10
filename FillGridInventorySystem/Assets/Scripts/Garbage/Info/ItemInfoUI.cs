using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : Singleton<ItemInfoUI>
{
    public Image image;

    private void Start()
    {
        Hide();
    }

    /// <summary>
    /// 设置信息
    /// </summary>
    public void SetInfo(Sprite sprite)
    {
        if (sprite != null)
            image.sprite = sprite;
    }

    public void Show()
    {
        image.gameObject.SetActive(true);
    }

    public void Hide()
    {
        image.gameObject.SetActive(false);
    }
}
