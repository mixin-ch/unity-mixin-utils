
using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="GameObject"/>.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Destroys the children of the GameObject.
        /// </summary>
        public static void DestroyChildren(this GameObject parent)
        {
            foreach (Transform child in parent.transform)
                Object.Destroy(child.gameObject);
        }

        /// <summary>
        /// This Method instantiates a Prefab
        /// </summary>
        /// <param name="prefab">The Prefab you want to instantiate</param>
        /// <param name="name">The Name of the new GameObject</param>
        /// <param name="parent">The Parent GameObject</param>
        public static GameObject GeneratePrefab(this GameObject prefab, GameObject parent, string name)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.name = name;
            if (parent != null)
                obj.transform.SetParent(parent.transform, false);

            return obj;
        }

        /// <inheritdoc cref="GeneratePrefab(GameObject, GameObject, string)"/>
        public static GameObject GeneratePrefab(this GameObject prefab, GameObject parent)
        {
            return GeneratePrefab(prefab, parent, prefab.name);
        }
    }
}
