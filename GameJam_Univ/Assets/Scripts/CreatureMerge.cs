using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMerge : MonoBehaviour
{
    public void OnTrigger2DEnter(Collider2D other) {
        if (other.CompareTag("Creature")) {
            Debug.Log("creature in range for merge");
        }
    }
}
