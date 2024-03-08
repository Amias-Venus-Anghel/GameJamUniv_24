using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] TMP_Text score_text = null;
    [SerializeField] TMP_Text time_text = null;
    [SerializeField] GameObject startPointPrefab = null;

    private Vector3 startPosition;
    private GenerateDeck generator; 
    private Transform worldCanvas;
    private Transform creatureCanvas;

    [SerializeField] private float score = 0;
    [SerializeField] private float roundTime = 10;
    [SerializeField] int roadCardsNumber = 3;
    [SerializeField] int cardsNumber = 5;

    private float endRoundTime = 0;

    void Start() {
        startPosition = GameObject.Find(" StartPoint").transform.position;
        generator = GameObject.Find("CardsSpawner").GetComponent<GenerateDeck>();
        worldCanvas = GameObject.Find("Canvas World").transform;
        creatureCanvas = GameObject.Find("Canvas Creatures").transform;
        NewRound();
    }

    void Update() {
        if (Time.time >= endRoundTime) {
            NewRound();
        }
        time_text.text = (endRoundTime - Time.time).ToString();
    }

    // called to start new round
    public void NewRound() {
        // destroy placed assets
        for (int i = 0; i < worldCanvas.childCount; i++) {
            Destroy(worldCanvas.GetChild(i).gameObject);
        }
        for (int i = 0; i < creatureCanvas.childCount; i++) {
            Destroy(creatureCanvas.GetChild(i).gameObject);
        }

        // create new start point
        GameObject start = Instantiate(startPointPrefab, worldCanvas);
        start.transform.position = startPosition;
        // generate new 
        generator.DestroyLeftovers();
        // TO DO: ajust nr of card per level
        roadCardsNumber += 1;
        cardsNumber += 1;
        generator.GenerateNewDeck(roadCardsNumber, cardsNumber);

        // debug score
        AddScore((int)(endRoundTime - Time.time));
        // reset time
        // TO DO: ajust time for round
        roundTime = roundTime + roadCardsNumber * 0.2f + cardsNumber * 0.2f;
        endRoundTime = Time.time + roundTime;
    }

    // called when enemy is killed
    public void AddScore(int score_add) {
        score += score_add;
        score_text.text = score.ToString();
    } 
}
