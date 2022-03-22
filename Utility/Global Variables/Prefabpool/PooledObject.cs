using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class PooledObject : MonoBehaviour
    {
        [SerializeField] protected UnityEvent onTakeFromPool;
        [SerializeField] protected UnityEvent onReturnToPool;
        // [SerializeField] private UnityEvent onDestroyed;
        [ShowInInspector, ReadOnly] protected PrefabPool prefabPool;
        [ShowInInspector, ReadOnly] protected int index;

        internal void Initialize(PrefabPool prefabPool, int index)
        {
            this.index = index;
            this.prefabPool = prefabPool;
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
            // onDestroyed?.Invoke();
            Destroy(gameObject);
        }

        public void Release()
        {
            prefabPool.ReleaseInstance(this, index);
        }
    }
}