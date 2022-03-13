using Sirenix.OdinInspector;
using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    [SerializeField] private bool randomizeAtStart = false;
    [SerializeField] private bool isScaleduniform = false;

    [ShowIf("isScaleduniform")] [SerializeField] private float minScale = 1f;
    [ShowIf("isScaleduniform")] [SerializeField] private float maxScale = 1f;

    [HideIf("isScaleduniform")] [SerializeField] private Vector3 minScaleVector = Vector3.one;
    [HideIf("isScaleduniform")] [SerializeField] private Vector3 maxScaleVector = Vector3.one;

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

    private void RandomizeScaleUniform()
    {
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void RandomizeScaleNonUniform()
    {
        float scaleX = Random.Range(minScaleVector.x, maxScaleVector.x);
        float scaleY = Random.Range(minScaleVector.y, maxScaleVector.y);
        float scaleZ = Random.Range(minScaleVector.z, maxScaleVector.z);
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
