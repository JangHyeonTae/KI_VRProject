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

    public bool canFollow = true;
    [field: SerializeField] public int HP { get; set; }

    public float moveSpeed;
    public Animator animator;
    public bool isAttack;
    public bool canAttack = false;
    public bool takeDamage = false;

    Coroutine cor;
    Coroutine followCor;
    Coroutine attackCor;
    Coroutine canAttackCor;
    Coroutine turnCor;
    Rigidbody rigid;

    public UnityEvent<EnemyBody> OnHitPoint;

    public int rand;
    public float randDelay = 1f;
    private float randTime = 0f;

    public float attackRange;
    public LayerMask targetLayer;
    public GameObject[] particlePrefab;
    public Transform[] particlePos;

    public float runTime;
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
        stateMachine.stateDic.Add(EState.LeftHook, new Enemy_LeftHook(this));
        stateMachine.stateDic.Add(EState.RightHook, new Enemy_RightHook(this));
        stateMachine.stateDic.Add(EState.TakeDamage, new Enemy_TakeDamage(this));
        stateMachine.stateDic.Add(EState.LeftUpper, new Enemy_LeftUpper(this));


        stateMachine.randStateDic.Add(0, new Enemy_LeftHook(this));
        stateMachine.randStateDic.Add(1, new Enemy_Jab(this));
        stateMachine.randStateDic.Add(2, new Enemy_LeftUpper(this));
        stateMachine.randStateDic.Add(3, new Enemy_RightHook(this));


        stateMachine.curState = stateMachine.stateDic[EState.Idle];
    }

    private void Update()
    {
        if(!isAttack)
        {
            rand = Random.Range(0, 4);
        }

        LookRotation();
        FollowTarget();
        stateMachine.Update();

        if(!canAttack)
        {
            CanAttackAnim();
        }
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
        yield return new WaitForSeconds(runTime);
        isAttack = false;
        yield return new WaitForEndOfFrame();
        attackCor = null;
    }

    public void CanAttackAnim()
    {
        if(canAttackCor == null)
        {
            canAttackCor = StartCoroutine(CanAttack());
        }
    }

    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1f);
        canAttack = false;
        canAttackCor = null;
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
        else if(d.sqrMagnitude < targetDistance && followCor != null)
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
        yield return new WaitForSeconds(0.5f);
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
        GameObject obj1 = Instantiate(particlePrefab[0], particlePos[0]);
        GameObject obj2 = Instantiate(particlePrefab[1], particlePos[1]);
        Destroy(obj1, 1.5f);
        Destroy(obj2, 1.5f);

        if (amount > 5)
        {
            takeDamage = true;
            //애니메이션 실행
            //statePattern에 넘길거임

            Debug.Log("Red");
        }
        else if (amount > 1 && amount <= 5)
        {
            takeDamage = true;
            Debug.Log("Yellow");
        }
        else
        {

            takeDamage = false;
            Debug.Log("Black");
        }

        yield return new WaitForSeconds(1f);
        takeDamage = false;
        cor = null;
    }
    #endregion

    public void EnemyAttack(int amount)
    {
        if (Manager.FightInstance.player[0].isDefend &&
            Manager.FightInstance.player[1].isDefend) return;

        
        Player targetPlayer = target.GetComponent<Player>();
        
        Vector3 targetPos = targetPlayer.transform.position;
        targetPos.y = 0;
        Vector3 attackPos = transform.position;
        attackPos.y = 0;

        Vector3 targetDir = (targetPos - attackPos).normalized;

        if (Vector3.Angle(transform.forward, targetDir) > attackRange * 0.5f)
            return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(amount);
        }
        
    }

    private void LookRotation()
    {
        if (target == null) return;
        if(!isAttack)
        {
            transform.LookAt(target);
        }
    }




}
