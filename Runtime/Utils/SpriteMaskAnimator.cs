using UnityEngine;
using UnityEngine.UI;

namespace Mixin.Utils
{
    [ExecuteAlways]
    public class SpriteMaskAnimator : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private SpriteMask _mask;

        // Update is called once per frame
        void Update()
        {
            _mask.sprite = _sprite.sprite;
        }
    }
}