
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyAddForce : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        [SerializeField] private float multiplier;
        [SerializeField] private Space space;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private Rigidbody rBody;

        #if UNITY_EDITOR
        void OnValidate()
        {
            if(rBody == null)
                rBody = GetComponent<Rigidbody>();
        }
        #endif

        public void AddForce()
        {
            var forceAmount = direction * multiplier;
            if(space == Space.Self)
                forceAmount = transform.TransformDirection(forceAmount);
            rBody.AddForce(forceAmount, forceMode);
        }
        public void RemoveForce()
        {
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
        }
    }
}