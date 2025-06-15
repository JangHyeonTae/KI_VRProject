using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum EState
{
    Idle,
    Walking,
    TakeDamage,
    Jab,
    LeftHook,
    RightHook,
    LeftUpper,
    LeftBlock,
    RightBlock,
    StepBack,
    StepForward
}

public abstract class BaseState
{
    public abstract void Enter();

    public abstract void Update();


    public abstract void Exit();
}
