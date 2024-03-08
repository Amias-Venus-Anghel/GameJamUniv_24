using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public float range = 0.5f;
    private Transform target;
    private int index = 0;
    private GameObject endCard = null;
    private GameObject hexagonEnd = null;
    private HexagonDragDrop hexagonEndScript;
    private Transform endpoint;
    public static Transform[] points;
    private int index_hexagon;
    private bool isEnd = false;

    void Start() {
        index_hexagon = 1;
        hexagonEnd = GameObject.FindGameObjectsWithTag("Endpoint")[0];
        endpoint = hexagonEnd.transform.Find("waypoint");
        target = endpoint;
        // StartCoroutine(Wait());
    }

    // IEnumerator Wait() {
    //     yield return new WaitForSeconds(5);

        
    // }

    void Update() {
        points = GameObject.Find("Waypoints").GetComponent<AddWaypoints>().points;
        if(Vector3.Distance(transform.position, target.position) <= range) {
            if(target == endpoint) {
                Destroy(gameObject); // de pus endpoint-ul in dreptul cararii
            } else {
                Debug.Log("if if else");
                if(index_hexagon >= points.Length) {
                    Debug.Log("if if else if");
                    isEnd = true;
                } else {
                    Debug.Log("if if else else");
                    index_hexagon++;
                } 
            }
        } else {
            Debug.Log("else");
            if(!isEnd){
                Debug.Log("else if");
                Debug.Log(index_hexagon);
                target = points[points.Length - index_hexagon];
            } else {
                Debug.Log("else else");
                target = endpoint;
            }
        }
        Debug.Log("out of if");
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
}
