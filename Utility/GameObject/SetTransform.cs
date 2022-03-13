using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class SetTransform : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private bool setPosition;
        [SerializeField] private bool setRotation;
        [SerializeField] private bool setScale;

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