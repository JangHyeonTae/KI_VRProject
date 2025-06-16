using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;
    private int hp;
    public int Hp { get { return hp; } set { hp = value; OnChangeHp?.Invoke(hp); } }
    public event Action<int> OnChangeHp;

    public GameObject particlePrefab;
    public bool isDefend = false;

    private void Start()
    {
        hp = maxHp;
    }

    private void OnEnable()
    {
        OnChangeHp += HpGuage;
    }

    private void OnDisable()
    {
        OnChangeHp -= HpGuage;
    }

    public void TakeDamage(int amount)
    {
        Hp = Mathf.Max(0, Hp - amount);
        Debug.Log($"{Hp}");
        //Instantiate(particlePrefab);
        if (Hp <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    public void Defend(bool _isDefend)
    {
        GameObject enemyObject = GetComponentInChildren<PlayerController>().gameObject;
        if (enemyObject == null)
        {
            isDefend = _isDefend;
        }
        else
        {
            isDefend = _isDefend;
        }
    }
    private void HpGuage(int amount)
    {
        Debug.Log($"{Hp}");

        if (amount < 100 && amount >= 50)
        {
            Debug.Log("100");
        }
        else if(amount > 50 && amount <= 20)
        {
            Debug.Log("50");
        }
        else
        {
            Debug.Log("10");
        }
    }
}
