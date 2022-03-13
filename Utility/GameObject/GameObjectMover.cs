using UnityEngine;

public class GameObjectMover : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    public void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }
}
