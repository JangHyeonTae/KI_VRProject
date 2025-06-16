using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : XRDirectInteractor
{
    private bool isAttack = false;
    private bool canDefence = true;

    List<Collider> colList = new();
    private GameObject enemyObject;
    private Coroutine attackCor;
    private Coroutine defenceCor;

    public bool isDefend = false;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (isAttack) return;
        base.OnHoverEntered(args);

        if (args.interactableObject.transform.gameObject == null)
        {
            Debug.Log($"Hover : null");
            return;
        }
        enemyObject = args.interactableObject.transform.gameObject;

    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        Debug.Log("dddd");
    }


    public void Defend()
    {
        if (!canDefence) return;

        if(defenceCor == null)
        {
            defenceCor = StartCoroutine(DefenceCor());
        }

    }

    IEnumerator DefenceCor()
    {
        isDefend = true;
        canDefence = false;
        yield return new WaitForSeconds(1.5f);
        isDefend = false;
        canDefence = true;
    }


    public void Attack()
    {
        if(attackCor == null)
        {
            attackCor = StartCoroutine(AttackCoroutine());
        }
    }


    IEnumerator AttackCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (enemyObject == null)
        {
            yield break;
        }

        Debug.Log($"cor : {enemyObject.name}");
        Manager.FightInstance.GetHitPoint(enemyObject.transform.GetComponentInChildren<EnemyBody>());
        
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    { 
        isAttack = true;
        canDefence = false;
        yield return new WaitForSeconds(1f);
        enemyObject = null;
        isAttack = false;
        yield return null;
        canDefence = true;
        if(attackCor != null)
        {
            StopCoroutine(attackCor);
            attackCor = null;
        }
    }

}
