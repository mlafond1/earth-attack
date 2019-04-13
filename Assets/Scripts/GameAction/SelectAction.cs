using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : GameAction
{

    public string towerName;

    public SelectAction(string towerName){
        this.towerName = towerName;
    }

    public SelectAction(){} // Needed for the reflection in ButtonAction

    override public void Execute(){
        ActionPlayer.SetNextAction(new PlaceAction(towerName));
    }

}
