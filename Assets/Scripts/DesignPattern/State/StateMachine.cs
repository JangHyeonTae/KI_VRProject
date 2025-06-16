using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class StateMachine
{
    public Dictionary<EState, BaseState> stateDic;
    public Dictionary<int, BaseState> randStateDic;

    public BaseState curState;
    public int curStateNum;
    public StateMachine()
    {
        stateDic = new Dictionary<EState, BaseState>();
        randStateDic = new Dictionary<int, BaseState>();
    }

    public void ChangeState(BaseState changedState)
    {
        if (curState == changedState) return;
        curState.Exit();
        curState = changedState; 
        curState.Enter(); 
    }

    public BaseState RandChange(int num)
    {
        if (curStateNum == num || num == -1) return null;

        curStateNum = num;
        return randStateDic[curStateNum];
    }

    public void Update() => curState.Update();

}
