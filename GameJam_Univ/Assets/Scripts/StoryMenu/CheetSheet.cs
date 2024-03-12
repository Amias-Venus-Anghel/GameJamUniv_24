using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheetSheet : MonoBehaviour
{
    [SerializeField] Sprite[] sheets = null;
    private Image image;
    private int index = 0;

    void Start() {
        image = GetComponent<Image>();
        image.sprite = sheets[0];
        gameObject.SetActive(false);
    } 

    public void NextSheet() {
        index ++;
        if (index >= sheets.Length) {
            index = 0;
        }
        image.sprite = sheets[index];
    }

    public void CloseOpen() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
}
