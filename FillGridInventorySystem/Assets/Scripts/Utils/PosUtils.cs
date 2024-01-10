using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 坐标工具
/// </summary>
public static class PosUtils
{
    /// <summary>
    /// 世界坐标转屏幕坐标
    /// </summary>
    /// <param name="wp"></param>
    /// <returns></returns>
    public static Vector2 WorldPosToScreenPos(Vector2 wp, RectTransform src)
    {
        // UI相机
        Camera uiCam = null;
        Canvas canvas = src.GetComponentInParent<Canvas>();
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCam = canvas.worldCamera;

        // 屏幕坐标
        Vector2 sp = RectTransformUtility.WorldToScreenPoint(uiCam, wp);

        return sp;
    }

    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    /// <returns></returns>
    public static Vector2 WorldPosToUIPos(Vector2 wp, RectTransform src, RectTransform target)
    {
        // 屏幕坐标
        Vector2 sp = WorldPosToScreenPos(wp, src);

        // UI坐标
        Vector2 lp = ScreenPosToUIPos(sp, target);
        return lp;
    }

    /// <summary>
    /// 屏幕坐标转UI坐标
    /// </summary>
    public static Vector2 ScreenPosToUIPos(Vector2 sp, RectTransform target)
    {
        // 父物体
        if (target.parent != null)
            target = target.parent as RectTransform;

        // UI相机
        Camera uiCam = null;
        Canvas canvas = target.GetComponentInParent<Canvas>();
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCam = canvas.worldCamera;

        // UI位置
        Vector2 lp = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            target,
            sp,
            uiCam,
            out lp
        );

        return lp;
    }

    /// <summary>
    /// 获取鼠标位置
    /// </summary>
    public static Vector2 GetMouseUIPos(RectTransform target)
    {
        // 鼠标屏幕位置
        return ScreenPosToUIPos(Input.mousePosition, target);
    }
}
