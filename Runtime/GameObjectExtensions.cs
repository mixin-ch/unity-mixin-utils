
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
    }
}
