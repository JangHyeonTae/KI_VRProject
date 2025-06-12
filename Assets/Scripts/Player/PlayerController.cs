using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isAttack = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Test test = FindObjectOfType<Test>();

        if (test == null)
            Debug.Log($"{test.name} null");


        if(other.gameObject.layer == 9 && test != null && !isAttack)
        {
            if(audioSource != null)
            {
                audioSource.Stop();
            }
            Debug.Log($"{isAttack}");
            test.OnHitPoint?.Invoke(other.gameObject.GetComponent<EnemyBody>());
            audioSource.clip = other.gameObject.GetComponent<EnemyBody>().damageBox.punchAudio;
            audioSource.Play();
            StartCoroutine(Delay());
        }

        //if(other.gameObject.layer == default)
        //{
        //    Debug.Log("default");
        //    StartCoroutine(Delay());
        //    return null;
        //}
        //else if(other.gameObject.layer == 9 && other.gameObject.layer != default)
        //{
        //    Debug.Log("hit");
        //    return other.gameObject.GetComponent<EnemyBody>();
        //}
    }

    IEnumerator Delay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        yield return null;
    }
}
