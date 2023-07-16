using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils
{
    public class ScaleOverTime : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gameObjectToScale; // The game object to scale

        public float scaleFactor = 1.2f; // The amount to scale the object by
        public float duration = 1.0f; // The duration of the scaling animation

        private Vector3 originalScale; // The original scale of the object

        // Start is called before the first frame update
        void Start()
        {
            if (_gameObjectToScale == null)
            {
                _gameObjectToScale = gameObject;
            }

            originalScale = _gameObjectToScale.transform.localScale;
            _gameObjectToScale.transform.localScale = Vector3.zero;
        }

        // Easing function
        private float EaseInOutSine(float t)
        {
            return -0.5f * (Mathf.Cos(Mathf.PI * t) - 1.0f);
        }

        // Coroutine for scaling in
        IEnumerator ScaleIn()
        {
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / duration;
                float scale = EaseInOutSine(t);
                _gameObjectToScale.transform.localScale = originalScale * Mathf.Lerp(0.0f, scaleFactor, scale);
                yield return null;
            }
        }

        // Coroutine for scaling out
        IEnumerator ScaleOut()
        {
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / duration;
                float scale = EaseInOutSine(t);
                _gameObjectToScale.transform.localScale = originalScale * Mathf.Lerp(scaleFactor, 0.0f, scale);
                yield return null;
            }
        }

        // Call this function to scale in the object
        public void DoScaleIn()
        {
            StopAllCoroutines();
            StartCoroutine(ScaleIn());
        }

        // Call this function to scale out the object
        public void DoScaleOut()
        {
            StopAllCoroutines();
            StartCoroutine(ScaleOut());
        }
    }
}