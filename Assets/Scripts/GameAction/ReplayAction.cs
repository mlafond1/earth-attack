using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAction : GameAction
{
    public override void Execute()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mars_2.0");
        CancelAction();
    }
}
