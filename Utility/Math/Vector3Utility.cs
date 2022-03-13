using UnityEngine;

namespace AAA.Utility
{
    public static class Vector3Utility
    {
        public static Vector3 RandomVector(Vector3 minVector, Vector3 maxVector)
        {
            return new Vector3(Random.Range(minVector.x, maxVector.x), Random.Range(minVector.y, maxVector.y), Random.Range(minVector.z, maxVector.z));
        }
    }
}