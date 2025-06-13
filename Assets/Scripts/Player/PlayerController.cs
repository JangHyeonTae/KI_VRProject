using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : XRDirectInteractor
{
    private bool isAttack = false;

    List<Collider> colList = new();
    private GameObject targetObject;
    private Coroutine attackCor;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        targetObject = args.interactableObject.transform.gameObject;

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
        if (targetObject == null)
        {
            yield break;
        }

        if (targetObject.layer == 9)
        {
            Manager.FightInstance.GetHitPoint(targetObject.transform.GetComponentInChildren<EnemyBody>());
            StartCoroutine(Delay());
        }
    }



    IEnumerator Delay()
    { 
        isAttack = true;
        yield return new WaitForSeconds(1f);
        targetObject = null;
        isAttack = false;
        yield return null;
    }
}
