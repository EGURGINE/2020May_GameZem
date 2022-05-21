using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour  where T : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected int count;
    protected Queue<T> objQueue = new Queue<T>();

    protected virtual void Awake()
    {
        Initialize(count);
    }

    private void Initialize(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            objQueue.Enqueue(CreateObj());
        }
    }

    private T CreateObj()
    {
        var newObj = Instantiate(prefab.GetComponent<T>());
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public T GetObj(Vector3 _pos)
    {
        if (objQueue.Count > 0)
        {
            var obj = objQueue.Dequeue();
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.SetParent(null);
            obj.transform.position = _pos;
            return obj;
        }
        else
        {
            return CreateObj();
        }
    }

    public T GetObj(Vector3 _pos, Vector3 _dir)
    {
        if (objQueue.Count > 0)
        {
            var obj = objQueue.Dequeue();
            obj.gameObject.transform.SetParent(null);
            obj.transform.position = _pos;
            obj.transform.forward = _dir;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return CreateObj();
        }
    }

    public void Return(T obj)
    {
        obj.transform.position = Vector3.zero;
        obj.gameObject.SetActive(false);
        obj.gameObject.transform.SetParent(transform);
        objQueue.Enqueue(obj);
    }
}
