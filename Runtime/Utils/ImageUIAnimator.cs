using UnityEngine;
using UnityEngine.UI;

namespace Mixin.Utils
{
    [ExecuteAlways]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageUIAnimator : MonoBehaviour
    {
        private Image img;
        private SpriteRenderer sprite;

        // Use this for initialization
        void Awake()
        {
            img = GetComponent<Image>();
            sprite = GetComponent<SpriteRenderer>();

            // Hide the SpriteRenderer
            sprite.color = new Color(0, 0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            img.sprite = sprite.sprite;
        }
    }
}