using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPool objectPool { get; set; }

    public void PoolInit(ObjectPool _objectPool)
    {
        objectPool = _objectPool;
    }

    public void ReturnPool()
    {
        objectPool.SetReturnPool(this);
    }
}
