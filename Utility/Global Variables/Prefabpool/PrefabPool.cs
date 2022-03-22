using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Prefab Pool")]
    public class PrefabPool : ScriptableObject
    {
        [SerializeField] private PooledObject[] prefabs;

        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxCapacity = 20;
        [SerializeField] private bool performCollectionChecks = false;

        [ShowInInspector, ReadOnly] private ObjectPool<PooledObject>[] pools;
        [ShowInInspector, ReadOnly] private bool isInitialized;

        void OnEnable()
        {
            InitializePrefabPool();
        }
        void OnDisable()
        {
            isInitialized = false;
        }

        public void InitializePrefabPool()
        {
            if(isInitialized)
                return;

            pools = new ObjectPool<PooledObject>[prefabs.Length];
            for (int i = 0; i < prefabs.Length; i++)
            {
                int t = i;
                pools[i] = new ObjectPool<PooledObject>(
                    ()=> CreatePooledObject(t),
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPooledObject,
                    performCollectionChecks, defaultCapacity, maxCapacity);
            }
        }

        #region Prefab Getters
        public PooledObject[] GetAllPrefabs()
        {
            return prefabs.Select((prefab)=>prefab).ToArray();
        }
        public PooledObject GetRandomPrefab()
        {
            if (prefabs.Length != 0)
                return prefabs[Random.Range(0, prefabs.Length)];
            return null;
        }
        public PooledObject GetPrefab(int index)
        {
            index = Mathf.Clamp(index, 0, pools.Length - 1);
            return prefabs[0];
        }
        #endregion

        #region Instance Management
        public GameObject GetRandomInstanceFromPool()
        {
            if (pools.Length == 0)
                return null;

            int index = Random.Range(0, pools.Length);
            return pools[index].Get().gameObject;
        }
        public GameObject GetInstance(int index)
        {
            index = Mathf.Clamp(index, 0, pools.Length - 1);
            return pools[index].Get().gameObject;
        }
        public void ReleaseInstance(PooledObject pooledObjectInstance, int index)
        {
            pools[index].Release(pooledObjectInstance);
        }
        #endregion

        #region Pool Methods
        private PooledObject CreatePooledObject(int index)
        {
            PooledObject pooledObjectPrefab = GetPrefab(index);
            PooledObject pooledObjectInstance = Instantiate(pooledObjectPrefab);
            pooledObjectInstance.Initialize(this, index);

            return pooledObjectInstance;
        }
        private void OnTakeFromPool(PooledObject pooledObject)=>pooledObject.OnTakeFromPool();
        private void OnReturnedToPool(PooledObject pooledObject)=>pooledObject.OnReturnedToPool();
        private void OnDestroyPooledObject(PooledObject pooledObject)=>pooledObject.OnDestroyedPooledObject();
        #endregion
    }
}