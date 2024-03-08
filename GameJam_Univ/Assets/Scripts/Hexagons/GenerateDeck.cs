using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    [SerializeField] GameObject prefabCard = null;
    [SerializeField] GameObject[] roadPrefabCards = null;
    [SerializeField] GameObject endPrefabCard = null;

    private HexagonDragDrop currentCard;

    public void GenerateNewDeck(int roads, int base_cards) {
        // generate card deck
        GameObject card;

        for (int i = 0; i < base_cards; i++) {
            card = Instantiate(prefabCard, transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent, false);
            card.transform.position = this.transform.position;
            card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);
        }

        card = Instantiate(endPrefabCard, transform.position, Quaternion.identity);
        card.transform.SetParent(transform.parent, false);
        card.transform.position = this.transform.position;
        card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);

        for (int i = 0; i < roads; i++) {
            int index = Random.RandomRange(0, roadPrefabCards.Length);
            // int index = i % 2;
            card = Instantiate(roadPrefabCards[index], transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent, false);
            card.transform.position = this.transform.position;
            card.transform.GetChild(0).GetComponent<HexagonDragDrop>().MakeDragable(false);
        }

        // make fisrt card draggable
        currentCard = card.transform.GetChild(0).GetComponent<HexagonDragDrop>();
        currentCard.MakeDragable(true);
    }

    public void DestroyLeftovers() {
        for (int i = 1; i < transform.parent.childCount; i++) {
            Destroy(transform.parent.GetChild(i).gameObject);
        }
    }

    void Update() {
        // make next card draggable
        if (currentCard.placed && transform.parent.childCount > 1) {
            currentCard = transform.parent.GetChild(transform.parent.childCount - 1).GetChild(0).GetComponent<HexagonDragDrop>();
            currentCard.MakeDragable(true);
        }
        else if (currentCard.placed && transform.parent.childCount <= 1) {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().StartEnemyWave();
        }
    }
}
