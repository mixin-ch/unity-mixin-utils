using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectManager 
{
    public static void DestroyChildren(this GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }


    /// <summary>
    /// This Method instantiates a Prefab
    /// </summary>
    /// <param name="prefab">The Prefab you want to instantiate</param>
    /// <param name="name">The Name of the new GameObject</param>
    /// <param name="parent">The Parent GameObject</param>
    /// <returns></returns>
    public static GameObject GeneratePrefab(this GameObject prefab, GameObject parent, string name)
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.name = name;
        if (parent != null)
            obj.transform.SetParent(parent.transform, false);

        return obj;
    }
    public static GameObject GeneratePrefab(this GameObject prefab, GameObject parent)
    {
        return GeneratePrefab(prefab, parent, prefab.name);
    }
}
