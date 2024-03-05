using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private Transform hexagon;
    private bool canRotate; 

    void Start()
    {
        hexagon = transform.GetChild(0);
    }

    

    void Update()
    {
        transform.position = hexagon.position;

        if (Input.GetMouseButtonDown(1) && canRotate) {
            transform.Rotate(0, 0, -60);
        }
    }

    public void SetCanRotate(bool canRotate) {
        this.canRotate = canRotate;
    }
}

