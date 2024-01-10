using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //当前地图上的物体
    public List<Item> CurrItemList = new List<Item>();
    public static MapManager Instance;
    public int Max_x = 5;
    public int Max_y = 5;
    public Map.MapData[,] m_Map = new Map.MapData[6, 6];
    void Awake()
    {
        Instance = this;
        for (int i = 0; i < 36; i++)
        {
            this.transform.GetComponentsInChildren<Map>()[i].m_x = i % 6;
            this.transform.GetComponentsInChildren<Map>()[i].m_y = Mathf.FloorToInt(i / 6);
        }
    }
}
