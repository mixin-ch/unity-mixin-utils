using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils
{
    public class CameraEdgeScroller : MonoBehaviour
    {
        public GameObject scrollLimit;
        public float scrollSpeed = 5f;
        public float scrollZoneWidth = 150f;
        public float cameraScrollLimitModifier = 0;

        private float scrollLimitLeft;
        private float scrollLimitRight;
        private float cameraHalfWidth;

        private void Start()
        {
            SetBorders();
        }

        private void SetBorders()
        {
            // Calculate the half width of the camera's viewport
            cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

            // Calculate the scroll limits based on the scrollLimit object's size
            Renderer limitRenderer = scrollLimit.GetComponent<Renderer>();
            scrollLimitLeft = scrollLimit.transform.position.x - limitRenderer.bounds.extents.x + cameraHalfWidth + cameraScrollLimitModifier;
            scrollLimitRight = scrollLimit.transform.position.x + limitRenderer.bounds.extents.x - cameraHalfWidth - cameraScrollLimitModifier;
        }

        private void Update()
        {
            // Get the mouse position
            float mousePositionX = Input.mousePosition.x;

            // Calculate the center of the screen
            float screenCenterX = Screen.width * 0.5f;

            // Calculate the left and right scroll boundaries
            float scrollBoundaryLeft = screenCenterX - scrollZoneWidth;
            float scrollBoundaryRight = screenCenterX + scrollZoneWidth;

            // Check if the mouse is outside the scroll boundaries
            if (mousePositionX < scrollBoundaryLeft && transform.position.x > scrollLimitLeft)
            {
                // Calculate the desired scroll amount based on the distance from the left boundary
                float scrollAmount = Mathf.Lerp(0, scrollSpeed, (scrollBoundaryLeft - mousePositionX) / scrollZoneWidth);
                transform.Translate(Vector3.left * scrollAmount * Time.deltaTime);
            }
            else if (mousePositionX > scrollBoundaryRight && transform.position.x < scrollLimitRight)
            {
                // Calculate the desired scroll amount based on the distance from the right boundary
                float scrollAmount = Mathf.Lerp(0, scrollSpeed, (mousePositionX - scrollBoundaryRight) / scrollZoneWidth);
                transform.Translate(Vector3.right * scrollAmount * Time.deltaTime);
            }
        }

        public void UpdateBorder(GameObject borderObject)
        {
            scrollLimit = borderObject;
            SetBorders();
        }
    }
}