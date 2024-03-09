using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckRoadLoop : MonoBehaviour
{
    private List<GameObject> roadPlacers = null;
    private HexagonDragDrop endPoint = null;
    private GameMaster gameMaster = null;

    void Start() {
        roadPlacers = new List<GameObject>();
        endPoint = GameObject.FindGameObjectWithTag("Endpoint").GetComponent<HexagonDragDrop>();
        gameMaster = GetComponent<GameMaster>();
    }

    void Update() {
        roadPlacers = GameObject.FindGameObjectsWithTag("RoadPlace").ToList<GameObject>();
        // if can't place anymore => a loop has occured
        if (roadPlacers.Count <= 0 && !endPoint.placed) {
            Debug.Log("loop");
            gameMaster.NewRound();
        }
    }
}
