using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComUI : Singleton<ComUI>
{
    public Image image;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = image.GetComponent<RectTransform>();
    }

    /// <summary>
    /// 获取新物品
    /// </summary>
    public void GetNewItem(int ID)
    {
        // 物品图片
        ItemData_SO ItemData = ItemFactory.Instance.GetItemDataByID(ID);
        Sprite sprite = ItemData.sprite;

        if (sprite == null) return;

        image.sprite = sprite;
        image.SetNativeSize();

        // 动效
        rectTransform.DOKill();
        canvasGroup.DOKill();

        canvasGroup.DOFade(1f, 0.2f);
        rectTransform.DOScale(Vector3.one * 0.8f, 0.2f).OnComplete(() =>
        {
            rectTransform.DOScale(Vector3.one * 1.2f, 0.4f).SetEase(Ease.OutBack);
            canvasGroup.DOFade(0f, 0.2f).SetDelay(0.8f);
        });
    }
}
