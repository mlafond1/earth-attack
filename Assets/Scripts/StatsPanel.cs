using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    
    CanvasGroup group;
    Text text;

    void Start(){
        group = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        Hide();
    }

    public void Display(AttackEnemy tower){
        text.text = "Name : " + tower.towerName +
                    "\n\nCost : " + tower.cost +
                    "\nPower : " + tower.power +
                    "\nAttackRate : " + tower.attackSpeed +
                    "\nRadius : " + tower.radius +
                    "\n\nDescription : \n" + tower.description;
        group.alpha = 1f;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    public void Display(AttackEnemy tower, AttackEnemy upgrade){
        if(upgrade == null){
            Display(tower);
            text.text += "\n\n" + "No more Upgrades";
        }
        text.text = "Name : " + tower.towerName +
                    "\n\nCost : " + upgrade.cost +
                    "\nPower : " + tower.power + "->" + upgrade.power +
                    "\nAttackRate : " + tower.attackSpeed + "->" + upgrade.attackSpeed +
                    "\nRadius : " + tower.radius + "->" + upgrade.radius +
                    "\n\nDescription : \n" + tower.description;
        group.alpha = 1f;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    public void Hide(){
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

}
