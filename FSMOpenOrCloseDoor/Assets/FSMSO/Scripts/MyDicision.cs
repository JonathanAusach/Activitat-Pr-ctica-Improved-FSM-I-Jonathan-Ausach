using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision.asset", menuName = "FSM/Decision")] 
public class MyDicision : Decision
{
    public bool decision; 
    public override bool Decide(Controller controller)
    {
        return decision;
    }
}
