using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    [SerializeField] int roadCardsNumber = 3;
    [SerializeField] int cardsNumber = 10;
    [SerializeField] GameObject prefabCard;
    [SerializeField] GameObject[] roadPrefabCards;
    [SerializeField] GameObject endPrefabCard;
    
    void Start()
    {
        GameObject card;

        for (int i = 0; i < cardsNumber; i++) {
            card = Instantiate(prefabCard, transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent);
        }

        card = Instantiate(endPrefabCard, transform.position, Quaternion.identity);
        card.transform.SetParent(transform.parent);

        for (int i = 0; i < roadCardsNumber; i++) {
            int index = Random.RandomRange(0, roadPrefabCards.Length);
            card = Instantiate(roadPrefabCards[index], transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent);
        }
    }
}
