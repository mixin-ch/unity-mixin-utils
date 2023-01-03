using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// A component to either hide or show this object on a specific device.
    /// </summary>
    public class VisibilityCondition : MonoBehaviour
    {
        /// <summary>
        /// Define the visibility of a specific platform.
        /// </summary>
        [Tooltip("Define the visibility of a specific platform.")]
        [SerializeField]
        private MixinDictionary<RuntimePlatform, bool> _platformVisibilityDictionary;

        void Start()
        {
            // Get the current platform.
            RuntimePlatform currentPlatform = Application.platform;

            // Check if it contains the current platform.
            if (_platformVisibilityDictionary.ContainsKey(currentPlatform))
                // If so, then make the Object (in)visible.
                gameObject.SetActive(_platformVisibilityDictionary[currentPlatform]);
        }
    }
}
