using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using UnityEngine.UI;
using System;

public class HexagonDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // referances
    [SerializeField] private bool starterPoint = false;
    [SerializeField] private GameObject[] holders = null;
    private CardDeck cardDeck;
    private Image image;
    // variables
    private bool canRotate = false;
    private bool placed = false;
    private int nrRotiri = 0;

    // road checks
    [SerializeField] private bool isRoad = false;
    [SerializeField] private int[] roadEndPoints = null; // index of road end points
    
    void Start() {
        image = GetComponent<Image>();
        cardDeck = transform.parent.GetComponent<CardDeck>();
        // initiate placeholders
        for (int i = 0; i < 6; i++) {
            holders[i].AddComponent<PlaceHolders>();
            holders[i].GetComponent<PlaceHolders>().SetIndex(i);
            // if point is not start, hide placeholders
            if (!starterPoint) {
                holders[i].GetComponent<Image>().enabled = false;
            }
        }

        // set road tiles
        if (isRoad) {
            for (int i = 0; i < roadEndPoints.Length; i++) {
                holders[roadEndPoints[i]].GetComponent<PlaceHolders>().SetAsRoad();
            }
        }

        // if point is start, can't be moved
        if (starterPoint) {
            placed = true;
            image.raycastTarget = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        image.raycastTarget = false;
        canRotate = true;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        cardDeck.SetCanRotate(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("draggin");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        canRotate = false;
        cardDeck.SetCanRotate(false);

        // reactivate raycastTarget to stop overplacing cards
        if (!placed) {
            image.raycastTarget = true;
        }

        int[] newIndex = new int[6];
        for (int i = 0; i < 6; i++) {
            newIndex[i] = (i + nrRotiri)%6;
        }
        for (int i = 0; i < 6; i++) {
            if (holders[i] != null) {
                // remake index order
                holders[i].GetComponent<PlaceHolders>().SetIndex(newIndex[i]);
                // show placeholders if card is placed
                if (placed) {
                    holders[i].GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    public void DestroyOnPos(int index) {
        placed = true;

        // move hexagon to world map
        Transform canvasWorld = GameObject.Find("Canvas World").transform;
        transform.SetParent(canvasWorld, false);
        cardDeck.transform.SetParent(canvasWorld, false);
        cardDeck.transform.SetAsFirstSibling();
        transform.SetAsLastSibling();

        // destroy neigh
        index = (index + 6 - (nrRotiri % 6))%6;
        Destroy(holders[index]);
        Destroy(holders[(index + 1)%6]);
        Destroy(holders[Math.Abs((index +5)%6)]);
    }

    public bool PointIsRoadEnd(int index) {
        index = (index + 6 - (nrRotiri % 6))%6;
        
        return holders[index].GetComponent<PlaceHolders>().IsRoadPoint();
    }

    public bool IsRoad() {
        return isRoad;
    }

    void Update() {
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && canRotate) {
            nrRotiri++;
            transform.Rotate(0, 0, -60);
        }
    }
}
