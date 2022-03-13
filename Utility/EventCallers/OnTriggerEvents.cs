using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvents : MonoBehaviour
{
    [SerializeField]
    private ColliderUnityEvent OnTriggerEntered = null;
    [SerializeField]
    private ColliderUnityEvent OnTriggerExited = null;

    private void OnTriggerEnter(Collider collider)
    {
        OnTriggerEntered.Invoke(collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        OnTriggerExited.Invoke(collider);
    }
}


[System.Serializable]
public class ColliderUnityEvent : UnityEvent<Collider>{}