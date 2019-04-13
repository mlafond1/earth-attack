using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{

    GameAction gameAction;
    public string actionName;
    public string towerName;

    void Start(){
        LoadGameAction();
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    private void LoadGameAction(){
        //Debug.Log(actionName + " " + towerName);
        if(towerName != ""){
            //Debug.Log(towerName + " selected");
            TowerFactory factory = TowerFactory.GetInstance();
            Object[] args = {factory.Build(towerName).transform};
            gameAction = (GameAction)System.Activator.CreateInstance(System.Type.GetType(actionName), args);
        }
        else {
            gameAction = (GameAction)System.Activator.CreateInstance(System.Type.GetType(actionName));
        }
    }

    void TaskOnClick(){
        ActionPlayer.SetNextAction(gameAction);
    }
}


