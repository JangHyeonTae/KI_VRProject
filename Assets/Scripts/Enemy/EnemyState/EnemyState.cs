using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    protected EnemySample enemy;

    public EnemyState(EnemySample _enemy)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

}

public class Enemy_Idle : EnemyState
{
    public Enemy_Idle(EnemySample _enemy) : base(_enemy)
    {

    }

    public override void Enter()
    {
        enemy.animator.Play(enemy.IDLE_HASH);
        enemy.canFollow = false;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.canFollow)
        {
            enemy.stateMachine.ChangeState(enemy.stateMachine.stateDic[EState.Walking]);
        }
    }

    public override void Exit() { }
}

public class Enemy_Walking : EnemyState
{
    public Enemy_Walking(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_LeftHook : EnemyState
{
    public Enemy_LeftHook(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {
        enemy.rand = 0;
        enemy.animator.Play(enemy.LEFTHOOK_HASH);
        enemy.isAttack = true;
        enemy.AttackAnim();
    }

    public override void Update()
    {
        if(!enemy.isAttack)
        {
            enemy.animator.Play(enemy.IDLE_HASH);
        }
    }

    public override void Exit() { }
}

public class Enemy_RightHook : EnemyState
{
    public Enemy_RightHook(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_LeftBlock : EnemyState
{
    public Enemy_LeftBlock(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_RightBlock : EnemyState
{
    public Enemy_RightBlock(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_StepBack : EnemyState
{
    public Enemy_StepBack(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_StepForward : EnemyState
{
    public Enemy_StepForward(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_Jab : EnemyState
{
    public Enemy_Jab(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_TakeDamage : EnemyState
{
    public Enemy_TakeDamage(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}

public class Enemy_LeftUpper : EnemyState
{
    public Enemy_LeftUpper(EnemySample _enemy) : base(_enemy)
    {

    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}