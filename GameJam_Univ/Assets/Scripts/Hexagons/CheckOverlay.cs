using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq.Expressions;

/* Canvas script to check and destroy left over placeholders */
public class CheckOverlay : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // to do: announce from game master when new game to reset this
    List<RectTransform> placeholders; 

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        placeholders = new List<RectTransform>();
    }

    // return true if overlay exists
    public void Check()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        // destroy extra components
        for (int i = 1; i < results.Count; i++) {
            Destroy(results[i].gameObject);
            Debug.Log("Overlay check : destroyed somethn");
        }
    }

    public void Check(Transform positionToCheck)
    {
        Debug.Log("check overlayer");
    
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = new Vector3(positionToCheck.localPosition.x, positionToCheck.localPosition.y, 10000);

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count > 0) 
            Debug.Log("Hit " + results[0].gameObject.name);
        
    }

    // public void AddPlaceholdersForCheck(RectTransform parent) {
    //     Debug.Log("Checking overlay");
    //     if (placeholders == null) {
    //         placeholders =  new List<RectTransform>();
    //     }

    //     for (int i = 0; i < parent.childCount; i++) {
    //         bool overlap = false;
    //         RectTransform child = parent.GetChild(i).GetComponent<RectTransform>();
    //         foreach (RectTransform r in placeholders) {
    //             if (r != null && child != null) {
    //                 if (rectOverlaps(r, child))
    //                 {
    //                     Debug.Log("Overlaps "  + child.gameObject.name);
    //                     overlap = true;
    //                     continue;
    //                 }
    //             }
               
    //         }
    //         // am uitt sa le adaug in lista
    //         if (!overlap) {
    //             placeholders.Add(child);
    //             Debug.Log("added in list " + child.gameObject.name);
    //         }
    //     }
    // }

    // bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    // {
    //     Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
    //     Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

    //     return rect1.Overlaps(rect2);
    // }

}
