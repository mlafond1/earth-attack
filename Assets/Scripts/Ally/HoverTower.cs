using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTower : MonoBehaviour
{
    
    AttackEnemy ae;
    public static bool forUpgrade {get; set;} = false;

    void Start(){
        ae = GetComponent<AttackEnemy>();
    }

    void OnMouseEnter(){
        if(!forUpgrade){
            GameObject.FindObjectOfType<StatsPanel>().Display(ae);
        }
        else{
            TowerFactory factory = TowerFactory.GetInstance();
            AttackEnemy upgrade = factory.Build(ae.towerName,ae.upgradeIndex)?.GetComponent<AttackEnemy>();
            GameObject.FindObjectOfType<StatsPanel>().Display(ae, upgrade);
        }
    }
    void OnMouseExit(){
        GameObject.FindObjectOfType<StatsPanel>().Hide();
    }
}

