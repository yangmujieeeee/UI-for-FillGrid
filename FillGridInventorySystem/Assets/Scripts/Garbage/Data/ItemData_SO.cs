using UnityEngine;

[CreateAssetMenu(fileName = "ItemData_SO", menuName = "Data/ItemData")]
public class ItemData_SO : ScriptableObject
{
    [Header("物体数据")]
    public int ID;
    public string Name;
    public int slotCount;
    public Sprite sprite;
    public Sprite info;
    public ItemType ItemType;
    // public ShapeType shadeType;

    [Header("实例生成")]
    public Item prefab;
}