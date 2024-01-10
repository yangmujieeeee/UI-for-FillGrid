using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SlotChanger
{
    public static void SlotChange(GridID currID, GridID targetID)
    {
        currID.x = (int)Mathf.Round(targetID.x + (PosUtils.WorldPosToUIPos(currID.gameObject.transform.position, currID.gameObject.GetComponent<RectTransform>(), InstanceParent.Instance.transform as RectTransform).x - PosUtils.WorldPosToUIPos(targetID.gameObject.transform.position, targetID.gameObject.GetComponent<RectTransform>(), InstanceParent.Instance.transform as RectTransform).x) / 100);
        currID.y = (int)Mathf.Round(targetID.y + (PosUtils.WorldPosToUIPos(targetID.gameObject.transform.position, targetID.gameObject.GetComponent<RectTransform>(), InstanceParent.Instance.transform as RectTransform).y - PosUtils.WorldPosToUIPos(currID.gameObject.transform.position, currID.gameObject.GetComponent<RectTransform>(), InstanceParent.Instance.transform as RectTransform).y) / 100);



    }
}
