using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{

    private static GameAction nextAction;
    private static Queue<GameAction> actionQueue = new Queue<GameAction>();

    public static void QueueAction(GameAction gameAction){
        actionQueue.Enqueue(gameAction);
    }
    public static void SetNextAction(GameAction gameAction){
        if(gameAction == null){
            nextAction = null; 
            return;
        }
        QueueAction(gameAction);
        nextAction = actionQueue.Dequeue();
    }

    public static void ProcessNextAction(){
        if(nextAction == null){
            if(actionQueue.Count == 0) return;
            else nextAction = actionQueue.Dequeue();
        }
        nextAction.Execute();
    }

    void FixedUpdate(){
        ActionPlayer.ProcessNextAction();
    }

}
