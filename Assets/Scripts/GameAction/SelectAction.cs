using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : GameAction
{

    public Transform selectedObject;

    public SelectAction(Transform selectedObject){
        this.selectedObject = selectedObject;
    }

    public SelectAction(){}

    override public void Execute(){
        ActionPlayer.SetNextAction(new PlaceAction(selectedObject.gameObject));
    }

}
