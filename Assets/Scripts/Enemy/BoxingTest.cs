using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingTest : MonoBehaviour
{
    [SerializeField] private GameObject[] hitPoint;
    [SerializeField] private GameObject avatar;


    Coroutine cor;
    Renderer render;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            int index = GetIndex(other.gameObject);
            if(index != -1)
            {
                cor = StartCoroutine(HitCollider(index));
            }
            
        }
    }

    private int GetIndex(GameObject target)
    {
        for(int i=0; i < hitPoint.Length; i++)
        {
            if(hitPoint[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    IEnumerator HitCollider(int index)
    {
        if(index == 0)
        {
            avatar.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if(index == 1)
        {
            avatar.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            avatar.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        
        yield return new WaitForSeconds(1f);
        avatar.GetComponent<MeshRenderer>().material.color = Color.white;
    }

}
