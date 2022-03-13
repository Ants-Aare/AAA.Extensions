using System;
using UnityEngine;

namespace Contaquest.Metaverse.Behaviours
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
