using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameAction gameAction {get; private set;}
    public string actionName;
    public string towerName;
    public bool isHovered;

    void Start(){
        LoadGameAction();
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    private void LoadGameAction(){
        //Debug.Log(actionName + " " + towerName);
        if(towerName != ""){
            System.Object[] args = {towerName};
            gameAction = (GameAction)System.Activator.CreateInstance(System.Type.GetType(actionName), args);
        }
        else {
            gameAction = (GameAction)System.Activator.CreateInstance(System.Type.GetType(actionName));
        }
    }

    void TaskOnClick(){
        ActionPlayer.SetNextAction(gameAction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(towerName == "") return;
        TowerFactory factory = TowerFactory.GetInstance();
        Tower tower = factory.Build(towerName).GetComponent<Tower>();
        GameObject.FindObjectOfType<StatsPanel>().Display(tower);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(towerName == "") return;
        GameObject.FindObjectOfType<StatsPanel>().Hide();
    }
}


