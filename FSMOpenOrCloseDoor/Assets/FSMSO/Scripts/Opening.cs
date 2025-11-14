using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Opening.asset", menuName = "FSM/OpenClosingDoor")]
public class Opening : Action
{
    //velocidad de abrirse
    public float speed;

    public bool Closing;

    public override void Act(Controller controller)
    {
        if (Closing)
        {
            DoorState2.MyDoor.CloseingDoor(speed,Closing);
        }
        else
        {
            DoorState2.MyDoor.OpeningDoor(speed,Closing);
        }
    }
}