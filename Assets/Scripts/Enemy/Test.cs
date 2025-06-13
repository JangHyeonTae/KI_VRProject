using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class Test : XRSimpleInteractable//, IDamageable
{
    public EnemyType type;
    public int maxHp = 0;

    [SerializeField] private Transform target;
    [field: SerializeField] public int HP { get; set; }
    [SerializeField] private GameObject avatar;
    Animator animator;

    Coroutine cor;

    public UnityEvent<EnemyBody> OnHitPoint;

    private void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        LookRotation();
    }

    private void OnEnable()
    {
        base.OnEnable();
        OnHitPoint.AddListener(GetDamageBox);
    }

    private void OnDisable()
    {
        base.OnDisable();
        OnHitPoint.RemoveListener(GetDamageBox);
    }

    private void Start()
    {
        HP = maxHp;
        animator = GetComponent<Animator>();
    }

    public void GetDamageBox(EnemyBody _enemyBody)
    {
        if (_enemyBody == null) return;
        Debug.Log($"GetDamageBox : {_enemyBody}");
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


    public void Attack(int amount)
    {

    }

    public void Defence()
    {

    }



    private void LookRotation()
    {
        transform.LookAt(SetTarget());
    }

    private Transform SetTarget()
    {
        return target;
    }
}


public enum EnemyType
{
    None,
    Test,
    Fruit
}
