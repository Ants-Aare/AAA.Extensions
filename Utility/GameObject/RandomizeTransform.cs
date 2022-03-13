using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class RandomizeTransform : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPositionDelta, maxRotationDelta;
        [SerializeField] private Vector3 minScale, maxScale = Vector3.one;
        [Tooltip("This will use the x Value of minScale and maxScale for y and z Values as well")]
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
            Vector3 positionDelta = Vector3Utility.RandomVector(maxPositionDelta * -1, maxPositionDelta);
            transform.Translate(positionDelta, space);
        }
        public void RandomizeRotation()
        {
            Vector3 rotationDelta = Vector3Utility.RandomVector(maxRotationDelta * -1, maxRotationDelta);
            transform.Rotate(rotationDelta, space);
        }
        public void RandomizeScale()
        {
            Vector3 newScale = Vector3.one;
            if(scaleUniform)
                newScale *= Random.Range(minScale.x, maxScale.x);
            else
                newScale = Vector3Utility.RandomVector(minScale, maxScale);
            transform.localScale = newScale;
        }
    }
}