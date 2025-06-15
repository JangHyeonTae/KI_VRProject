using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class StateMachine
{
    public Dictionary<EState, BaseState> stateDic;

    public BaseState curState;

    public StateMachine()
    {
        stateDic = new Dictionary<EState, BaseState>();
    }

    public void ChangeState(BaseState changedState)
    {
        if (curState == changedState) return;
        curState.Exit();
        curState = changedState; 
        curState.Enter(); 
    }

    public void Update() => curState.Update();

}
