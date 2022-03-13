using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Prefab Pool")]
    public class Prefabpool : ScriptableObject
    {
        [SerializeField] private GameObject[] prefabs;

        public GameObject GetRandomPrefabFromPool()
        {
            if (prefabs.Length != 0)
                return prefabs[Random.Range(0, prefabs.Length)];
            return null;
        }
    }
}