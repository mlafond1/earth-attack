using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{

    public abstract void Execute();

    protected void CancelAction(){
        ActionPlayer.SetNextAction(null);
    }

}
