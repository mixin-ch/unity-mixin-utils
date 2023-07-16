using UnityEngine;

namespace Mixin.Utils
{
    [CreateAssetMenu(fileName = "ColorTransitionSO", menuName = "Mixin/ColorTransition/ColorTransitionSO")]
    public class ColorTransitionSO : ScriptableObject
    {
        [SerializeField]
        private ColorTransition _colorTransition;

        public ColorTransition ColorTransition { get => _colorTransition; }
    }
}
