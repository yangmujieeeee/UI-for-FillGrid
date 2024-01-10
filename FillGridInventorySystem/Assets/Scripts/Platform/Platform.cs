using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 生成货车
/// </summary>
public class Platform : MonoBehaviour
{
    [Header("生成范围")]
    public Vector2Int spawnRange;

    [Header("Slot位置")]
    public Transform slotParent;
    private RectTransform[] slotsTransform;

    // Slot宽高参数
    private float slotSize;

    // 生成数量
    private int spawnCount;
    private int spawnTimes;
    private int maxSpawnTimes = 10;

    // 物体列表
    private List<Item> ItemList = new List<Item>();

    // 起始位置
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 verticalPosRange = new Vector2(690f, 338f);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // 计算父物体
        spawnCount = slotParent.childCount;

        // 初始化Slot父物体表
        slotsTransform = new RectTransform[spawnCount];
        for (int i = 0; i < spawnCount; i++)
            slotsTransform[i] = slotParent.GetChild(i) as RectTransform;

        // 初始化Slot大小
        slotSize = slotsTransform[0].rect.width;
    }

    private void Start()
    {
        SpawnItems();
    }

    private void OnEnable()
    {
        EventHandler.PickItemEvent += OnPickItemEvent;
    }

    private void OnDisable()
    {
        EventHandler.PickItemEvent -= OnPickItemEvent;
    }



    /// <summary>
    /// 拿取物体
    /// </summary>
    /// <param name="Item"></param>
    private void OnPickItemEvent(Item Item)
    {
        Item target = ItemList.Find(v => v == Item);

        if (target != null)
        {
            print("Pick");
            ItemList.Remove(target);
        }

        if (ItemList.Count < 1)
            EventHandler.CallCanSkipEvent(true);
    }



    /// <summary>
    /// 生成物体
    /// </summary>
    public void SpawnItems()
    {
        spawnTimes = 0;
        int[] spawnIDs = TrySpawnItems();

        for (int i = 0; i < spawnCount; i++)
        {
            int id = spawnIDs[i];
            ItemData_SO ItemData = ItemFactory.Instance.GetItemDataByID(id);

            // 记录
            Item Item = ItemFactory.Instance.InstantiateItem(id, slotsTransform[i]);
            ItemList.Add(Item);

            // 设置Slot缩放大小
            ItemGrid grid = Item.GetComponent<ItemGrid>();
            float maxLength = Mathf.Max(grid.RectTrans.rect.width, grid.RectTrans.rect.height);
            float multiplier = slotSize / maxLength;
            slotsTransform[i].localScale = Vector3.one * multiplier;
        }
    }

    /// <summary>
    /// 尝试按规则生成
    /// </summary>
    private int[] TrySpawnItems()
    {
        spawnTimes++;

        int[] spawnIDs = new int[spawnCount];
        int checkSum = 0;
        for (int i = 0; i < spawnCount; i++)
        {
            // 随机下标
            int randIndex = Random.Range(0, 8);

            // 找到数据
            ItemData_SO ItemData = ItemFactory.Instance.ItemDataList_SO.ItemDataList[randIndex];

            // 存储ID
            spawnIDs[i] = ItemData.ID;

            // 添加校验
            checkSum += ItemData.slotCount;
        }

        // 重新生成
        if ((checkSum < spawnRange.x || checkSum > spawnRange.y) && spawnTimes < maxSpawnTimes)
            spawnIDs = TrySpawnItems();
        else
        {
            // 相同道具
            for (int i = 0; i < spawnCount; i++)
            {
                int count = 0;
                for (int j = 0; j < spawnCount; j++)
                {
                    if (i != j && spawnIDs[i] == spawnIDs[j])
                        count++;
                }

                if (count > 2)
                {
                    spawnIDs = TrySpawnItems();
                    break;
                }
            }
        }

        return spawnIDs;
    }

}
