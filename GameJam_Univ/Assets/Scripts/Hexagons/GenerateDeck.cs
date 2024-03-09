using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    [SerializeField] GameObject prefabCard = null;
    [SerializeField] GameObject[] roadPrefabCards = null;
    [SerializeField] GameObject endPrefabCard = null;

    private HexagonDragDrop currentCard;
    private bool announce;
    private bool placeEndRoad;

    public List<Transform> waypoints;

    private GameMaster gameMaster;

    void Start() {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    public void GenerateNewDeck(int roads, int base_cards) {
        // generate card deck
        GameObject card;

        // normal cards
        for (int i = 0; i < base_cards; i++) {
            card = Instantiate(prefabCard, transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent, false);
            card.transform.position = this.transform.position;
            card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);
        }
        // endpoint
        card = Instantiate(endPrefabCard, transform.position, Quaternion.identity);
        card.transform.SetParent(transform.parent, false);
        card.transform.position = this.transform.position;
        card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);

        waypoints = new List<Transform>();
        // roads
        for (int i = 0; i < roads; i++) {
            int index = Random.Range(0, roadPrefabCards.Length);
            card = Instantiate(roadPrefabCards[index], transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent, false);
            card.transform.position = this.transform.position;
            // add waypoint
            waypoints.Add(card.transform.GetChild(0));

            card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);
        }

        // make fisrt card draggable
        currentCard = card.transform.GetChild(0).GetComponent<HexagonDragDrop>();
        currentCard.MakeDragable(true);

        announce = true;
        placeEndRoad = false;
    }

    public void DestroyLeftovers() {
        for (int i = 1; i < transform.parent.childCount; i++) {
            Destroy(transform.parent.GetChild(i).gameObject);
        }
    }

    void Update() {
        // make next card draggable
        if (currentCard.placed && transform.parent.childCount > 1) {
            if (placeEndRoad) {
                placeEndRoad = false;
                // announce gamemaster that road has been build
                gameMaster.RoadHasBeenBuild();
            }
            currentCard = transform.parent.GetChild(transform.parent.childCount - 1).GetChild(0).GetComponent<HexagonDragDrop>();
            currentCard.MakeDragable(true);
            placeEndRoad = currentCard.endPoint;
        }
        else if (currentCard.placed && transform.parent.childCount <= 1 && announce) {
            gameMaster.StartEnemyWave();
            announce = false;
        }
    }
}
