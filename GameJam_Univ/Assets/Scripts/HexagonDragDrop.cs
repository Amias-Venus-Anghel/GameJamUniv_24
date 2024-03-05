using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using UnityEngine.UI;
using System;

public class HexagonDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool starterPoint;
    [SerializeField] private GameObject[] holders = null;
    private CardDeck cardDeck;
    private Image image;

    private bool canRotate = false;
    private bool placed = false;
    private int nrRotiri = 0;
    

    void Start() {
        image = GetComponent<Image>();
        cardDeck = transform.parent.GetComponent<CardDeck>();
        for (int i = 0; i < 6; i++) {
            holders[i].AddComponent<PlaceHolders>();
            holders[i].GetComponent<PlaceHolders>().SetIndex(i);
            if (!starterPoint) {
                holders[i].GetComponent<Image>().enabled = false;
            }
        }
        if (starterPoint) {
            image.raycastTarget = false;
            placed = true;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
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

        image.raycastTarget = true;

        int[] newIndex = new int[6];
        for (int i = 0; i < 6; i++) {
            newIndex[i] = (i + nrRotiri)%6;
        }

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
