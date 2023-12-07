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
                InstantiatePrefab();
        }
        public void InstantiatePrefab()
        {
            var go = prefabPool.GetInstanceFromPool();
            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
        }
        public void InstantiatePrefab(GameObject prefab)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}