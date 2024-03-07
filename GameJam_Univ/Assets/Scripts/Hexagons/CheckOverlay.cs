using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/* Canvas script to check and destroy left over placeholders */
public class CheckOverlay : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    // return true if overlay exists
    public bool Check()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        Debug.Log("nr suprapuneri: " + results.Count);

        if (results.Count <= 1) {
            return false;
        }
        
        // destroy extra components
        for (int i = 1; i < results.Count; i++) {
            Destroy(results[i].gameObject);
            Debug.Log("destroyed somethn");
        }

        return true;
        
    }
}
