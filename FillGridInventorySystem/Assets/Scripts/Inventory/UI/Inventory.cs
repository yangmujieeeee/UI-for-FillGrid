using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包UI
/// </summary>
public class Inventory : Singleton<Inventory>
{
    public Sprite[] sprites;

    public List<InventorySlot> Slots => inventorySlotList;
    private List<InventorySlot> inventorySlotList;

    protected override void Awake()
    {
        base.Awake();

        // 初始化背包格
        inventorySlotList = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            inventorySlotList[i].Init(this);

            if ((i / 6) % 2 == 0)
                inventorySlotList[i].SetSprite(sprites[i % 2]);
            else
                inventorySlotList[i].SetSprite(sprites[1 - i % 2]);
        }
    }
}
