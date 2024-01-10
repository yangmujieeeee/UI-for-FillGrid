using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTimer : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 设置填充
    /// </summary>
    /// <param name="ratio"></param>
    public void SetFillAmount(float ratio)
    {
        image.fillAmount = ratio;
    }
}
