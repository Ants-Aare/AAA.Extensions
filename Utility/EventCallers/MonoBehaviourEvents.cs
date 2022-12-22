using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


namespace AAA.Utility.EventCallers
{
    public class MonoBehaviourEvents : MonoBehaviour
    {
        [SerializeField] [BoxGroup("Start")] private UnityEvent OnAwake, OnEnabled, OnStart;
        [SerializeField] [BoxGroup("End")] private UnityEvent OnDestroyed, OnDisabled;

        private void Awake() => OnAwake.Invoke();
        private void Start() => OnStart.Invoke();
        private void OnEnable() => OnEnabled.Invoke();
        private void OnDisable() => OnDisabled.Invoke();
        private void OnDestroy() => OnDestroyed.Invoke();
    }
}