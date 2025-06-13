using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightManager : MonoBehaviour
{
    private static FightManager fightInstance;
    public static FightManager FightInstance { get { return fightInstance; } }

    public EnemyBody[] enemyBody;
    Dictionary<int, EnemyBody> enemyBodyDict;


    private EnemyBody changeBody;
    public EnemyBody ChangeBody { get { return changeBody; } set {  changeBody = value; OnHit?.Invoke(changeBody); } }

    public UnityEvent<EnemyBody> OnHit;

    public PlayerController player;
    public Test enemy;


    private void Awake()
    {
        if(fightInstance == null)
        {
            fightInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        enemyBody = new EnemyBody[] { };
        enemyBodyDict = new Dictionary<int, EnemyBody>();
        for (int i = 0; i < enemyBody.Length; i++)
        {
            enemyBodyDict.Add(enemyBody[i].damageBox.ID, enemyBody[i]);
        }
    }

    private void OnEnable()
    {
        OnHit.AddListener(HitBody);
    }

    private void OnDisable()
    {
        OnHit.RemoveListener(HitBody);
    }

    public EnemyBody GetHitPoint(EnemyBody _enemybody)
    {
        if(enemyBodyDict.ContainsKey(_enemybody.damageBox.ID))
        {
            Debug.Log($"GetHitPoint : {_enemybody}");
            HitBody(_enemybody);
            return enemyBodyDict[_enemybody.damageBox.ID];
        }
        else
        {
            return null;
        }
    }

    public void HitBody(EnemyBody _enemyBody)
    {
        if (_enemyBody == null) return;
        Debug.Log($"HitBody : {_enemyBody}");
        ChangeBody = GetHitPoint(_enemyBody);
        enemy.GetComponent<Test>().OnHitPoint?.Invoke(ChangeBody);
    }
}
