using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum EState
{
    Jab = 0,
    LeftHook,
    RightHook,
    LeftUpper,
    LeftBlock,
    RightBlock,
    StepBack,
    StepForward,
    Walking,
    Idle,
    TakeDamage,
}

public abstract class BaseState
{
    public abstract void Enter();

    public abstract void Update();


    public abstract void Exit();
}
