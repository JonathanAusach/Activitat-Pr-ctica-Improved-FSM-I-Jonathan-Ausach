using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionDebug.asset", menuName = "FSM/ActionDebug")]
public class ActionDebug : Action {
    public string messageToDebug;
    public override void Act(Controller controller) {
        Debug.Log(messageToDebug);
    }
}
