using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public Stack<PooledObject> poolStack;
    public int size;
    public PooledObject targetPrefab;
    public GameObject poolParent;


    public ObjectPool(Transform Parent, int _size, PooledObject targetPrefab)
    {
        Init(Parent, _size, targetPrefab);
    }

    public void Init(Transform Parent, int _size, PooledObject _targetPrefab)
    {
        poolStack = new Stack<PooledObject>(_size);
        targetPrefab = _targetPrefab;
        poolParent = new GameObject($"{targetPrefab.name} Parent");
        poolParent.transform.parent = Parent;

        for(int i=0; i< _size; i++)
        {
            PooledObject obj = MonoBehaviour.Instantiate(targetPrefab, poolParent.transform);
            obj.gameObject.SetActive(false);
            poolStack.Push(obj);
        }
    }

    public PooledObject GetPool()
    {
        if(poolStack.Count == 0) 
            MonoBehaviour.Instantiate(targetPrefab, poolParent.transform);
        

        PooledObject obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        obj.PoolInit(this);
        return obj;
    }

    public void SetReturnPool(PooledObject obj)
    {
        obj.PoolInit(this);
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }


}
