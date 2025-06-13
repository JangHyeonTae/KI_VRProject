using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : XRDirectInteractor
{
    private bool isAttack = false;

    List<Collider> colList = new();
    public LayerMask targetLayer;
    private GameObject targetObject;
    private GameObject curObject;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);


        if(targetObject)
        targetObject = args.interactableObject.transform.gameObject;


        //if((target.layer & targetLayer) != 0 || target != null)
        //{
        //    targetObject = target;
        //    Debug.Log($"{target.name}");
        //}
        Attack();
    }


    public void Attack()
    {
        if (targetObject == null)
        {
            return;
        }

        if(targetObject.layer == 9)
        {
            Manager.FightInstance.GetHitPoint(targetObject.GetComponentInChildren<EnemyBody>());
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
