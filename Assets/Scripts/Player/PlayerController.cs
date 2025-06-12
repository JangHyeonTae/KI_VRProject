using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Test test = FindObjectOfType<Test>();

        if (test == null)
            Debug.Log($"{test.name} null");


        if(other.gameObject.layer == 9 && test != null)
        {
            Debug.Log($"{other.gameObject.GetComponent<EnemyBody>().damageBox.trigPoint}");
            Debug.Log($"playerController : {other.gameObject.GetComponent<EnemyBody>().damageBox.value}");
            test.OnHitPoint?.Invoke(other.gameObject.GetComponent<EnemyBody>());
        }
        else
        {
            return;
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
        yield return new WaitForSeconds(1f);
        Debug.Log("잘못때림");
        yield return null;
    }
}
