using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeAnimationCloud : MonoBehaviour
{
    private int uses = 2;
    void Awake() {
        gameObject.SetActive(false);
    }
    public void End() {
        uses--;
        if (uses <= 0) {
            Destroy(this.gameObject);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
