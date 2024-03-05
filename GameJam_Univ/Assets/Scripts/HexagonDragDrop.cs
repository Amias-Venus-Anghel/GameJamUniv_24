﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using UnityEngine.UI;
using System;

public class HexagonDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // referances
    [SerializeField] private bool starterPoint;
    [SerializeField] private GameObject[] holders = null;
    private CardDeck cardDeck;
    private Image image;
    // variables
    private bool canRotate = false;
    private bool placed = false;
    private int nrRotiri = 0;
    
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
        // if point is start, can't be moved
        if (starterPoint) {
            placed = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // check if card has been placed before movement
        if (!placed) {
            Debug.Log("Begin drag");
            image.raycastTarget = false;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            canRotate = true;
            cardDeck.SetCanRotate(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // check if card has been placed before movement
        if (!placed) {
            Debug.Log("draggin");
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        canRotate = false;
        cardDeck.SetCanRotate(false);
        // reactivate raycastTarget to stop overplacing cards
        image.raycastTarget = true;

        int[] newIndex = new int[6];
        for (int i = 0; i < 6; i++) {
            newIndex[i] = (i + nrRotiri)%6;
        }
        // show placeholders if card is placed
        for (int i = 0; i < 6; i++) {
            if (holders[i] != null) {
                holders[i].GetComponent<PlaceHolders>().SetIndex(newIndex[i]);
                if (placed) {
                    holders[i].GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    public void DestroyOnPos(int index) {
        placed = true;
        // might not be needed to distroy placeholders
        index = (index + 6 - (nrRotiri % 6))%6;

        Destroy(holders[index]);
        Destroy(holders[(index + 1)%6]);
        Destroy(holders[Math.Abs((index - 1)%6)]);
    }

    public bool IsPlaced() {
        return placed;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1) && canRotate) {
            nrRotiri++;
            transform.Rotate(0, 0, -60);
        }
    }
}
