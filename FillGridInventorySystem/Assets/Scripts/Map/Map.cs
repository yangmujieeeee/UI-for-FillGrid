using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //地图
    public struct MapData
    {
        public int x;
        public int y;
        public bool IsFilled;
    }
    public static Map Instance;
    public Map.MapData[,] m_Map = new Map.MapData[6, 6];
    public int m_x;
    public int m_y;
    void Awake()
    {
        Instance = this;
    }
}
