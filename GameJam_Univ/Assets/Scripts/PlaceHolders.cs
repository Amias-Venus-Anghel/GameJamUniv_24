using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using System;

public class PlaceHolders : MonoBehaviour, IDropHandler
{
    [SerializeField] private int index;

    public void OnDrop(PointerEventData eventData)
    {   
        GameObject dropped = eventData.pointerDrag;
        if (dropped.GetComponent<HexagonDragDrop>().IsPlaced())
            return;

        dropped.GetComponent<HexagonDragDrop>().DestroyOnPos((index + 3)%6);

        dropped.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

    public void SetIndex(int index) {
        this.index = index;
    }

}
