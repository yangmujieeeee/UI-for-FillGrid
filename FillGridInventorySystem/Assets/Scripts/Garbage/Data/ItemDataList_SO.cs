using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Data/ItemDataList")]
public class ItemDataList_SO : ScriptableObject
{
    [Header("物体数据")]
    public List<ItemData_SO> ItemDataList;
}