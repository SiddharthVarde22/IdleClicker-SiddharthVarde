using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    Stack<T> m_pool;
    Func<T> m_onFactory;
    Action<T> m_onGet, m_onRelease, m_onDestroy;

    public ObjectPool(int a_minimumPoolSize, Func<T> a_onFactory, Action<T> a_onGet, Action<T> a_onRelease, Action<T> a_onDestroy)
    {
        m_pool = new Stack<T>(a_minimumPoolSize);
        m_onFactory = a_onFactory;
        m_onGet = a_onGet;
        m_onRelease = a_onRelease;
        m_onDestroy = a_onDestroy;
    }

    public T Get()
    {
        T l_objectToReturn;
        if(m_pool.Count > 0)
        {
            l_objectToReturn = m_pool.Pop();
            m_onGet.Invoke(l_objectToReturn);
        }
        else
        {
            l_objectToReturn = m_onFactory.Invoke();
        }
        return l_objectToReturn;
    }
    public void Release(T a_objectToRelease)
    {
        m_pool.Push(a_objectToRelease);
        m_onRelease(a_objectToRelease);
    }
    public void Destroy(T a_objectToDelete)
    {
        m_onRelease(a_objectToDelete);
        m_onDestroy(a_objectToDelete);
    }

    ~ObjectPool()
    {
        for(int i = 0, l_count = m_pool.Count; i < l_count; i++)
        {
            T l_object = m_pool.Pop();
            m_onRelease(l_object);
            m_onDestroy(l_object);
        }
        m_pool = null;
        m_onFactory = null;
        m_onGet = m_onRelease = m_onDestroy = null;
    }
}
