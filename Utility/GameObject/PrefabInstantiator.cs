using UnityEngine;

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
            prefabPool.GetAllInstances();
        }
        public void InstantiateRandomPrefab()
        {
            GameObject go = prefabPool.GetRandomInstanceFromPool();
            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;

        }
        public void InstantiateSpecificPrefab(int index)
        {
            GameObject go = prefabPool.GetInstanceFromPool(index);
            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
        }
        public void InstantiatePrefab(GameObject prefab)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}