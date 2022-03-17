using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class SetTransform : MonoBehaviour
    {
        [SerializeField] private bool setPosition;
        [SerializeField] private bool setRotation;
        [SerializeField] private bool setScale;
        [SerializeField] private Transform targetTransform;

        public void SetTransformToTarget()
        {
            SetTransformToTarget(targetTransform);
        }
        public void SetTransformToTarget(Transform newTransform)
        {
            if (setPosition)
                transform.position = newTransform.position;
            if (setRotation)
                transform.rotation = newTransform.rotation;
            if (setScale)
                transform.localScale = newTransform.localScale;
        }
    }
}