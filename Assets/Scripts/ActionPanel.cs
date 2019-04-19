using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    
    CanvasGroup group;

    void Start(){
        group = GetComponent<CanvasGroup>();
        Display();
    }

    public void Display(){
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void Hide(){
        group.alpha = 0f;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

}
