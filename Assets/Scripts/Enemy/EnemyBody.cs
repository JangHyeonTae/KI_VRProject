using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyBody : XRSimpleInteractable
{
    public DamageBox damageBox;
    public string tag;

    private void Start()
    {
        tag = gameObject.name;
    }


}
