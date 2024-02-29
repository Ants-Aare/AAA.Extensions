using System;
using UnityEngine;

namespace AAA.Extensions
{
    //inspired by https://github.com/Unity-Technologies/Graphics/pull/2287/files#diff-cc2ed84f51a3297faff7fd239fe421ca1ca75b9643a22f7808d3a274ff3252e9R195
    public static class DebugExtensions
    {
        static readonly Vector4[] SNdcFrustum =
        {
            new(-1, 1, -1, 1),
            new(1, 1, -1, 1),
            new(1, -1, -1, 1),
            new(-1, -1, -1, 1),

            new(-1, 1, 1, 1),
            new(1, 1, 1, 1),
            new(1, -1, 1, 1),
            new(-1, -1, 1, 1)
        };

        // Cube with edge of length 1
        private static readonly Vector4[] SUnitCube =
        {
            new(-0.5f, 0.5f, -0.5f, 1),
            new(0.5f, 0.5f, -0.5f, 1),
            new(0.5f, -0.5f, -0.5f, 1),
            new(-0.5f, -0.5f, -0.5f, 1),

            new(-0.5f, 0.5f, 0.5f, 1),
            new(0.5f, 0.5f, 0.5f, 1),
            new(0.5f, -0.5f, 0.5f, 1),
            new(-0.5f, -0.5f, 0.5f, 1)
        };

        // Sphere with radius of 1
        private static readonly Vector4[] SUnitSphere = MakeUnitSphere(16);

        // Square with edge of length 1
        private static readonly Vector4[] SUnitSquare =
        {
            new(-0.5f, 0.5f, 0, 1),
            new(0.5f, 0.5f, 0, 1),
            new(0.5f, -0.5f, 0, 1),
            new(-0.5f, -0.5f, 0, 1),
        };

        private static Vector4[] MakeUnitSphere(int len)
        {
            Debug.Assert(len > 2);
            var v = new Vector4[len * 3];
            for (var i = 0; i < len; i++)
            {
                var f = i / (float)len;
                var c = Mathf.Cos(f * (float)(Math.PI * 2.0));
                var s = Mathf.Sin(f * (float)(Math.PI * 2.0));
                v[0 * len + i] = new Vector4(c, s, 0, 1);
                v[1 * len + i] = new Vector4(0, c, s, 1);
                v[2 * len + i] = new Vector4(s, 0, c, 1);
            }

            return v;
        }

        public static void DrawFrustum(Matrix4x4 projMatrix)
        {
            DrawFrustum(projMatrix, Color.red, Color.magenta, Color.blue);
        }

        public static void DrawFrustum(Matrix4x4 projMatrix, Color near, Color edge, Color far)
        {
            var v = new Vector4[SNdcFrustum.Length];
            var m = projMatrix.inverse;

            for (var i = 0; i < SNdcFrustum.Length; i++)
            {
                var s = m * SNdcFrustum[i];
                v[i] = s / s.w;
            }

            // Near
            for (var i = 0; i < 4; i++)
            {
                var s = v[i];
                var e = v[(i + 1) % 4];
                Debug.DrawLine(s, e, near);
            }

            // Far
            for (var i = 0; i < 4; i++)
            {
                var s = v[4 + i];
                var e = v[4 + ((i + 1) % 4)];
                Debug.DrawLine(s, e, far);
            }

            // Middle
            for (var i = 0; i < 4; i++)
            {
                var s = v[i];
                var e = v[i + 4];
                Debug.DrawLine(s, e, edge);
            }
        }

        public static void DrawFrustumSplits(Matrix4x4 projMatrix, float splitMaxPct, Vector3 splitPct, int splitStart, int splitCount, Color color)
        {
            var v = SNdcFrustum;
            var m = projMatrix.inverse;

            // Compute camera frustum
            var f = new Vector4[SNdcFrustum.Length];
            for (var i = 0; i < SNdcFrustum.Length; i++)
            {
                var s = m * v[i];
                f[i] = s / s.w;
            }

            // Compute shadow far plane/quad
            var qMax = new Vector4[4];
            for (var i = 0; i < 4; i++)
            {
                qMax[i] = Vector4.Lerp(f[i], f[4 + i], splitMaxPct);
            }

            // Draw Shadow far/max quad
            for (var i = 0; i < 4; i++)
            {
                var s = qMax[i];
                var e = qMax[(i + 1) % 4];
                Debug.DrawLine(s, e, Color.black);
            }

            // Compute split quad (between near/shadow far)
            var q = new Vector4[4];
            for (var j = splitStart; j < splitCount; j++)
            {
                var d = splitPct[j];
                for (var i = 0; i < 4; i++)
                {
                    q[i] = Vector4.Lerp(f[i], qMax[i], d);
                }

                // Draw
                for (var i = 0; i < 4; i++)
                {
                    var s = q[i];
                    var e = q[(i + 1) % 4];
                    Debug.DrawLine(s, e, color);
                }
            }
        }

        public static void DrawBox(Vector4 pos, Vector3 size, Color color, float duration = 0.0f)
        {
            var v = SUnitCube;
            var sz = new Vector4(size.x, size.y, size.z, 1);
            for (var i = 0; i < 4; i++)
            {
                var s = pos + Vector4.Scale(v[i], sz);
                var e = pos + Vector4.Scale(v[(i + 1) % 4], sz);
                Debug.DrawLine(s, e, color, duration);
            }

            for (var i = 0; i < 4; i++)
            {
                var s = pos + Vector4.Scale(v[4 + i], sz);
                var e = pos + Vector4.Scale(v[4 + ((i + 1) % 4)], sz);
                Debug.DrawLine(s, e, color, duration);
            }

            for (var i = 0; i < 4; i++)
            {
                var s = pos + Vector4.Scale(v[i], sz);
                var e = pos + Vector4.Scale(v[i + 4], sz);
                Debug.DrawLine(s, e, color, duration);
            }
        }

        public static void DrawBox(Matrix4x4 transform, Color color)
        {
            var v = SUnitCube;
            var m = transform;
            for (var i = 0; i < 4; i++)
            {
                var s = m * v[i];
                var e = m * v[(i + 1) % 4];
                Debug.DrawLine(s, e, color);
            }

            for (var i = 0; i < 4; i++)
            {
                var s = m * v[4 + i];
                var e = m * v[4 + ((i + 1) % 4)];
                Debug.DrawLine(s, e, color);
            }

            for (var i = 0; i < 4; i++)
            {
                var s = m * v[i];
                var e = m * v[i + 4];
                Debug.DrawLine(s, e, color);
            }
        }

        public static void DrawSphere(Vector4 pos, float radius, Color color, float duration = 0.0f)
        {
            var v = SUnitSphere;
            var len = SUnitSphere.Length / 3;
            for (var i = 0; i < len; i++)
            {
                var sX = pos + radius * v[0 * len + i];
                var eX = pos + radius * v[0 * len + (i + 1) % len];
                var sY = pos + radius * v[1 * len + i];
                var eY = pos + radius * v[1 * len + (i + 1) % len];
                var sZ = pos + radius * v[2 * len + i];
                var eZ = pos + radius * v[2 * len + (i + 1) % len];
                Debug.DrawLine(sX, eX, color, duration);
                Debug.DrawLine(sY, eY, color, duration);
                Debug.DrawLine(sZ, eZ, color, duration);
            }
        }

        public static void DrawPoint(Vector4 pos, float scale, Color color, float duration = 0.0f)
        {
            var sX = pos + new Vector4(+scale, 0, 0);
            var eX = pos + new Vector4(-scale, 0, 0);
            var sY = pos + new Vector4(0, +scale, 0);
            var eY = pos + new Vector4(0, -scale, 0);
            var sZ = pos + new Vector4(0, 0, +scale);
            var eZ = pos + new Vector4(0, 0, -scale);
            Debug.DrawLine(sX, eX, color, duration);
            Debug.DrawLine(sY, eY, color, duration);
            Debug.DrawLine(sZ, eZ, color, duration);
        }

        public static void DrawAxes(Vector4 pos, float scale = 1.0f, float duration = 0.0f)
        {
            Debug.DrawLine(pos, pos + new Vector4(scale, 0, 0), Color.red, duration);
            Debug.DrawLine(pos, pos + new Vector4(0, scale, 0), Color.green, duration);
            Debug.DrawLine(pos, pos + new Vector4(0, 0, scale), Color.blue, duration);
        }

        public static void DrawAxes(Matrix4x4 transform, float scale = 1.0f, float duration = 0.0f)
        {
            var p = transform * new Vector4(0, 0, 0, 1);
            var x = transform * new Vector4(scale, 0, 0, 1);
            var y = transform * new Vector4(0, scale, 0, 1);
            var z = transform * new Vector4(0, 0, scale, 1);

            Debug.DrawLine(p, x, Color.red, duration);
            Debug.DrawLine(p, y, Color.green, duration);
            Debug.DrawLine(p, z, Color.blue, duration);
        }

        public static void DrawQuad(Matrix4x4 transform, Color color)
        {
            var v = SUnitSquare;
            var m = transform;
            for (var i = 0; i < 4; i++)
            {
                var s = m * v[i];
                var e = m * v[(i + 1) % 4];
                Debug.DrawLine(s, e, color);
            }
        }

        public static void DrawPlane(Plane plane, float scale, Color edgeColor, float normalScale, Color normalColor, float duration = 0.0f)
        {
            // Flip plane distance: Unity Plane distance is from plane to origin
            DrawPlane(new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, -plane.distance), scale, edgeColor, normalScale, normalColor, duration);
        }

        public static void DrawPlane(Vector4 plane, float scale, Color edgeColor, float normalScale, Color normalColor, float duration = 0.0f)
        {
            var n = Vector3.Normalize(plane);
            var d = plane.w;

            var u = Vector3.up;
            var r = Vector3.right;
            if (n == u)
                u = r;

            r = Vector3.Cross(n, u);
            u = Vector3.Cross(n, r);

            for (var i = 0; i < 4; i++)
            {
                var s = scale * SUnitSquare[i];
                var e = scale * SUnitSquare[(i + 1) % 4];
                s = s.x * r + s.y * u + n * d;
                e = e.x * r + e.y * u + n * d;
                Debug.DrawLine(s, e, edgeColor, duration);
            }

            // Diagonals
            {
                var s = scale * SUnitSquare[0];
                var e = scale * SUnitSquare[2];
                s = s.x * r + s.y * u + n * d;
                e = e.x * r + e.y * u + n * d;
                Debug.DrawLine(s, e, edgeColor, duration);
            }
            {
                var s = scale * SUnitSquare[1];
                var e = scale * SUnitSquare[3];
                s = s.x * r + s.y * u + n * d;
                e = e.x * r + e.y * u + n * d;
                Debug.DrawLine(s, e, edgeColor, duration);
            }

            Debug.DrawLine(n * d, n * (d + 1 * normalScale), normalColor, duration);
        }
    }
}