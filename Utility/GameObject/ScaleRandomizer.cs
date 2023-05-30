
using NaughtyAttributes;
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class ScaleRandomizer : MonoBehaviour
    {
        [SerializeField] private bool randomizeAtStart = false;
        [SerializeField] private bool isScaleduniform = false;

        [ShowIf("isScaleduniform")][SerializeField] private float minScale = 1f;
        [ShowIf("isScaleduniform")][SerializeField] private float maxScale = 1f;

        [HideIf("isScaleduniform")][SerializeField] private Vector3 minScaleVector = Vector3.one;
        [HideIf("isScaleduniform")][SerializeField] private Vector3 maxScaleVector = Vector3.one;

        private void Start()
        {
            if (randomizeAtStart)
                RandomizeScale();
        }
        public void RandomizeScale()
        {
            if (isScaleduniform)
                RandomizeScaleUniform();
            else
                RandomizeScaleNonUniform();
        }

        public void RandomizeScaleUniform()
        {
            var scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public void RandomizeScaleNonUniform()
        {
            var scaleX = Random.Range(minScaleVector.x, maxScaleVector.x);
            var scaleY = Random.Range(minScaleVector.y, maxScaleVector.y);
            var scaleZ = Random.Range(minScaleVector.z, maxScaleVector.z);
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}