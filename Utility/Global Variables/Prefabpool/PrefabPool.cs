using Sirenix.OdinInspector;
using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Prefab Pool")]
    public class PrefabPool : ScriptableObject
    {
        [SerializeField] private GameObject[] prefabs;

        public GameObject[] GetAllPrefabs()
        {
            return prefabs;
        }
        public GameObject GetRandomPrefabFromPool()
        {
            if (prefabs.Length != 0)
                return prefabs[Random.Range(0, prefabs.Length)];
            return null;
        }
        public GameObject GetPrefab(int index)
        {
            if(prefabs.Length > 1)
            {
                if(index >= prefabs.Length)
                    return prefabs[prefabs.Length - 1];
                if(index < 0)
                    return prefabs[0];
                return prefabs[index];
            }
            return prefabs[0];
        }
    }
}