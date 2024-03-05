using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    [SerializeField] int cardsNumber = 10;
    [SerializeField] GameObject prefabCard;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cardsNumber; i++) {
            GameObject card = Instantiate(prefabCard, transform.position, Quaternion.identity);
            card.transform.SetParent(transform.parent);
        }
    }
}
