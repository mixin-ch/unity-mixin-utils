
using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="Vector2"/>.
    /// </summary>
    static class Vector2Extensions
    {
        /// <summary>
        /// Transforms radian value to Vector2.
        /// </summary>
        public static Vector2 RadianToVector2(this float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Transforms Vector2 to radian value.
        /// </summary>
        public static float Vector2ToRadian(this Vector2 vector2)
        {
            return Vector2ToDegree(vector2) * Mathf.Deg2Rad;
        }

        /// <summary>
        /// Transforms degree value to Vector2.
        /// </summary>
        public static Vector2 DegreeToVector2(this float degree)
        {
            float radian = degree * Mathf.Deg2Rad;
            return radian.RadianToVector2();
        }

        /// <summary>
        /// Transforms Vector2 to degree value.
        /// </summary>
        public static float Vector2ToDegree(this Vector2 vector2)
        {
            float angle = Vector2.Angle(new Vector2(1, 0), vector2);

            if (vector2.y < 0)
                angle = 360 - angle;

            return angle;
        }

        /// <summary>
        /// Transforms revolution value to Vector2.
        /// One revolution is 360 degrees.
        /// </summary>
        public static Vector2 RevolutionToVector2(this float revolution)
        {
            return DegreeToVector2(revolution * 360f);
        }

        /// <summary>
        /// Transforms Vector2 to revolution value.
        /// One revolution is 360 degrees.
        /// </summary>
        public static float Vector2ToRevolution(this Vector2 vector2)
        {
            return Vector2ToDegree(vector2) / 360f;
        }

        /// <summary>
        /// Transforms Vector2 to Quaternion.
        /// </summary>
        public static Quaternion Vector2ToQuaternion(this Vector2 vector2)
        {
            return Quaternion.Euler(0, 0, vector2.Vector2ToDegree());
        }
    }
}
