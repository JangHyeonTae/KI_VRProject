using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingTest : MonoBehaviour
{
    [SerializeField] private SphereCollider[] hitPoint;
    [SerializeField] private GameObject avatar;
    Coroutine cor;
    Renderer render;
    Material mat;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            cor = StartCoroutine(HitCollider());
        }
    }

    IEnumerator HitCollider()//int index)
    {
        avatar.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        avatar.GetComponent<MeshRenderer>().material.color = Color.white;
    }

}
