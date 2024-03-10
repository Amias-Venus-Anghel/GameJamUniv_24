using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HappinessScore : MonoBehaviour
{
    void Start() {
        DataBringer dataBringer = FindObjectOfType<DataBringer>();

        GetComponent<TMP_Text>().text = "This week's happiness: " + dataBringer.finalScore.ToString();
    }
}
