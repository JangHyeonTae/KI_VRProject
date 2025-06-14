using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : XRDirectInteractor
{
    private bool isAttack = false;

    List<Collider> colList = new();
    private GameObject enemyObject;
    private Coroutine attackCor;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (args.interactableObject.transform.gameObject == null)
        {
            Debug.Log($"Hover : null");
        }
        enemyObject = args.interactableObject.transform.gameObject;

    }

    public void Attack()
    {
        if(attackCor == null)
        {
            attackCor = StartCoroutine(AttackCoroutine());
        }
        else
        {
            StopCoroutine(attackCor);
        }
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (enemyObject == null)
        {
            Debug.Log($"cor : null");
            yield break;
        }

        Debug.Log($"cor2 : {enemyObject.name}");
        Manager.FightInstance.GetHitPoint(enemyObject.transform.GetComponentInChildren<EnemyBody>());
        StartCoroutine(Delay());
        
    }



    IEnumerator Delay()
    { 
        isAttack = true;
        yield return new WaitForSeconds(1f);
        enemyObject = null;
        isAttack = false;
        yield return null;
    }
}
