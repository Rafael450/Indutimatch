using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termometro : MonoBehaviour
{
    public Sprite[] Term;
    private int pont;
    GameObject Ui;
    
    void Start() 
    {
        Ui = GameObject.Find("UIManager");
    }
    // Update is called once per frame
    void Update()
    {
        if(pont != Ui.GetComponent<UIManager>().pontos)
        {
            pont = Ui.GetComponent<UIManager>().pontos;
            GetComponent<SpriteRenderer>().sprite = Term[pont+3];
        }
    }
}
