using UnityEngine;

namespace Mixin.Utils
{
    public static class MousePointer
    {
        // check if the cursor is currently over the object
        public static bool IsCursorOverObject(GameObject objectToFade, SpriteRenderer objectSpriteRenderer)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objectPosition = objectToFade.transform.position;
            Vector2 objectSize = objectSpriteRenderer.bounds.size;

            // check if the mouse position is within the object's bounds
            if (mousePosition.x >= objectPosition.x - objectSize.x / 2 &&
                mousePosition.x <= objectPosition.x + objectSize.x / 2 &&
                mousePosition.y >= objectPosition.y - objectSize.y / 2 &&
                mousePosition.y <= objectPosition.y + objectSize.y / 2)
            {
                return true;
            }
            return false;
        }
    }
}
