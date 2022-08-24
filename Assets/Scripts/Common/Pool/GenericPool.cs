using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class GenericPool<T> : MonoBehaviour, IPool where T : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;
    protected List<T> pool;

    public Type MemberType => typeof(T);

    virtual protected void Awake()
    {
        pool = new List<T>(10);
    }

    /// <summary>
    /// Returns component from the pool.
    /// </summary>
    /// <returns></returns>
    virtual public T Get()
    {
        T obj;
        if (pool.Count > 0)
        {

            obj = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            obj = InstantiateNew();
        }
        return obj;
    }

    /// <summary>
    /// Adds component to the pool.
    /// </summary>
    /// <param name="obj"></param>
    virtual public void Add(T obj)
    {
        if (!pool.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            pool.Add(obj);
            // Debug.Log($"Adding {obj.name} to pool", obj);
        }
    }

    public void Add(GameObject go)
    {
        var member = go.GetComponent<T>();
        if (member == null)
        {
            Debug.LogException(new ArgumentException($"{go.name} doesn't have component of type {typeof(T).FullName}"));
            return;
        }
        Add(member);
    }

    /// <summary>
    /// Instantiates a new game object from the given prefab and returns component of the required type.
    /// </summary>
    /// <returns>Required component from the instantiated game object.</returns>
    protected T InstantiateNew()
    {
        if (prefab == null)
        {
            throw new NullReferenceException("Prefab is null");
        }

        GameObject TGO = Instantiate<GameObject>(prefab);
        T obj = TGO.GetComponent<T>();

        // make sure that instantiated game object has the requried component.
        if (obj == null)
        {
            throw new UnityException($"Prefab doesn't have component type of {typeof(T)}");
        }

        return obj;
    }
}