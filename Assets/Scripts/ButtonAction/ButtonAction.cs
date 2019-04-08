using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{

    GameAction gameAction;
    public Transform associated = null;
    public string actionName;

    void Start(){
        LoadGameAction();
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
    }

    private void LoadGameAction(){
        Debug.Log(actionName + " " + associated);
        if(associated != null){
            Object[] args = {associated};
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


