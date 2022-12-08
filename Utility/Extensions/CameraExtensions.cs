using AAA.Utility.Extensions;
using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class CameraExtensions
    {
        public static float GetOrthograpicCameraDistanceAtFrustumHeight(float frustumHeight)
        {
            return frustumHeight * 0.5f;
        }

        public static float GetOrthograpicCameraDistanceAtFrustumWidth(float frustumWidth)
        {
            return frustumWidth * 0.5f;
        }

        public static float GetFrustumHeightAtPerspectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            return 2.0f * cameraDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static float GetFrustumWidthAtPrespectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);

            return 2.0f * cameraDistance * Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static Vector2 GetFrustumSizeAtPerspectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            float width = camera.GetFrustumWidthAtPrespectiveCameraDistance(cameraDistance);
            float height = camera.GetFrustumHeightAtPerspectiveCameraDistance(cameraDistance);

            return new Vector2(width, height);
        }

        public static float GetPrespectiveCameraDistanceAtFrustumHeight(this Camera camera, float frustumHeight)
        {
            return frustumHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static float GetPrespectiveCameraDistanceAtFrustumWidth(this Camera camera, float frustumWidth)
        {
            return camera.GetPrespectiveCameraDistanceAtFrustumWidth(frustumWidth, camera.aspect);
        }

        public static float GetPrespectiveCameraDistanceAtFrustumWidth(this Camera camera, float frustumWidth, float aspect)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, aspect);

            return frustumWidth * 0.5f / Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static Vector2 GetPrespectiveCameraDistanceAtFrustumSize(this Camera camera, Vector2 frustumSize)
        {
            float width = camera.GetPrespectiveCameraDistanceAtFrustumWidth(frustumSize.x);
            float height = camera.GetPrespectiveCameraDistanceAtFrustumHeight(frustumSize.y);

            return new Vector2(width, height);
        }

        public static Vector3 ClampedViewportToWorldPoint(this Camera camera, Vector2 viewportPosition)
        {
            return camera.ClampedViewportToWorldPoint(viewportPosition, camera.nearClipPlane);
        }

        public static Vector3 ClampedViewportToWorldPoint(this Camera camera, Vector2 viewportPosition, float cameraDistance)
        {
            Vector3 finalPosition = new(
                Mathf.Clamp(viewportPosition.x, 0f, 1f),
                Mathf.Clamp(viewportPosition.y, 0f, 1f),
                cameraDistance
            );

            return camera.ViewportToWorldPoint(finalPosition);
        }

        public static Vector3 ClampedViewportWithOrthographicSizeToWorldPoint(
            this Camera camera,
            float orthographicSize,
            Vector2 viewportPosition,
            float cameraDistance
            )
        {
            Transform cameraTransform = camera.transform;

            float aspectRatio = MathExtensions.Divide(camera.pixelWidth, camera.pixelHeight);

            Vector2 halfScreenSize = new(
                orthographicSize * aspectRatio,
                orthographicSize
            );

            float screenWidth = orthographicSize * aspectRatio * 2f;
            float screenHeight = orthographicSize * 2f;

            Vector3 screenPosition = new(
                screenWidth * Mathf.Clamp(viewportPosition.x, 0f, 1f),
                screenHeight * Mathf.Clamp(viewportPosition.y, 0f, 1f),
                cameraDistance
            );

            Vector3 cameraPosition = cameraTransform.position;

            Vector3 finalPosition = new(
                (cameraPosition.x - halfScreenSize.x) + screenPosition.x,
                (cameraPosition.y - halfScreenSize.y) + screenPosition.y,
                screenPosition.z
            );

            Vector3 rotatedPosition = cameraTransform.rotation.RotatePointAroundPivot(finalPosition, cameraPosition);

            return rotatedPosition;
        }
    }
}
