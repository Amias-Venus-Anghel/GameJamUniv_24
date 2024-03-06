using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class PlaceHolders : MonoBehaviour, IDropHandler
{
    [SerializeField] private int index = 0;
    private bool isRoadEndPoint;

    public void OnDrop(PointerEventData eventData)
    {   
        HexagonDragDrop dropped = eventData.pointerDrag.GetComponent<HexagonDragDrop>();

        // road compatibility checks
        if (dropped.IsRoad()) {
            // if road but not road point
            if (!dropped.PointIsRoadEnd((index + 3)%6)) {
                return;
            } 
            // is road point but not positioned to a road point
            else if (!isRoadEndPoint) {
                return;
            }
        }
        // is not road but trying to connect to a road point
        else if (isRoadEndPoint) {
            return;
        }

        GameObject.Find("Canvas").GetComponent<CheckOverlay>().Check();
        // Debug.Log("placed on pos " + index);
        dropped.DestroyOnPos((index + 3)%6);
        dropped.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

    public void SetIndex(int index) {
        this.index = index;
    }   

    public void SetAsRoad() {
        this.isRoadEndPoint = true;
    }
    public bool IsRoadPoint() {
        return isRoadEndPoint;
    }

}
