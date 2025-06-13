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
    public float radius;
    public Vector3 offset;
    private Collider targetColllider;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
    }

    public void Attack()
    {
        Collider col = GetComponent<Collider>();
        Manager.FightInstance.GetHitPoint(col.gameObject.GetComponent<EnemyBody>());
        StartCoroutine(Delay());
    }



    IEnumerator Delay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        yield return null;
    }
}
