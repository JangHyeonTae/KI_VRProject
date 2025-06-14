using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyBody : XRSimpleInteractable
{
    public DamageBox damageBox;
    public string tag;
    private void Awake()
    {
        
        base.Awake();
    }

    private void Start()
    {
        tag = gameObject.name;
    }

    private void OnEnable()
    {
        base.OnEnable();
    }

    private void OnDisable()
    {
        base.OnDisable();
    }


}
