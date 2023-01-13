using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AAA.Extensions
{
    public static class PolygonCollider2dExtensions
    {
        public static void SetPathAsRect(this PolygonCollider2D polygonCollider2D, Rect rect)
        {
            polygonCollider2D.pathCount = 1;
            polygonCollider2D.SetPath(0, new []
            {
                rect.min,
                rect.min + new Vector2(rect.width, 0),
                rect.max,
                rect.max - new Vector2(rect.width, 0)
            });
        }

        // /// <seealso cref="https://www.h3xed.com/programming/automatically-create-polygon-collider-2d-from-2d-mesh-in-unity"/>
        public static void SetMeshAsPaths(this PolygonCollider2D polygonCollider2D, Mesh mesh, Func<Vector3, Vector2> meshToPlaneFunc)
        {
            polygonCollider2D.pathCount = 0;

            foreach (List<Vector3> contour in mesh.GetContourVerticesOfMesh())
            {
                polygonCollider2D.pathCount++;
                Vector2[] contour2d = contour.Select(meshToPlaneFunc).ToArray();
                polygonCollider2D.SetPath(polygonCollider2D.pathCount - 1, contour2d);
            }
        }
    }
}
