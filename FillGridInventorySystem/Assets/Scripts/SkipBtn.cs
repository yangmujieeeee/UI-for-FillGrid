using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipBtn : MonoBehaviour
{
    private Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.interactable = false;
    }


    private void OnEnable()
    {
        EventHandler.CanSkipEvent += OnCanSkipEvent;
    }

    private void OnDisable()
    {
        EventHandler.CanSkipEvent -= OnCanSkipEvent;
    }

    private void OnCanSkipEvent(bool canSkip)
    {
        btn.interactable = canSkip;
    }
}
