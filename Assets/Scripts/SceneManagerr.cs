using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public int clicks = 0;
    public void Newton() {
        SceneManager.LoadScene("nilto");
    }

    public void Aristoteles() {
        SceneManager.LoadScene("Aristoteles");
    }
    public void Galileu() {
        SceneManager.LoadScene("Galileu");
    }
    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
    public void Egg() {
        clicks++;
        if(clicks >= 2)
        {
            SceneManager.LoadScene("Egg");
        }
    }
}
