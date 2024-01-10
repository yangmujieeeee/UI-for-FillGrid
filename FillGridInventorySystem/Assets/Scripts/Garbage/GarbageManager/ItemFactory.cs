using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    [Header("生成物体")]
    public ItemDataList_SO ItemDataList_SO;

    // 生成字典
    private Dictionary<int, ItemData_SO> ItemDataDic = new Dictionary<int, ItemData_SO>();

    protected override void Awake()
    {
        base.Awake();

        // 初始化物体字典
        for (int i = 0; i < ItemDataList_SO.ItemDataList.Count; i++)
        {
            ItemData_SO ItemData = ItemDataList_SO.ItemDataList[i];
            int ItemID = ItemData.ID;

            if (!ItemDataDic.ContainsKey(ItemID))
                ItemDataDic.Add(ItemData.ID, ItemData);
            else
                Debug.LogWarning("物体ID重复");
        }
    }

    /// <summary>
    /// 通过ID找到物体数据
    /// </summary>
    public ItemData_SO GetItemDataByID(int id)
    {
        if (ItemDataDic.ContainsKey(id))
            return ItemDataDic[id];
        else
        {
            Debug.LogWarning("无此物体ID");
            return null;
        }
    }

    /// <summary>
    /// 生成单个物体
    /// </summary>
    /// <param name="ItemID">ID</param>
    /// <returns></returns>    
    public Item InstantiateItem(int ItemID)
    {
        ItemData_SO ItemData = GetItemDataByID(ItemID);

        Item Item = Instantiate(ItemData.prefab, InstanceParent.Instance.transform);

        // 初始化
        Item.Init(ItemID);
        ItemDrawer drawer = Item.GetComponent<ItemDrawer>();
        drawer.Init(ItemData);

        return Item;
    }

    /// <summary>
    /// 生成单个物体
    /// </summary>
    /// <param name="ItemID">ID</param>
    /// <param name="parent">父物体</param>
    /// <returns></returns>
    public Item InstantiateItem(int ItemID, Transform parent)
    {
        ItemData_SO ItemData = GetItemDataByID(ItemID);

        Item Item = Instantiate(ItemData.prefab, parent);

        // 初始化
        Item.Init(ItemID);
        ItemDrawer drawer = Item.GetComponent<ItemDrawer>();
        drawer.Init(ItemData);

        return Item;
    }
}
