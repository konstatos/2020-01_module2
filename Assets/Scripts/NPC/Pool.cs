using ModestTree;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component
{
    public Stack<T> All = new Stack<T>();
    public Transform Root;
    public T Prefab;

    public Pool(T prefab, Transform root = null)
    {
        Root = root;
        Prefab = prefab;
        if (!prefab)
            Log.Error("Нет префаба для инициализации пулла!");
        //Debug.LogWarning("Init pool: " + prefab);
    }

    public T GetFromPool()
    {
        if (All.Count == 0)
            return Object.Instantiate(Prefab, Root);
        var h = All.Pop();
        if (!h)
        {
            Log.Error("Элемент в пулле уничтожен - айайай! Тип=" + Prefab);
            return GetFromPool();
        }
        h.gameObject.SetActive(true);
        return h;
    }

    public void ToPool(T hit)
    {
        All.Push(hit);
        hit.gameObject.SetActive(false);
    }
}
