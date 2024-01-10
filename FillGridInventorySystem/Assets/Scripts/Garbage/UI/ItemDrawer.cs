using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrawer : MonoBehaviour
{
    [Header("绘制UI")]
    public Image body;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="ItemData"></param>
    public void Init(ItemData_SO ItemData)
    {
        if (ItemData.sprite != null)
            body.sprite = ItemData.sprite;
    }
}
