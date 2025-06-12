using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour, IDamageable
{
    public EnemyType type;
    public int maxHp = 0;

    [SerializeField] private Transform target;
    [field: SerializeField] public int HP { get; set; }
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject[] damageCollider;
    Animator animator;

    Coroutine cor;

    public UnityEvent<EnemyBody> OnHitPoint;

    private void Update()
    {
        LookRotation();
    }

    private void LookRotation()
    {
        transform.LookAt(SetTarget());
    }

    private Transform SetTarget()
    {
        return target;
    }

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
        animator = GetComponent<Animator>();
    }

    public void GetDamageBox(EnemyBody _enemyBody)
    {
        Debug.Log($"GetDamageBox 1 :{_enemyBody.damageBox.GetDamageValue()}");
        if (_enemyBody == null) return;
        Debug.Log($"GetDamageBox 2 :{_enemyBody.damageBox.GetDamageValue()}");
        TakeDamage(_enemyBody.damageBox.GetDamageValue());
    }



    public void TakeDamage(int amount)
    {
        Debug.Log($"TakeDamage 1 :{amount}");
        if (amount <= 0) return;
        HP = Mathf.Max(0, HP - amount);
        Debug.Log($"TakeDamage 2 :{amount}");
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
            //�ִϸ��̼� ����
            //statePattern�� �ѱ����
            avatar.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (amount > 1 && amount <= 5)
        {
            avatar.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            avatar.GetComponent<MeshRenderer>().material.color = Color.black;
        }

        yield return new WaitForSeconds(1f);
        //�ִϸ��̼� ����
        StopCoroutine(cor);
        cor = null;
        avatar.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void Attack(int amount)
    {

    }

    public void Defence()
    {

    }
}


public enum EnemyType
{
    None,
    Test,
    Fruit
}
