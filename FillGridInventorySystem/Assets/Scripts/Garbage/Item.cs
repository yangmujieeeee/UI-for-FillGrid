using System.IO.Compression;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    //ItemSo
    public ItemData_SO ItemData_SO;
    //自己块ID
    public List<GridID> MySlot = new List<GridID>();
    //生成的物体
    public int EndItemID;
    //能够被消除
    public bool CanBeElimiate;
    public GridID BeDragedGrid;

    private ItemGrid grid;

    void Awake()
    {
        for (int i = 0; i < transform.Find("Panel_Slots").transform.childCount; i++)
        {
            transform.Find("Panel_Slots").GetComponentsInChildren<GridID>()[i].IDinItem = i;
            MySlot.Add(transform.Find("Panel_Slots").GetComponentsInChildren<GridID>()[i]);
        }

        if (grid == null)
            grid = GetComponent<ItemGrid>();
    }

    public void Init(int ID)
    {
        if (grid == null)
            grid = GetComponent<ItemGrid>();

        ItemData_SO data = ItemFactory.Instance.GetItemDataByID(ID);
        ItemData_SO = data;
    }

    /// <summary>
    /// 能否被放下，每帧调用/按下调用
    /// </summary>
    /// <returns></returns>
    public bool CanBePutDown()
    {
        foreach (GridID gridID in MySlot)
        {
            // print("x: " + gridID.x + " | y: " + gridID.y);
            if (gridID.x > MapManager.Instance.Max_x || gridID.y > MapManager.Instance.Max_y || gridID.x < 0 || gridID.y < 0)
                return false;
        }

        foreach (GridID gridID in MySlot)
        {
            if (gridID.x > 5 || gridID.y > 5)
                return false;

            Debug.Log(gridID.x);
            Debug.Log(gridID.y);

            if (MapManager.Instance.m_Map[gridID.x, gridID.y].IsFilled == true)
            {
                Debug.Log(gridID.x);
                Debug.Log(gridID.y);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// slot数据的转换，按下调用
    /// </summary>
    public void DoSlotData(GridID currGrid)
    {
        foreach (var SlotData in MySlot)
        {
            if (SlotData.IDinItem != currGrid.IDinItem)
                SlotChanger.SlotChange(SlotData, currGrid);
        }
    }

    /// <summary>
    /// 移除
    /// </summary>
    public void Remove()
    {
        MapManager.Instance.CurrItemList.Remove(this);
        if (grid != null)
        {
            grid.Remove();
        
        }
        Destroy(gameObject);

    }

}
