using AAA.Extensions;
using UnityEngine;
using AAA.Utility.Math;

namespace AAA.Utility.GameObjectUtil
{
    public class RandomizeTransform : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPositionDelta, maxRotationDelta;
        [SerializeField] private Vector3 minScale, maxScale = Vector3.one;
        [Tooltip("This will use the x Value of minScale and maxScale for all scale values")]
        [SerializeField] private bool scaleUniform = true;
        [SerializeField] private Space space = Space.Self;

        public void RandomizeAll()
        {
            RandomizePosition();
            RandomizeRotation();
            RandomizeScale();
        }
        public void RandomizePosition()
        {
            var positionDelta = Vector3Extensions.RandomVector(maxPositionDelta * -1, maxPositionDelta);
            transform.Translate(positionDelta, space);
        }
        public void RandomizeRotation()
        {
            var rotationDelta = Vector3Extensions.RandomVector(maxRotationDelta * -1, maxRotationDelta);
            transform.Rotate(rotationDelta, space);
        }
        public void RandomizeScale()
        {
            var newScale = Vector3.one;
            if(scaleUniform)
                newScale *= Random.Range(minScale.x, maxScale.x);
            else
                newScale = Vector3Extensions.RandomVector(minScale, maxScale);
            transform.localScale = newScale;
        }
    }
}