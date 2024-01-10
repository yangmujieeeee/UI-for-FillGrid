using System;
using UnityEngine;

public static class EventHandler
{
    /// <summary>
    /// 拿取物体
    /// </summary>
    public static event Action<Item> PickItemEvent;
    public static void CallPickItemEvent(Item Item)
    {
        PickItemEvent?.Invoke(Item);
    }

    /// <summary>
    /// 平台切换
    /// </summary>
    public static event Action PlatformSwitchEvent;
    public static void CallPlatformSwitchEvent()
    {
        PlatformSwitchEvent?.Invoke();
    }

    public static event Action<bool> CanSkipEvent;
    public static void CallCanSkipEvent(bool can)
    {
        CanSkipEvent?.Invoke(can);
    }
}