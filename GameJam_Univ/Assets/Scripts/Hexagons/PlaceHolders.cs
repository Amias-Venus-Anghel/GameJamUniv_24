using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


public class PlaceHolders : MonoBehaviour, IDropHandler
{
    [SerializeField] private int index = 0;
    private bool isRoadEndPoint;
    AudioManager audioManager;

    void Start() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        GameObject.Find("GameMaster").GetComponent<GameMaster>().ListenForWavePlaceholders(GetComponent<Image>());
    }

    public void OnDrop(PointerEventData eventData)
    {   
        HexagonDragDrop dropped = eventData.pointerDrag.GetComponent<HexagonDragDrop>();

        if (dropped.placed) {
            return;
        }

        GameObject.Find("Canvas World").GetComponent<CheckOverlay>().Check();

        // road compatibility checks
        if (dropped.IsRoad()) {
            // if road but not road point
            if (!dropped.PointIsRoadEnd((index + 3)%6)) {
                audioManager.PlaySFX(audioManager.wrong_place);
                return;
            } 
            // is road point but not positioned to a road point
            else if (!isRoadEndPoint) {
                audioManager.PlaySFX(audioManager.wrong_place);
                return;
            }
        }
        // is not road but trying to connect to a road point
        else if (isRoadEndPoint) {
            audioManager.PlaySFX(audioManager.wrong_place);
            return;
        }

        // Debug.Log("placed on pos " + index);
        dropped.DestroyOnPos((index + 3)%6);
        dropped.transform.position = this.transform.position;
        // set tags for loop check
        dropped.SetTagsOnRoadEnds();
        audioManager.PlaySFX(audioManager.placed);
        Camera.main.GetComponent<MoveCamera>().IncreaseMaxSize(dropped.transform.position);
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
