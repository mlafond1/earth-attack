using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreUpgradeAction : GameAction
{
    override public void Execute(){
        HoverTower.forUpgrade = true;
        ActionPlayer.SetNextAction(new UpgradeAction());
        ActionPlayer.QueueAction(new RestoreNormalHoverAction());
    }

}
