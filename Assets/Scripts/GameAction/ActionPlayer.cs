using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{

    private static GameAction nextAction;

    public static void SetNextAction(GameAction gameAction){
        nextAction = gameAction;
    }

    public static void ProcessNextAction(){
        if(nextAction == null) return;
        nextAction.Execute();
    }

    void FixedUpdate(){
        ActionPlayer.ProcessNextAction();
    }

}
