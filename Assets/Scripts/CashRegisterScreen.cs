using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CashRegisterScreen : MonoBehaviour {

    public GameObject button;
    public GameObject text;
    private List<string> coctels;
    private int points = 0;

    private void Start()
    {
        button = GameObject.FindWithTag("Button");
        text = GameObject.FindWithTag("Text");
        coctels = new List<string>{"Mojito", "Caipirinha", "Pisco Sour"};
    }

    void StartGame()
    {
        SelectCoctel();
        button.GetComponentInChildren<Text>().text = "Finish Coctel";
    }

    void EndGame()
    {
        AddScore();
        button.GetComponentInChildren<Text>().text = "Start Coctel";
    }

    void SelectCoctel()
    {
        int rnd = new System.Random().Next(coctels.Count());
        text.GetComponentInChildren<Text>().text = coctels[rnd];
    }

    void AddScore()
    {
        points += 10; // temporal
        text.GetComponentInChildren<Text>().text = points + " $";
    }

}
