using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreNormalHoverAction : GameAction
{
    override public void Execute(){
        HoverTower.forUpgrade = false;
        CancelAction();
    }

}
