using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    
    CanvasGroup group;
    Image image;
    Text text;

    void Start(){
        group = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        Hide();
    }

    public void Win(){
        Debug.Log("Partie gagnée");
        ClearScene();
        Display();
        image.color = new Color(0.4f, 1, 0.4f, 0.4f);
        text.text = "Vous avez gagné!!";
        GameObject.FindObjectOfType<StatsPanel>().Hide();
        GameObject.FindObjectOfType<ActionPanel>().Hide();
        // TODO celebration?
    }

    public void Lose(){
        Debug.Log("Partie perdue");
        ClearScene();
        Display();
        image.color = new Color(1, 0.4f, 0.4f, 0.4f);
        text.text = "Vous avez perdu...";
        GameObject.FindObjectOfType<StatsPanel>().Hide();
        GameObject.FindObjectOfType<ActionPanel>().Hide();
    }

    private void ClearScene(){
        RessourceManager.GetInstance().StopAllCoroutines();
        GameObject.FindObjectOfType<SpawnEnemy>().StopSpawning();
        EnemyHealth[] enemies = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (var e in enemies){
            Destroy(e.gameObject);
        }
    }

    private void Display(){
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
