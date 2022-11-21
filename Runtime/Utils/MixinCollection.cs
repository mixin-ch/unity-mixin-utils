using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// Collection of Mixin.
    /// </summary>
    public class MixinCollection : MonoBehaviour
    {
        /// <summary>
        /// This just returns the official Mixin Color. <br></br>
        /// Currently Color.red.
        /// </summary>
        /// <returns></returns>
        public static Color GetMixinColor()
        {
            return Color.red;
        }
    }
}