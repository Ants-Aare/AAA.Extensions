using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


namespace AAA.Utility
{
    public class PooledObject : MonoBehaviour
    {
        [SerializeField] protected UnityEvent onTakeFromPool;
        [SerializeField] protected UnityEvent onReturnToPool;
        // [SerializeField] private UnityEvent onDestroyed;
        [ShowNonSerializedField, ReadOnly] protected PrefabPool PrefabPool;

        internal void Initialize(PrefabPool prefabPool)
        {
            this.PrefabPool = prefabPool;
        }
        internal void OnTakeFromPool()
        {
            gameObject.SetActive(true);
            onTakeFromPool?.Invoke();
        }
        internal void OnReturnedToPool()
        {
            gameObject.SetActive(false);
            onReturnToPool?.Invoke();
        }
        internal void OnDestroyedPooledObject()
        {
            Destroy(gameObject);
        }

        public void Release()
        {
            PrefabPool.ReleaseInstance(this);
        }
    }
}