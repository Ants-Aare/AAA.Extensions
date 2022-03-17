using UnityEngine;
using AAA.Utility.GlobalVariables;

namespace AAA.Utility.GameObjectUtil
{
    public class PrefabInstantiator : MonoBehaviour
    {
        [SerializeField] private PrefabPool prefabPool;
        [SerializeField] private bool instantiateAtStart = false;

        public void Start()
        {
            if (instantiateAtStart)
                InstantiateAllPrefabs();
        }

        public void InstantiateAllPrefabs()
        {
            foreach (GameObject prefab in prefabPool.GetAllPrefabs())
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }
        }
        public void InstantiateRandomPrefab()
        {
            Instantiate(prefabPool.GetRandomPrefabFromPool(), Vector3.zero, Quaternion.identity);
        }
        public void InstantiateSpecificPrefab(int index)
        {
            Instantiate(prefabPool.GetPrefab(index), Vector3.zero, Quaternion.identity);
        }
        public void InstantiatePrefab(GameObject prefab)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}