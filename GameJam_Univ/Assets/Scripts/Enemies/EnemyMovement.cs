using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public float range = 0.5f;
    public int happinessDamage = 300;
    private Transform target;
    private Transform hexagonEnd = null;

    private List<Transform> points;

    private int index_hexagon = 1;

    private WaveSpawner spawner = null;

    void Start() {
        spawner = GameObject.Find("GameMaster").GetComponent<WaveSpawner>();

        index_hexagon = 1;
        hexagonEnd = GameObject.FindGameObjectWithTag("Endpoint").transform;
        points = GameObject.Find("CardsSpawner").GetComponent<GenerateDeck>().waypoints;

        target = points[points.Count - index_hexagon];
    }

    void Update() {
        if(Vector3.Distance(transform.position, target.position) <= 0.5f) { 
            if (target == hexagonEnd) {
                spawner.EnemyDied();
                // add score penality
                GameObject.Find("GameMaster").GetComponent<GameMaster>().AddScore(-happinessDamage);
                hexagonEnd.gameObject.GetComponent<EndPointDamage>().HurtHeartAnimate();
                Destroy(gameObject); // de pus endpoint-ul in dreptul cararii
            }
            else if (index_hexagon >= points.Count) {
                target = hexagonEnd;
            }
            else {
                index_hexagon++;
                target = points[points.Count - index_hexagon];
            }
        }

        var step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }


}
