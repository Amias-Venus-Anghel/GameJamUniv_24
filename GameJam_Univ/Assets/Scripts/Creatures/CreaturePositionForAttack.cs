using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePositionForAttack : MonoBehaviour
{
    private float smallestDist = float.PositiveInfinity;
    private Transform closestRoad = null;  
    private bool move = false;
    private float speed = 20f;
    private float positionDistance = 0.001f;
    void Start() {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().ListenForWave(this);
    }

    public void ToPosition() {
        // Destroy(this.GetComponent<CreatureMerge>());
        // GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
        // //  find closest road
        // foreach (GameObject road in roads) {
        //     float dist = (transform.position - road.transform.position).sqrMagnitude;

        //     if (dist < smallestDist) {
        //         smallestDist = dist;
        //         closestRoad = road.transform;
        //     }
        // }
        // //  start moving to target
        // move = true;
        // //  start movement animation maybe
    }

    void Update() {
        // if (move) {
        //     var step =  speed * Time.deltaTime;
        //     transform.position = Vector3.MoveTowards(transform.position, closestRoad.position, step);
        //     if (Vector3.Distance(transform.position, closestRoad.position) < positionDistance)
        //     {
        //         // once creature is on position, destroy script
        //         Destroy(this);
        //     }
        // }
    
    }
}
