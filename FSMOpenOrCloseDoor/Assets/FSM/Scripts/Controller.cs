using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esta clase se encargará de gestionar Estado actual, 
/// Las acciones a realizar del estado actual, y a evaluar 
/// las decisiones que provocarán transiciones entre estados
/// </summary>
public class Controller : MonoBehaviour {
    [SerializeField] State firstState;
    State currentState;

    private void Start() {
        ChangeState(firstState);
    }

    private void Update() {
        UpdateStateActions();
        EvaluateStateTransitions();
    }

    void UpdateStateActions() {
        foreach (Action action in currentState.actions) {
            action.Act(this);
        }
    }

    void EvaluateStateTransitions() {
        State newState = null;

        for (int i = 0; i < currentState.transitions.Length
            && newState == null; i++) {
            if (currentState.transitions[i].decision.Decide(this)) {
                newState = currentState.transitions[i].trueState;
            } else {
                newState = currentState.transitions[i].falseState;
            }
        }

        if (newState != null) {
            ChangeState(newState);
        }
    }

    void ChangeState(State newState) {
        if (currentState != null) ExitState(currentState);
        EnterState(newState);
        currentState = newState;
    }

    void ExitState(State stateToExit) {
        foreach (Action action in stateToExit.exitActions) {
            action.Act(this);
        }
    }

    void EnterState(State stateToEnter) {
        foreach (Action action in stateToEnter.enterActions) {
            action.Act(this);
        }
    }

}
