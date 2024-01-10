using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包格子
/// </summary>
public class InventorySlot : MonoBehaviour
{
    private Inventory inventory;
    private RectTransform rectTransform;


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(Inventory _inventory)
    {
        inventory = _inventory;
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetSprite(Sprite sp)
    {
        transform.GetChild(1).GetComponent<Image>().sprite = sp;
    }

    /// <summary>
    /// 尝试显示提示
    /// </summary>
    public void TryShowItemTip(ItemGrid grid)
    {
        // print("Show");
    }

    /// <summary>
    /// 放置
    /// </summary>
    public void PutDown(ItemGrid grid)
    {
        // 计算位置差
        Vector2 gridPos = PosUtils.WorldPosToUIPos(grid.GetSlotWorldPos(), grid.RectTrans, rectTransform);
        

        Vector2 offset = rectTransform.anchoredPosition - gridPos;
        Vector2 target = grid.RectTrans.anchoredPosition + offset;

        grid.SetPos(target);
    }
}
