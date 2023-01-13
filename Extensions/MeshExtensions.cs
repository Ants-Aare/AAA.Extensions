using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AAA.Extensions
{
    public static class MeshExtensions
    {
        public static Vector3 GetPivot(this Mesh mesh)
        {
            return mesh.bounds.GetPivot();
        }

        /// <summary>
        /// Similar to a Sprite PixelPerUnit but for meshes. A mesh with a factor of 300 and a width of 100
        /// will end up with 3 in unity units
        /// </summary>
        /// <param name="mesh">The mesh to apply this to</param>
        /// <param name="meshUnitPerUnit">The factor to apply this with</param>
        public static void ApplyVertexUnitConversion(this Mesh mesh, float meshUnitPerUnit)
        {
            var vertices = mesh.vertices;

            foreach (var i in ..vertices.Length)
            {
                vertices[i] /= meshUnitPerUnit;
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
        }

        public static Vector3 RepositionMeshToCenterOfBounds(this Mesh mesh)
        {
            var center = mesh.bounds.center;
            var vertices = mesh.vertices;

            foreach (var i in ..vertices.Length)
            {
                vertices[i] -= center;
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            return center;
        }

        /// <summary>
        /// From a mesh that has all vertices on the same plane,
        /// generates the UVs for each vertex taking into account the total size and minPosition
        /// so for example an element of size (2,2) positioned at (0,1) positioned and minPos of (-1,-1) would have
        /// uvs of (0.5, 1)
        /// </summary>
        /// <param name="mesh">the mesh to calculate on</param>
        /// <param name="minPosition">the minimum position of a vertex to get the UV for</param>
        /// <param name="size">the total size of the element</param>
        /// <param name="vertexToPlanePositionFunc">Func to turn the vector3 into a vector2 on the plane's coordinate system</param>
        public static void GenerateUvsOfPlaneVerticesBySize(
            this Mesh mesh,
            Vector2 minPosition,
            Vector2 size,
            Func<Vector3, Vector2> vertexToPlanePositionFunc)
        {
            var vertices = mesh.vertices;
            var uvs = new Vector2[vertices.Length];

            for (int i = 0; i < uvs.Length; i++)
            {
                var planePosition = vertexToPlanePositionFunc(vertices[i]) - minPosition;
                uvs[i] = planePosition / size;
            }

            mesh.uv = uvs;
        }

        public static bool TryGenerateMeshPlaneContourUsingOffset(this Mesh mesh, Vector3 offset, out Mesh contourMesh)
        {
            List<List<Vector3>> allContourVertices = mesh.GetContourVerticesOfMesh();

            bool found = allContourVertices.TryGet(0, out List<Vector3> contourVertices);

            if (!found)
            {
                contourMesh = default;
                return false;
            }

            if (contourVertices.Count < 2)
            {
                contourMesh = default;
                return false;
            }

            //Generate vertices
            Vector3[] vertices = new Vector3[contourVertices.Count * 2];
            foreach (int i in ..contourVertices.Count)
            {
                Vector3 position = contourVertices[i];
                vertices[i] = position;
                vertices[i + contourVertices.Count] = position + offset;
            }

            //Generate UVs
            Vector2[] uvs = new Vector2[vertices.Length];
            foreach (var i in ..contourVertices.Count)
            {
                bool pair = i % 2 == 0;
                uvs[i] = new Vector2(pair ? 0 : 1, 0);
                uvs[i + contourVertices.Count] = new Vector2(pair ? 0 : 1, 1);
            }

            //Generate triangles
            // Multiplied by 3 becase for each vertex we have a triangle
            int[] triangles = new int[vertices.Length * 3];
            int vertexCount = contourVertices.Count;

            //Do all vertices minus the last one which we do explicitly again because it loops to the first one
            foreach (int i in ..(vertexCount - 1))
            {
                int startIndex = i * 6;

                triangles[startIndex] = i;
                triangles[startIndex + 1] = i + 1 + vertexCount;
                triangles[startIndex + 2] = i + vertexCount;

                triangles[startIndex + 3] = i + 1;
                triangles[startIndex + 4] = i + 1 + vertexCount;
                triangles[startIndex + 5] = i;
            }

            //Explicit last vertex triangles
            int lastIndexStart = (vertexCount - 1) * 6;
            triangles[lastIndexStart] = vertexCount - 1;
            triangles[lastIndexStart + 1] = vertexCount;
            triangles[lastIndexStart + 2] = vertexCount + vertexCount - 1;

            triangles[lastIndexStart + 3] = 0;
            triangles[lastIndexStart + 4] = vertexCount;
            triangles[lastIndexStart + 5] = vertexCount - 1;

            contourMesh = new Mesh
            {
                vertices = vertices,
                triangles = triangles,
                uv = uvs
            };
            contourMesh.RecalculateBounds();
            contourMesh.RecalculateNormals();
            contourMesh.RecalculateTangents();
            contourMesh.Optimize();

            return true;
        }

        public static void SetPathAsRect(this PolygonCollider2D polygonCollider2D, Rect rect)
        {
            polygonCollider2D.pathCount = 1;
            polygonCollider2D.SetPath(0, new[]
            {
                rect.min,
                rect.min + new Vector2(rect.width, 0),
                rect.max,
                rect.max - new Vector2(rect.width, 0)
            });
        }

        /// <seealso cref="https://www.h3xed.com/programming/automatically-create-polygon-collider-2d-from-2d-mesh-in-unity"/>
        public static List<List<Vector3>> GetContourVerticesOfMesh(this Mesh mesh)
        {
            List<List<Vector3>> ret = new List<List<Vector3>>();

            // Get triangles and vertices from mesh
            int[] triangles = mesh.triangles;
            Vector3[] vertices = mesh.vertices;

            // Get just the outer edges from the mesh's triangles (ignore or remove any shared edges)
            Dictionary<string, KeyValuePair<int, int>> edges = new Dictionary<string, KeyValuePair<int, int>>();
            for (int i = 0; i < triangles.Length; i += 3)
            {
                for (int e = 0; e < 3; e++)
                {
                    int vert1 = triangles[i + e];
                    int index2 = i + e + 1 > i + 2
                        ? i
                        : i + e + 1;
                    int vert2 = triangles[index2];
                    string edge = Mathf.Min(vert1, vert2) + ":" + Mathf.Max(vert1, vert2);
                    if (edges.ContainsKey(edge))
                    {
                        edges.Remove(edge);
                    }
                    else
                    {
                        edges.Add(edge, new KeyValuePair<int, int>(vert1, vert2));
                    }
                }
            }

            // Create edge lookup (Key is first vertex, Value is second vertex, of each edge)
            Dictionary<int, int> lookup = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> edge in edges.Values)
            {
                if (lookup.ContainsKey(edge.Key) == false)
                {
                    lookup.Add(edge.Key, edge.Value);
                }
            }

            // Loop through edge vertices in order
            int startVert = 0;
            int nextVert = 0;
            HashSet<int> visited = new HashSet<int>();
            while (visited.Count < lookup.Count)
            {
                List<Vector3> contourPositions = new List<Vector3>();

                do
                {
                    if (!visited.Add(nextVert))
                    {
                        throw new InvalidOperationException(
                            "Loop found while creating contour");
                    }

                    contourPositions.Add(vertices[nextVert]);
                    nextVert = lookup[nextVert];
                } while (nextVert != startVert);

                ret.Add(contourPositions);

                bool found = false;
                foreach (int key in lookup.Keys)
                {
                    if (visited.Contains(key))
                    {
                        continue;
                    }

                    startVert = key;
                    found = true;
                    break;
                }

                if (!found)
                {
                    break;
                }

                nextVert = startVert;
            }

            return ret;
        }
    }
}
