using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaypoints : MonoBehaviour
{
    public static GameObject[] cards;
    public Transform[] points;
    int i = 0;
    void Start() {
        StartCoroutine(Wait());
    }

    
    IEnumerator Wait() {
        yield return new WaitForSeconds(2);

        cards = new GameObject[GameObject.FindGameObjectsWithTag("Card").Length];
        points = new Transform[cards.Length];
        cards  = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in cards) {
            points[i] = card.transform.Find("waypoint");
            i++;
        }
    }
}
