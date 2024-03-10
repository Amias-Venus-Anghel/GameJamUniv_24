using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowStorySlides : MonoBehaviour
{
    [SerializeField] private Image[] images = null;
    [SerializeField] private GameObject buttonNext = null;
    [SerializeField] private GameObject buttonPrev = null;

    private int indexActual;

    void Start() {
        indexActual = 0;
        // hide all but first image
        for (int i = 1; i < images.Length; i++) {
            images[i].gameObject.SetActive(false);
        }
        buttonPrev.SetActive(false);
    }

    public void NextSlide() {
        if (indexActual + 1 < images.Length) {
            images[indexActual].gameObject.SetActive(false);
            indexActual++;
            images[indexActual].gameObject.SetActive(true);
            
            if (indexActual == images.Length - 1) {
                buttonNext.SetActive(false);
            }
        }

        if (indexActual > 0) {
            buttonPrev.SetActive(true);
        }
    }

    public void PrevSlide() {
        if (indexActual - 1 >= 0) {
            images[indexActual].gameObject.SetActive(false);
            indexActual--;
            images[indexActual].gameObject.SetActive(true);
            
            if (indexActual == 0) {
                buttonPrev.SetActive(false);
            }
        }

        if (indexActual < images.Length - 1) {
            buttonNext.SetActive(true);
        }
    }

    public void SkipIntro() {
        SceneManager.LoadSceneAsync(2);
    }
}
