using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePositionForAttack : MonoBehaviour
{
    private float smallestDist_2 = float.PositiveInfinity;
    private float smallestDist_1 = float.PositiveInfinity;
    private Transform closestRoad_2 = null;  
    private Transform closestRoad_1 = null; 
    private Vector3 target; 
    private bool move = false;
    private float speed = 20f;
    private float positionDistance = 10f;
    void Start() {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().ListenForWave(this);
        positionDistance = Random.Range(positionDistance / 2, positionDistance);
        // Debug.Log(positionDistance);
        target = transform.position;
    }

    public void ToPosition() {
        Destroy(this.GetComponent<CreatureMerge>());
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
        //  find closest road
        int check = 1;
        foreach (GameObject road in roads) {
            float dist = (transform.position - road.transform.position).sqrMagnitude;
            // check alternativ
            switch(check) {
                case 1:
                    if (dist < smallestDist_1) {
                        smallestDist_1 = dist;
                        closestRoad_1 = road.transform;
                        check = 2;
                    } else if (dist < smallestDist_2) {
                        smallestDist_2 = dist;
                        closestRoad_2 = road.transform;
                    }
                    break;
                case 2:
                    if (dist < smallestDist_2) {
                        smallestDist_2 = dist;
                        closestRoad_2 = road.transform;
                        check = 1;
                    } else if (dist < smallestDist_1) {
                        smallestDist_1 = dist;
                        closestRoad_1 = road.transform;
                    }
                    break;
            }
           
        }
        //  start moving to target
        float x = Random.Range(closestRoad_1.position.x, closestRoad_2.position.x);
        float y = Random.Range(closestRoad_1.position.y, closestRoad_2.position.y);
        // this makes them leave
        // x = Random.Range(x, target.x);
        // y = Random.Range(y, target.y);
        target = new Vector3(x, y, 0);
        move = true;
        //  start movement animation maybe
    }

    void Update() {
        if (move) {
            var step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < positionDistance)
            {
                // once creature is on position, destroy script
                // Destroy(this);
                move = false;
            }
        }
    }

    public void HappyLeave() {
        float x = 100;
        float y = 100;
        x = Random.Range(x, target.x);
        y = Random.Range(y, target.y);
        target = new Vector3(x, y, 0);
        move = true;
    }
 
}
