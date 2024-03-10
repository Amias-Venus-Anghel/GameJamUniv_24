using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] TMP_Text score_text = null;
    [SerializeField] Slider time_slider = null;
    [SerializeField] GameObject startPointPrefab = null;
    
    AudioManager audioManager;

    private Vector3 startPosition;
    private GenerateDeck generator; 
    private Transform worldCanvas;
    private Transform creatureCanvas;

    [SerializeField] private float score = 0;
    [SerializeField] private float roundTime = 10;
    [SerializeField] int roadCardsNumber = 3;
    [SerializeField] int cardsNumber = 5;
    [SerializeField] int enemiesWave = 5;
    [SerializeField] int enemiesSpawnSpeed = 5;

    // count played rounds
    private int dayOfWeek = 0;

    private float endRoundTime = 0;
    private bool enemyStage = false;
    private bool roadIsBuild = false;

    private List<CreaturePositionForAttack> toPositions;
    private List<Image> placeholders = null;
    [SerializeField] private Sprite placeholderReplacer = null;

    private WaveSpawner waveSpawner;

    void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        startPosition = GameObject.Find(" StartPoint").transform.position;
        generator = GameObject.Find("CardsSpawner").GetComponent<GenerateDeck>();
        worldCanvas = GameObject.Find("Canvas World").transform;
        creatureCanvas = GameObject.Find("Canvas Creatures").transform;

        waveSpawner = GetComponent<WaveSpawner>();

        toPositions = new List<CreaturePositionForAttack>();
        NewRound();
    }

    void Update() {
        if (Time.time >= endRoundTime) {
            NewRound();
        }
        // adjust timer
        float timeleft = endRoundTime - Time.time;
        time_slider.value =  timeleft / roundTime;
    }

    // called to start new round
    public void NewRound() {
        // reset time
        endRoundTime = Time.time + roundTime;

        audioManager.PlaySFX(audioManager.startingRound);
        // destroy left over cards
        generator.DestroyLeftovers();

        // adjust difficulty only if level finished
        if (roadIsBuild) {
            // if enemy stage happened
            if (enemyStage) {
                // this is a new round
                dayOfWeek++;
                if (dayOfWeek == 8) {
                    // week ended, go to endscreen
                    FindObjectOfType<DataBringer>().EndGame(score);
                }
                enemyStage = false;
                roadIsBuild = false;
                AdjustDifficulty();
                ClearScene();
            } else {
                // if wave didnt start but road is build and time up, start enemy wave
                StartEnemyWave();
            }
        }
        else {
            // this is a reset if road wasnt finished
            roadIsBuild = false;
            enemyStage = false;
            ClearScene();
        }
        
    }

    private void ClearScene() {
        Camera.main.GetComponent<MoveCamera>().ResetCamera();
        
        waveSpawner.DestroyLeftovers();
        
        if (placeholders == null) {
            placeholders = new List<Image>();
        }

        // destroy placed assets
        for (int i = 1; i < worldCanvas.childCount; i++) {
            Destroy(worldCanvas.GetChild(i).gameObject);
        }

        for (int i = 0; i < creatureCanvas.childCount; i++) {
            Destroy(creatureCanvas.GetChild(i).gameObject);
        }

        // create new start point
        GameObject start = Instantiate(startPointPrefab, worldCanvas);
        start.transform.position = startPosition;

        //  generate new pack
        generator.GenerateNewDeck(roadCardsNumber, cardsNumber);
    }

    public void StartEnemyWave() {
        // add bonus score if time left for hexagon placement
        AddScore((int)(endRoundTime - Time.time));

        endRoundTime = Time.time + roundTime;
        enemyStage = true;

        StartCoroutine(CallToAction());
    }

    private void AdjustDifficulty() {
        Debug.Log("adjusting difficulty");
        // TO DO: ajust nr of card per level
        roadCardsNumber += 1;
        cardsNumber += 1;   
        // adjust nr of enemies and speed of spawning
        enemiesWave += 2;
        if (enemiesSpawnSpeed > 1.4f) {
            enemiesSpawnSpeed -= 1;
        }

        // TO DO: ajust time for round
        roundTime = roundTime + roadCardsNumber * 0.2f + cardsNumber * 0.2f;
    }

    IEnumerator CallToAction() {
        foreach (Image i in placeholders) {
            if (i != null) {
                i.sprite = placeholderReplacer;
            }
        }

        yield return new WaitForSeconds(1);

        // call all existing creatures to positions 
        foreach (CreaturePositionForAttack c in toPositions) {
            // announce creature
            if (c != null) {
                c.ToPosition();
            }
        }

        // yield return new WaitForSeconds(1);
        waveSpawner.spawnEnemyWaves(enemiesWave, enemiesSpawnSpeed);
    }

    // called when enemy is killed
    public void AddScore(int score_add) {
        score += score_add;
        score_text.text = score.ToString();
    }

    public void ListenForWave(CreaturePositionForAttack creature) {
        toPositions.Add(creature);
    }

    public void ListenForWavePlaceholders(Image image) {
        placeholders.Add(image);
    }

    public void AllEnemiesKilled() {
        // add bonus from time left
        // TO DO: check if enemies died by endpoint or killed 
        AddScore((int)(endRoundTime - Time.time));
        endRoundTime = 0;
    }

    public void RoadHasBeenBuild() {
        roadIsBuild = true;
    }
}
