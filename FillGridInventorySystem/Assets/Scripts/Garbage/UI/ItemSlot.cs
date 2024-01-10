using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 一个物体格子
/// </summary>
public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// IPointerEnterHandler, IPointerExitHandler
{
    private int ID;

    // 跟随鼠标移动
    private ItemGrid grid;
    private bool isDragging;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(ItemGrid _grid, int _ID)
    {
        grid = _grid;
        ID = _ID;
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragging) return;

        isDragging = true;
        grid.BeginDrag(eventData, ID);
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        grid.Drag(eventData);
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        isDragging = false;
        grid.EndDrag(eventData);
    }

    // /// <summary>
    // /// 鼠标移入
    // /// </summary>
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     grid.PointerEnter();
    // }

    // /// <summary>
    // /// 鼠标退出
    // /// </summary>
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     grid.PointerExit();
    // }
}
