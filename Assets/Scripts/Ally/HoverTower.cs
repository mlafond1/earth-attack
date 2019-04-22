using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTower : MonoBehaviour
{
    
    private Tower tower;
    private static CanvasGroup radiusZone;
    public static bool forUpgrade {get; set;} = false;

    void Start(){
        tower = GetComponent<Tower>();
        if(radiusZone == null)
            radiusZone = GameObject.Find("RadiusZone").GetComponent<CanvasGroup>();
    }

    void OnMouseEnter(){
        if(!forUpgrade){
            GameObject.FindObjectOfType<StatsPanel>().Display(tower);
        }
        else{
            TowerFactory factory = TowerFactory.GetInstance();
            Tower upgrade = factory.Build(tower.towerName,tower.upgradeIndex)?.GetComponent<Tower>();
            GameObject.FindObjectOfType<StatsPanel>().Display(tower, upgrade);
        }
        radiusZone.transform.position = transform.position;
        radiusZone.transform.position += new Vector3(0f,0.6f,0f);
        float r = 2*tower.radius/15;
        radiusZone.GetComponentInChildren<UnityEngine.UI.Image>().transform.localScale = new Vector3(r,r,r);
        radiusZone.alpha = 1f;
    }
    void OnMouseExit(){
        GameObject.FindObjectOfType<StatsPanel>().Hide();
        radiusZone.GetComponentInChildren<UnityEngine.UI.Image>().transform.localScale = new Vector3(1f,1f,1f);
        radiusZone.alpha = 0f;
    }
}

