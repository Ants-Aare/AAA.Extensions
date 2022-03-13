using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MakeRectSizeUniform : MonoBehaviour
{
    [SerializeField] private bool sampleWidth = true;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = transform as RectTransform;
    }
    private void OnTransformParentChanged()
    {
        if(sampleWidth)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, rectTransform.rect.width);
        }
        else
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.height, rectTransform.rect.height);
    }
}