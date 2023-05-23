using System.Collections;
using UnityEngine;

namespace Mixin.Utils
{
    public class ColorTransitionPlayer : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private ColorTransitionSO _colorTransitionSO;

        [SerializeField]
        private float _framerate = 24f; // Time in seconds to show each frame

        private Color _fromColor;
        private Color _toColor;
        private float _timeElapsed;

        private Coroutine _playCoroutine;

        private ColorTransition ColorTransition => _colorTransitionSO?.ColorTransition;

        public void Play()
        {
            if (ColorTransition == null)
                return;

            if (ColorTransition.Interval <= 0)
                return;

            if (_playCoroutine != null)
                StopCoroutine(_playCoroutine);

            if (_spriteRenderer == null)
            {
                Debug.LogError("AnimationSequence must be attached to a GameObject with a SpriteRenderer component.");
                return;
            }

            _fromColor = _spriteRenderer.color;
            _toColor = GetRandomColor();
            _timeElapsed = 0;

            _playCoroutine = StartCoroutine(PlayTransition());
        }

        public void Stop()
        {
            StopCoroutine(_playCoroutine);
        }

        private IEnumerator PlayTransition()
        {
            while (true)
            {
                yield return new WaitForSeconds(_framerate);

                _timeElapsed += _framerate;

                while (_timeElapsed >= ColorTransition.Interval)
                {
                    _timeElapsed -= ColorTransition.Interval;
                    _fromColor = _toColor;
                    _toColor = GetRandomColor();
                }

                _spriteRenderer.color = _fromColor.MixRGB(_toColor, _timeElapsed / ColorTransition.Interval);
            }
        }

        private Color GetRandomColor()
        {
            Color[] colorArray = ColorTransition?.ColorArray;

            if (colorArray == null)
                return Color.white;


            return colorArray[Random.Range(0, colorArray.Length)];
        }
    }
}
