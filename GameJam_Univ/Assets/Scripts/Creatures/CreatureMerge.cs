using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMerge : MonoBehaviour
{
    private Transform mergeWith;
    private float mergeSpeed = 10;
    public string color_cod = "blue";

    public void OnTriggerStay2D (Collider2D other) {
        if (other.CompareTag("Creature")) {
            // start merging if both are creatures and on same canvas
            if (transform.parent.parent.gameObject == other.transform.parent.parent.gameObject) {
                Debug.Log("create can merge");
                mergeWith = other.transform;
            }
        }
    }

    void Update() {
        if (mergeWith != null) {
            var step =  mergeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mergeWith.position, step);
            if (Vector3.Distance(transform.position, mergeWith.position) < 0.001f)
            {
                Debug.Log("Merged");
                // play animation on top of objects
                // destroy one of the creatures
                Destroy(mergeWith.gameObject);
            }
        }
    
    }
}
