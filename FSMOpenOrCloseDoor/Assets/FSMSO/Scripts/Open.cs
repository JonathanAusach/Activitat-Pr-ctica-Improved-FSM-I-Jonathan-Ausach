using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Open.asset", menuName = "FSM/OpenCloseDoor")]
public class Open : Action {

    public bool Close;
    public override void Act(Controller controller)
    {
        if (Close)
        {
            DoorState2.MyDoor.CloseDoor();
        }
        else
        {
            DoorState2.MyDoor.OpenDoor();
        }
    }
}
