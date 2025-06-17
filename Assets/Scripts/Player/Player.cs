using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public Camera cam;
    [SerializeField] private int maxHp;
    private int hp;
    public int Hp { get { return hp; } set { hp = value; OnChangeHp?.Invoke(hp); } }
    public event Action<int> OnChangeHp;

    public GameObject[] particlePrefab;
    public Transform[] particlePos;

    public GameObject hpPanel;
    private int rand;

    Coroutine panelCor;
    private void Start()
    {
        Hp = maxHp;
        cam = Camera.main;
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
        if (amount <= 0) return;

        Hp = Mathf.Max(0, Hp - amount);
        GameObject obj1 = Instantiate(particlePrefab[0], particlePos[0]);
        GameObject obj2 = Instantiate(particlePrefab[1], particlePos[1]);
        Destroy(obj1, 1f);
        Destroy(obj2, 1f);


        if (Hp <= 0)
        {
            Manager.Instance.ShowEnd();
        }
    }


    
    private void HpGuage(int hp)
    { 
        Debug.Log($"{Hp}");

        if(hp < 100)
        {
            if(panelCor == null)
            {
                //panelCor = StartCoroutine(TakeDamagePanel());
            }
        }
    }

   //WaitForSeconds panelShow = new WaitForSeconds(1f);
   //IEnumerator TakeDamagePanel()
   //{
   //    hpPanel.SetActive(true);
   //    yield return panelShow;
   //    hpPanel.SetActive(false);
   //    if(panelCor != null)
   //    {
   //        StopCoroutine(panelCor);
   //        panelCor = null;
   //    }
   //}
}
