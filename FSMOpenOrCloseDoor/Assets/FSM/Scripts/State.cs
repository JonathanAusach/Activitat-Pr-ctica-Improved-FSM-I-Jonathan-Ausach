using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State.state", menuName = "FSM/State")]
public class State : ScriptableObject {
    public Action[] enterActions;
    public Action[] actions;
    public Action[] exitActions;
    public Transition[] transitions;
}