﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{

    private UnityEngine.UI.Text ressourceText;
    public float startingRessources = 50;
    public float ressources;
    private float timer;
    private bool stopSignal = false;
    private  static RessourceManager instance;

    void Start()
    {
        if(startingRessources == 0) startingRessources = 50;
        ressourceText = gameObject.GetComponent<UnityEngine.UI.Text>();
        ressources = startingRessources;
        DisplayRessources();
        //StartCoroutine(PassiveIncome());
        instance = this;
    }

    private IEnumerator PassiveIncome(){
        while(!stopSignal){
            Add(1);
            yield return new WaitForSecondsRealtime(5);
        }
    }
    
    public void Add(float amount){
        if(amount >= 0){
            ressources += amount;
            DisplayRessources();
        }
    }

    public bool Spend(float amount){
        bool canSpend = CanSpend(amount);
        if(canSpend) {
            ressources -= amount;
            DisplayRessources();
        }
        return canSpend;
    }

    public bool CanSpend(float amount){
        return amount <= ressources && amount >= 0;
    }

    private void DisplayRessources(){
        ressourceText.text = "Ressources : " + ressources;
    }

    public void SendStopSignal(){
        stopSignal = true;
    }

    public static RessourceManager GetInstance(){
        return instance;
    }

}
