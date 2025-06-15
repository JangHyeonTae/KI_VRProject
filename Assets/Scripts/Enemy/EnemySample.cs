using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySample : MonoBehaviour, IDamageable
{

    public bool CanMove { get; set; } = true;

    public StateMachine stateMachine;

    private int maxHp = 100;

    [SerializeField] private Transform target;
    [SerializeField] private float targetDistance;

    public bool canFollow = false;
    [field: SerializeField] public int HP { get; set; }

    public float moveSpeed;
    public Animator animator;
    public bool isAttack;

    Coroutine cor;
    Coroutine followCor;
    Coroutine attackCor;
    Rigidbody rigid;

    public UnityEvent<EnemyBody> OnHitPoint;

    public int rand;
    public float randDelay = 1f;
    private float randTime = 0f;

    #region Animattion_Hash
    public readonly int IDLE_HASH = Animator.StringToHash("Idle");
    public readonly int WALKING_HASH = Animator.StringToHash("Walking");
    public readonly int LEFTHOOK_HASH = Animator.StringToHash("LeftHook");
    public readonly int RIGHTHOOK_HASH = Animator.StringToHash("RightHook");
    public readonly int LEFTBLOCK_HASH = Animator.StringToHash("LeftBlock");
    public readonly int RIGHTBLOCK_HASH = Animator.StringToHash("RightBlock");
    public readonly int JAB_HASH = Animator.StringToHash("Jab");
    public readonly int LEFTUPPER_HASH = Animator.StringToHash("LeftUpper");
    public readonly int STEPFORWARD_HASH = Animator.StringToHash("StepForward");
    public readonly int STEPBACK_HASH = Animator.StringToHash("StepBack");
    public readonly int TAKEDAMAGE_HASH = Animator.StringToHash("TakeDamage");
    #endregion

    private void OnEnable()
    {
        OnHitPoint.AddListener(GetDamageBox);
    }

    private void OnDisable()
    {
        OnHitPoint.RemoveListener(GetDamageBox);
    }

    private void Start()
    {
        HP = maxHp;
        rigid = GetComponent<Rigidbody>();
        StateMachineInit();
    }

    private void StateMachineInit()
    {
        stateMachine = new StateMachine();

        stateMachine.stateDic.Add(EState.Idle, new Enemy_Idle(this));
        stateMachine.stateDic.Add(EState.Jab, new Enemy_Jab(this));
        stateMachine.stateDic.Add(EState.Walking, new Enemy_Walking(this));
        stateMachine.stateDic.Add(EState.StepBack, new Enemy_StepBack(this));
        stateMachine.stateDic.Add(EState.LeftHook, new Enemy_LeftHook(this));
        stateMachine.stateDic.Add(EState.StepForward, new Enemy_StepForward(this));
        stateMachine.stateDic.Add(EState.RightHook, new Enemy_RightHook(this));
        stateMachine.stateDic.Add(EState.RightBlock, new Enemy_RightBlock(this));
        stateMachine.stateDic.Add(EState.LeftBlock, new Enemy_LeftBlock(this));
        stateMachine.stateDic.Add(EState.TakeDamage, new Enemy_TakeDamage(this));
        stateMachine.stateDic.Add(EState.LeftUpper, new Enemy_LeftUpper(this));

        stateMachine.curState = stateMachine.stateDic[EState.Idle];
    }

    private void Update()
    {
        if(!isAttack)
        {
            rand = Random.Range(0, 9);
        }

        LookRotation();
        FollowTarget();
        stateMachine.Update();
    }

    #region AttackCoroutine
    public void AttackAnim()
    {
        if (attackCor == null)
        {
            attackCor = StartCoroutine(CorAttack());
        }
    }

    private IEnumerator CorAttack()
    {
        yield return new WaitForSeconds(0.7f);
        attackCor = null;
        isAttack = false;
    }
    #endregion

    #region FollowTarget
    private void FollowTarget()
    {
        if (!CanMove) return;
        if (target == null) return;

        Transform pos = gameObject.transform;
        Vector3 d = target.position - this.transform.position;
        if (d.sqrMagnitude > targetDistance)
        {
            if (followCor == null)
            {
                followCor = StartCoroutine(FollowDelay());
            }
        }
        else if(d.sqrMagnitude > targetDistance && followCor != null)
        {
            StopCoroutine(followCor);
            canFollow = false;
            followCor = null;
        }

        if (canFollow)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    IEnumerator FollowDelay()
    {
        yield return new WaitForSeconds(2f);
        canFollow = true;
        yield return null;
    }

    #endregion 

    #region DamageSystem
    public void GetDamageBox(EnemyBody _enemyBody)
    {
        if (_enemyBody == null) return;
        TakeDamage(_enemyBody.damageBox.GetDamageValue());
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        HP = Mathf.Max(0, HP - amount);
        if (cor == null)
        {
            cor = StartCoroutine(TakeHit(amount));
        }
    }

    IEnumerator TakeHit(int amount)
    {
        Debug.Log($"test : {amount}");
        if (amount > 5)
        {
            //애니메이션 실행
            //statePattern에 넘길거임

            Debug.Log("Red");
        }
        else if (amount > 1 && amount <= 5)
        {

            Debug.Log("Yellow");
        }
        else
        {

            Debug.Log("Black");
        }

        yield return new WaitForSeconds(1f);
        //애니메이션 종료
        StopCoroutine(cor);
        cor = null;
    }
    #endregion

    public void Attack(int amount)
    {

    }

    public void Defence()
    {

    }



    private void LookRotation()
    {
        if (target == null) return;
        transform.LookAt(target);
    }



}
