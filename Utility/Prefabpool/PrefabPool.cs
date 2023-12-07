using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;


namespace AAA.Utility
{
    [CreateAssetMenu(menuName = "Variable/Prefab Pool")]
    public class PrefabPool : ScriptableObject
    {
        [SerializeField] private PooledObject prefab;

        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxCapacity = 20;
        [SerializeField] private bool performCollectionChecks = false;

         private ObjectPool<PooledObject> _pool;
        [ShowNonSerializedField, ReadOnly] private bool _isInitialized;
        private readonly List<PooledObject> _instances = new List<PooledObject>();

        void OnEnable()
        {
            InitializePrefabPool();
        }

        void OnDisable()
        {
            _isInitialized = false;
        }

        public void InitializePrefabPool()
        {
            if (_isInitialized)
                return;

            // _pool = new ObjectPool<PooledObject>[prefabs.Length];
            _pool = new ObjectPool<PooledObject>(
                CreatePooledObject,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPooledObject,
                performCollectionChecks, defaultCapacity, maxCapacity
                );
        }

        #region Prefab Getters

        public PooledObject GetPrefab()
        {
            return prefab;
        }

        #endregion

        #region Instance Management

        public GameObject GetInstanceFromPool()
        {
            var pooledObject = _pool.Get();
            _instances.Add(pooledObject);
            return pooledObject.gameObject;
        }
        public void ReleaseInstance(PooledObject pooledObjectInstance)
        {
            _pool.Release(pooledObjectInstance);
        }

        public void ReleaseAllInstances()
        {
            foreach (var t in _instances)
            {
                _pool.Release(t);
            }
            _instances.Clear();
        }

        #endregion

        #region Pool Methods

        private PooledObject CreatePooledObject()
        {
            var pooledObjectPrefab = GetPrefab();
            var pooledObjectInstance = Instantiate(pooledObjectPrefab);
            pooledObjectInstance.Initialize(this);

            return pooledObjectInstance;
        }
        private void OnTakeFromPool(PooledObject pooledObject) => pooledObject.OnTakeFromPool();
        private void OnReturnedToPool(PooledObject pooledObject) => pooledObject.OnReturnedToPool();
        private void OnDestroyPooledObject(PooledObject pooledObject) => pooledObject.OnDestroyedPooledObject();

        #endregion
    }
}