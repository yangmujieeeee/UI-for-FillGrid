using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// 一个物体板块
/// </summary>
public class ItemGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 物体
    private Item Item;

    // 物体格子
    public List<ItemSlot> SlotList => slotList;
    private List<ItemSlot> slotList;

    // 拖拽
    public RectTransform RectTrans => rectTransform;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 offset;
    private Vector2 startPoint;
    private Transform startParent;

    /// 物体被选中
    public bool IsSelected => isSelected;
    private bool isSelected;

    // 当前格子编号
    public int SlotID => slotID;
    private int slotID;

    // 射线检测
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private bool hasPicked;
    private int rotateFlag = 0;

    private int startFlag;
    private List<GridID> currList = new List<GridID>();

    private void Awake()
    {
        // 必要组件
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        Item = GetComponent<Item>();

        raycaster = GetComponentInParent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();

        // 初始化格子
        slotList = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());
        for (int i = 0; i < slotList.Count; i++)
            slotList[i].Init(this, i);
    }

    private void Update()
    {
        // 选中时检测背包是否可达
        if (isSelected)
        {
            DragCheck();
            DoRotate();
        }
    }

    /// <summary>
    /// 获取当前格的世界位置
    /// </summary>
    public Vector2 GetSlotWorldPos()
    {
        return slotList[slotID].transform.position;
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="target"></param>
    public void SetPos(Vector2 target)
    {
        rectTransform.anchoredPosition = target;
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    public void BeginDrag(PointerEventData eventData, int ID)
    {

        if (isSelected) return;

        startFlag = rotateFlag;

        ////////地图位置的修改
        if (MapManager.Instance.CurrItemList.Contains(this.GetComponent<Item>()))
        {
            foreach (var gridID in this.GetComponent<Item>().MySlot)
            {
                currList.Add(gridID);
                MapManager.Instance.m_Map[gridID.x, gridID.y].IsFilled = false;
            }
            foreach (var gridID in currList)
            {
                MapManager.Instance.m_Map[gridID.x, gridID.y].IsFilled = false;
            }
        }
        MapManager.Instance.CurrItemList.Remove(this.GetComponent<Item>());
        // 选中
        isSelected = true;
        slotID = ID;

        // 父物体
        startParent = transform.parent;
        transform.SetParent(InstanceParent.Instance.transform);
        transform.SetAsLastSibling();
        transform.localScale = Vector3.one;

        // 记录
        startPoint = rectTransform.anchoredPosition;
        offset = startPoint - PosUtils.GetMouseUIPos(rectTransform);

        // 取消碰撞
        canvasGroup.blocksRaycasts = false;

        // 从车上取用
        if (!hasPicked)
        {
            // offset = Vector2.zero;
            // SetPos(PosUtils.GetMouseUIPos(rectTransform));
        }

        Cursor.visible = false;
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    public void Drag(PointerEventData eventData)
    {

        if (!isSelected) return;
        // 应用偏移
        Vector2 mousPos = PosUtils.GetMouseUIPos(rectTransform);
        rectTransform.anchoredPosition = mousPos + offset;
    }

    /// <summary>
    /// 拖拽检查
    /// </summary>
    private void DragCheck()
    {
        GameObject hitObject = RaycastCheck();
        if (hitObject != null)
        {
            // 背包格子
            if (hitObject.TryGetComponent(out InventorySlot slot))
            {
                // 显示提示
                slot.TryShowItemTip(this);
            }
        }
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void EndDrag(PointerEventData eventData)
    {
        if (!isSelected) return;
        int m = slotList[slotID].gameObject.GetComponent<GridID>().x;
        int n = slotList[slotID].gameObject.GetComponent<GridID>().y;
        Cursor.visible = true;

        // *** 射线检测 ***
        GameObject hitObject = RaycastCheck();
        // GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

        // print(hitObject.name);

        bool hasPutDown = false;
        if (hitObject != null)
        {
            // 背包格子
            if (hitObject.TryGetComponent(out InventorySlot slot))
            {
                ////////////转化为地图坐标
                slotList[slotID].gameObject.GetComponent<GridID>().x = hitObject.GetComponent<Map>().m_x;
                slotList[slotID].gameObject.GetComponent<GridID>().y = hitObject.GetComponent<Map>().m_y;
                this.GetComponent<Item>().DoSlotData(slotList[slotID].gameObject.GetComponent<GridID>());
                if (this.GetComponent<Item>().CanBePutDown())
                {
                    // 放置
                    hasPutDown = true;
                    slot.PutDown(this);
                    //////加入到地图数列中去
                    MapManager.Instance.CurrItemList.Add(this.GetComponent<Item>());
                    foreach (var gridID in this.GetComponent<Item>().MySlot)
                    {
                        MapManager.Instance.m_Map[gridID.x, gridID.y].IsFilled = true;
                    }
                }

                // 通知拿取
                if (!hasPicked)
                {
                    EventHandler.CallPickItemEvent(Item);
                    hasPicked = true;
                }
            }
        }

        // 取消选择
        isSelected = false;

        // 恢复碰撞
        canvasGroup.blocksRaycasts = true;

        // 恢复位置
        if (!hasPutDown)
        {
            slotList[slotID].gameObject.GetComponent<GridID>().x = m;
            slotList[slotID].gameObject.GetComponent<GridID>().y = n;
            this.GetComponent<Item>().DoSlotData(slotList[slotID].gameObject.GetComponent<GridID>());
            rectTransform.anchoredPosition = startPoint;

            transform.SetParent(startParent);
            transform.localScale = Vector3.one;
            MapManager.Instance.CurrItemList.Remove(this.GetComponent<Item>());
            if (MapManager.Instance.CurrItemList.Contains(this.GetComponent<Item>()))
            {
                foreach (var gridID in currList)
                {
                    MapManager.Instance.m_Map[gridID.x, gridID.y].IsFilled = true;
                }
            }

            rotateFlag = startFlag;
            transform.rotation = Quaternion.Euler(Vector3.forward * rotateFlag * 90f);
        }

        slotID = -1;
    }

    /// <summary>
    /// 背包检测
    /// </summary>
    private GameObject RaycastCheck()
    {
        // 设置eventData
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = PosUtils.WorldPosToScreenPos(GetSlotWorldPos(), rectTransform);
        // eventData.position = Input.mousePosition;

        // 获取检测结果
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        if (results.Count > 0)
        {
            GameObject hitObject = results[0].gameObject;
            return hitObject;
        }

        return null;
    }

    /// <summary>
    /// 鼠标移入
    /// </summary>
    // public void PointerEnter()
    // {

    // }

    /// <summary>
    /// 鼠标退出
    /// </summary>
    // public void PointerExit()
    // {

    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item.ItemData_SO.info != null)
        {
            ItemInfoUI.Instance.SetInfo(Item.ItemData_SO.info);
            ItemInfoUI.Instance.Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemInfoUI.Instance.Hide();
    }

    /// <summary>
    /// 移除处理
    /// </summary>
    public void Remove()
    {
        if (isSelected)
        {
            Cursor.visible = true;
            ItemInfoUI.Instance.Hide();
        }
    }

    public void DoRotate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotateFlag++;
            //Debug.Log(transform.rotation.z);
            // this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, rotateFlag * 90), 0.2f);

            transform.rotation = Quaternion.Euler(Vector3.forward * rotateFlag * 90f);
        }
    }
}