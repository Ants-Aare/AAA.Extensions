using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEvents : MonoBehaviour
{
    [SerializeField]
    private CollisionUnityEvent OnCollisionEntered = null;
    [SerializeField]
    private CollisionUnityEvent OnCollisionExited = null;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEntered.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExited.Invoke(collision);
    }
}


[System.Serializable]
public class CollisionUnityEvent : UnityEvent<Collision>{}