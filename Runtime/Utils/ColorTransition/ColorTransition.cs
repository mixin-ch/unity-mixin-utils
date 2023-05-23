using UnityEngine;

namespace Mixin.Utils
{
    [System.Serializable]
    public class ColorTransition
    {
        [SerializeField]
        private Color[] _colorArray;
        [SerializeField]
        private float _interval;

        public Color[] ColorArray { get => _colorArray; }
        public float Interval { get => _interval; }
    }
}
