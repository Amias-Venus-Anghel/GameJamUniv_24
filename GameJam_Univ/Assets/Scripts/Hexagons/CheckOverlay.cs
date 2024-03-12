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

}
