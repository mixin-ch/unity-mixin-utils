using System.Collections;
using UnityEngine;

namespace Mixin.Utils
{
    public class SpriteFader : MonoBehaviour
    {
        public float fadeDuration = 1f;
        public GameObject targetObject; // The target object whose child objects' sprites need to be controlled

        // Find and show all SpriteRenderer components in targetObject's children
        public void ShowAllSprites()
        {
            if (targetObject == null)
            {
                Debug.LogError("Target object is not assigned!");
                return;
            }

            SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }
        }

        // Find and hide all SpriteRenderer components in targetObject's children
        public void HideAllSprites()
        {
            if (targetObject == null)
            {
                Debug.LogError("Target object is not assigned!");
                return;
            }

            SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            }
        }

        // Find and fade out all SpriteRenderer components in targetObject's children
        public void FadeOutAllSprites()
        {
            if (targetObject == null)
            {
                Debug.LogError("Target object is not assigned!");
                return;
            }

            SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                StartCoroutine(FadeOut(spriteRenderer));
            }
        }

        // Find and fade in all SpriteRenderer components in targetObject's children
        public void FadeInAllSprites()
        {
            if (targetObject == null)
            {
                Debug.LogError("Target object is not assigned!");
                return;
            }

            SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                StartCoroutine(FadeIn(spriteRenderer));
            }
        }

        // Coroutine to fade out a SpriteRenderer
        private IEnumerator FadeOut(SpriteRenderer spriteRenderer)
        {
            Color originalColor = spriteRenderer.color;
            Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(originalColor.a, targetColor.a, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = targetColor;
        }

        // Coroutine to fade in a SpriteRenderer
        private IEnumerator FadeIn(SpriteRenderer spriteRenderer)
        {
            Color originalColor = spriteRenderer.color;
            Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(originalColor.a, targetColor.a, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = targetColor;
        }
    }
}