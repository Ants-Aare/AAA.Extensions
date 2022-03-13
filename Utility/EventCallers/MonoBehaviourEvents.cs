using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AAA.Utility
{
    public class MonoBehaviourEvents : MonoBehaviour
    {
        [SerializeField] [HorizontalGroup("Split", 0.5f)][BoxGroup("Split/Start")]private UnityEvent OnAwake, OnEnabled, OnStart;
        [SerializeField][BoxGroup("Split/End")] private UnityEvent OnDestroyed, OnDisabled;

        private void Awake() => OnAwake.Invoke();
        private void Start() => OnStart.Invoke();
        private void OnEnable() => OnEnabled.Invoke();
        private void OnDisable() => OnDisabled.Invoke();
        private void OnDestroy() => OnDestroyed.Invoke();
    }
}