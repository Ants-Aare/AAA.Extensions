using System.Collections.Generic;
using UnityEngine;

namespace AAA.Extensions
{
    public static class TransformExtensions
    {
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void LocalReset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void SetPositionX(this Transform transform, float x)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(x, currPosition.y, currPosition.z);
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, y, currPosition.z);
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, currPosition.y, z);
        }

        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(x, y, currPosition.z);
        }

        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(x, currPosition.y, z);
        }

        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            var currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, y, z);
        }

        public static void SetPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetPositionXY(position.x, position.y);
        }

        public static void SetPositionXZ(this Transform transform, Vector2 position)
        {
            transform.SetPositionXZ(position.x, position.y);
        }

        public static void SetPositionYZ(this Transform transform, Vector2 position)
        {
            transform.SetPositionYZ(position.x, position.y);
        }

        public static void SetLocalPositionX(this Transform transform, float x)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, currPosition.y, currPosition.z);
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, y, currPosition.z);
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, currPosition.y, z);
        }

        public static void SetLocalPositionXY(this Transform transform, float x, float y)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, y, currPosition.z);
        }

        public static void SetLocalPositionXZ(this Transform transform, float x, float z)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, currPosition.y, z);
        }

        public static void SetLocalPositionYZ(this Transform transform, float y, float z)
        {
            var currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, y, z);
        }

        public static void SetLocalPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetLocalPositionXY(position.x, position.y);
        }

        public static void SetRotationX(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(angleDegrees, currRotation.y, currRotation.z);
        }

        public static void SetRotationY(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currRotation.x, angleDegrees, currRotation.z);
        }

        public static void SetRotationZ(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currRotation.x, currRotation.y, angleDegrees);
        }

        public static void SetLocalRotationX(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(angleDegrees, currRotation.y, currRotation.z);
        }

        public static void SetLocalRotationY(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currRotation.x, angleDegrees, currRotation.z);
        }

        public static void SetLocalRotationZ(this Transform transform, float angleDegrees)
        {
            var currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currRotation.x, currRotation.y, angleDegrees);
        }

        public static void SetLocalScaleX(this Transform transform, float x)
        {
            var currScale = transform.localScale;
            transform.localScale = new Vector3(x, currScale.y, currScale.z);
        }

        public static void SetLocalScaleXY(this Transform transform, Vector2 xy)
        {
            var currScale = transform.localScale;
            transform.localScale = new Vector3(xy.x, xy.y, currScale.z);
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            var currScale = transform.localScale;
            transform.localScale = new Vector3(currScale.x, y, currScale.z);
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            var currScale = transform.localScale;
            transform.localScale = new Vector3(currScale.x, currScale.y, z);
        }

        public static List<T> GetChildren<T>(this T transform)
            where T : Transform
        {
            var ret = new List<T>();

            for (var i = 0; i < transform.childCount; ++i)
            {
                ret.Add((T)transform.GetChild(i));
            }

            return ret;
        }

        public static IEnumerable<T> GetChildrenEnumerable<T>(this T transform)
            where T : Transform
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                yield return (T)transform.GetChild(i);
            }
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.RemoveParentAndDestroy();
            }
        }

        public static void DestroyImmediateChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyActiveChildren(this Transform t)
        {
            for (int i = 0; i <= t.childCount - 1; i++)
            {
                var gameObject = t.GetChild(i).gameObject;
                if (gameObject.activeSelf)
                    Object.Destroy(gameObject);
            }
        }

        public static void DestroyInactiveChildren(this Transform t)
        {
            for (int i = 0; i <= t.childCount - 1; i++)
            {
                var gameObject = t.GetChild(i).gameObject;
                if (!gameObject.activeSelf)
                    Object.Destroy(gameObject);
            }
        }
    }
}