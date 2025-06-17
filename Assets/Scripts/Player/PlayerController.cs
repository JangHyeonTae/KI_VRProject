using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : XRDirectInteractor
{
    private bool isAttack = false;
    private bool canDefence = true;

    List<Collider> colList = new();
    private GameObject enemyObject;


    public Collider leftHand = null;
    public Collider rightHand = null;

    private Coroutine attackCor;
    private Coroutine defenceCor;

    public bool isDefend = false;

    [SerializeField] private InputActionProperty rightHandButton; // A ��ư (�Ǵ� Ʈ����)
    [SerializeField] private InputActionProperty leftHandButton;  // X ��ư (�Ǵ� Ʈ����)

    private void OnEnable()
    {
        base.OnEnable();

        rightHandButton.action.Enable();
        rightHandButton.action.started += OnRightPressedDown;
        rightHandButton.action.canceled += OnRightReleased;

        leftHandButton.action.Enable();
        leftHandButton.action.started += OnLeftPressedDown;
        leftHandButton.action.canceled += OnLeftReleased;
    }

    private void OnDisable()
    {
        base.OnDisable();

        rightHandButton.action.started -= OnRightPressedDown;
        rightHandButton.action.canceled -= OnRightReleased;
        rightHandButton.action.Disable();

        leftHandButton.action.started -= OnLeftPressedDown;
        leftHandButton.action.canceled -= OnLeftReleased;
        leftHandButton.action.Disable();
    }

    private void OnRightPressedDown(InputAction.CallbackContext ctx)
    {
        if (rightHandButton == null) return;
        RightStartDefend();
    }

    private void OnRightReleased(InputAction.CallbackContext ctx)
    {
        RightStopDefend();
    }

    private void OnLeftPressedDown(InputAction.CallbackContext ctx)
    {
        if (leftHandButton == null) return;
        LeftStartDefend();
    }

    private void OnLeftReleased(InputAction.CallbackContext ctx)
    {
        LeftStopDefend();
    }

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



    private void LeftStartDefend()
    {
        if (isDefend || !canDefence) return;

        leftHand.enabled = false;
        isDefend = true;
        Debug.Log("���� ��� ����");
    }

    private void RightStartDefend()
    {
        if (isDefend || !canDefence) return;

        rightHand.enabled = false;
        isDefend = true;
        Debug.Log("������ ��� ����");
    }

    private void LeftStopDefend()
    {
        if (!isDefend) return;

        leftHand.enabled = true;
        isDefend = false;
        Debug.Log("���� ��� ����");
    }

    private void RightStopDefend()
    {
        if (!isDefend) return;

        rightHand.enabled = true;
        isDefend = false;
        Debug.Log("������ ��� ����");
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

        Manager.FightInstance.GetHitPoint(enemyObject.transform.GetComponent<EnemyBody>());
        
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
