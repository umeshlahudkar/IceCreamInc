using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private Transform parent;
    private Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab, Transform parent, int initialSize)
    {
        this.prefab = prefab;
        this.parent = parent;
        InitializePool(initialSize);
    }

    private void InitializePool(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            T newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(false);
            pool.Enqueue(newObj);
        }
    }

    public T GetObjectFromPool()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(prefab);
            return newObj;
        }
    }

    public void ReturnObjectToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
