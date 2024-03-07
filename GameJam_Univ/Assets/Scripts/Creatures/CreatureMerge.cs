using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureMerge : MonoBehaviour
{
    private Transform mergeWith;
    private CreatureManager manager;
    private Image image;
    private float mergeSpeed = 10;
    [SerializeField] private string color_cod = "blue";
    private string merge_cod;

    void Start() {
        manager = transform.parent.gameObject.GetComponent<CreatureManager>();
        image = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnTriggerStay2D (Collider2D other) {
        if (other.CompareTag("Creature")) {
            // start merging if both are creatures and on same canvas
            if (transform.parent.parent.gameObject == other.transform.parent.parent.gameObject) {
                Debug.Log("in range to merge");
                merge_cod = other.GetComponent<CreatureMerge>().GetColorCode();
                merge_cod = manager.GetCombinationCode(color_cod, merge_cod);

                if (merge_cod != null) {
                    mergeWith = other.transform;
                }
            }
        }
    }

    void Update() {
        if (mergeWith != null && merge_cod != null) {
            var step =  mergeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mergeWith.position, step);
            if (Vector3.Distance(transform.position, mergeWith.position) < 0.001f)
            {
                Debug.Log("Merged");
                // play animation on top of objects
                // destroy one of the creatures
                Destroy(mergeWith.gameObject);
                color_cod = merge_cod;
                image.sprite = manager.GetSpriteOfCode(color_cod);
                merge_cod = null;
            }
        }
    
    }

    public string GetColorCode() {
        return color_cod;
    }

    public void SetColorCode(string color_cod, Sprite sprite) {
        this.color_cod = color_cod;
        if (image == null) {
            image = transform.GetChild(0).GetComponent<Image>();
        }
        image.sprite = sprite;
    }
}
