using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightManager : MonoBehaviour
{
    private static FightManager fightInstance;
    public static FightManager FightInstance { get { return fightInstance; } }

    public EnemyBody[] enemyBody = new EnemyBody[6];
    Dictionary<string, EnemyBody> enemyBodyDict;


    private EnemyBody changeBody;
    public EnemyBody ChangeBody { get { return changeBody; } set {  changeBody = value; OnHit?.Invoke(changeBody); } }

    public UnityEvent<EnemyBody> OnHit;

    public EnemySample enemy;
    public Player player;


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


    }

    private void Start()
    {
        enemyBodyDict = new Dictionary<string, EnemyBody>();
        for (int i = 0; i < enemyBody.Length; i++)
        {
            if (enemyBody[i] == null || enemyBody[i].damageBox == null)
            {
                
                Debug.Log($"{enemyBody[i].tag}notset");
                continue;
            }

            enemyBodyDict.Add(enemyBody[i].tag, enemyBody[i]);
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
        Debug.Log($"GetHitPoint1 : {_enemybody.tag}");
        if (enemyBodyDict.ContainsKey(_enemybody.tag))
        {
            Debug.Log($"GetHitPoint : {_enemybody.tag}");
            HitBody(_enemybody);
            return enemyBodyDict[_enemybody.tag];
        }
        else
        {
            Debug.Log("Null");
            return null;
        }
    }

    public void HitBody(EnemyBody _enemyBody)
    {
        if (_enemyBody == null) return;
        Debug.Log($"HitBody : {_enemyBody.name}");
        enemy.OnHitPoint?.Invoke(_enemyBody);
    }
}
