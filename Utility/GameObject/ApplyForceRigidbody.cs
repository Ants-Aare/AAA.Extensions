using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForceRigidbody : MonoBehaviour
{
    [SerializeField]
    private Vector3 force;

    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
            ApplyForceToRigidbody(rb);
    }
    public void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
            ApplyForceToRigidbody(rb);
    }

    public void ApplyForceToRigidbody(Rigidbody rigidbody)
    {
        rigidbody.AddForce(force);
    }
}