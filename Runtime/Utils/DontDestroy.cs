using UnityEngine;

namespace Mixin.Utils
{
    public class DontDestroy : MonoBehaviour
    {
        private static DontDestroy _instance;
        public static DontDestroy Instance { get { return _instance; } }

        void Awake()
        {
            // If there isn't, keep this instance and mark it as persistent
            DontDestroyOnLoad(gameObject);

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
    }
}