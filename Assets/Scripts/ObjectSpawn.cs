using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private XROrigin player;
    [SerializeField] private Vector3 offset;

    Coroutine cor;
    WaitForSeconds delay;

    [SerializeField] private int curLevel = 3;
    void Start()
    {
        spawnPos.position = player.transform.position + offset;
        
    }



    public void Spawn()
    {
        if (prefab == null) return;

        for(int i =0; i < curLevel; i++)
        { 
            GameObject obj = Instantiate(prefab, spawnPos);
        }
    }

}
