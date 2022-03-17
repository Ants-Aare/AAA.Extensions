using System;
using UnityEngine;

namespace AAA.Utility.Physics
{
    public class RigidBodyController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody[] rigidbodies;

        public void SetRigidBodiesActive(bool isActive)
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = isActive;
            }
        }
    }
}
