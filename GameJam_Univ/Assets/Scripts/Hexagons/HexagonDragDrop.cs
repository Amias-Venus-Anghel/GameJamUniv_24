using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class HexagonDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // referances
    [SerializeField] private bool starterPoint = false;
    [SerializeField] private bool endPoint = false;
    [SerializeField] private GameObject[] holders = null;
    private CardDeck cardDeck;
    private Image image;
    private Transform creatures;
    // variables
    private bool canRotate = false;
    public bool placed = false;
    private int nrRotiri = 0;

    // road checks
    [SerializeField] private bool isRoad = false;
    [SerializeField] private int[] roadEndPoints = null; // index of road end points
    
    void Awake() {
        image = GetComponent<Image>();
        cardDeck = transform.parent.GetComponent<CardDeck>();
        if (!endPoint) {
            // end point doesnt have creatures
            creatures = transform.parent.GetChild(transform.parent.childCount - 1);
        }

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
        image.raycastTarget = false;
        canRotate = true && !placed;
        // move to world canvas
        Transform canvasWorld = GameObject.Find("Canvas World").transform;
        transform.SetParent(canvasWorld, false);
        cardDeck.transform.SetParent(canvasWorld, false);
        cardDeck.transform.SetAsFirstSibling();
        transform.SetAsLastSibling();
        if (!endPoint) {
            creatures.transform.SetParent(canvasWorld);
            creatures.SetAsLastSibling();
        }
        cardDeck.SetCanRotate(canRotate);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placed) {
            return;
        }

        // transform position of mouse to world position
        Vector3 pos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        if (!endPoint) {
            creatures.position = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canRotate = false;
        cardDeck.SetCanRotate(canRotate);
        if (!endPoint) {
            creatures.position = transform.position;
            // if placed, move creatures to their canvas
            if (placed) {
                creatures.SetParent(GameObject.Find("Canvas Creatures").transform);
            }
        }

        image.raycastTarget = true;

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
        if (placed) {
            return;
        }

        placed = true;
        
        if (endPoint) {
            // destroy all holders if it s endpoint
            for (int i = 0; i < 6; i++) {
                Destroy(holders[i]);
            }    
            return;
        }

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
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R)) && canRotate) {
            nrRotiri++;
            transform.Rotate(0, 0, -60);
            if (!endPoint) {
                creatures.Rotate(0, 0, -60);
                // rotate creature sprites;
                for (int i = 0; i < creatures.childCount; i++) {
                    creatures.GetChild(i).GetChild(0).Rotate(0, 0, 60);
                }
            }

        }
    }

    public void MakeDragable(bool draggable) {
        image.raycastTarget = draggable;
    }
}
