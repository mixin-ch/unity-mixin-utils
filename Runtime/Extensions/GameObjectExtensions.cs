
using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="GameObject"/>.
    /// </summary>
    static class GameObjectExtensions
    {
        /// <summary>
        /// Destroys the children of the GameObject.
        /// </summary>
        public static void DestroyChildren(this GameObject parent)
        {
            foreach (Transform child in parent.transform)
                Object.Destroy(child.gameObject);
        }

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
}
