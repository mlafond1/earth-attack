using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAction : GameAction
{
    
    public override void Execute()
    {
        Application.Quit();
        CancelAction();
    }

}
