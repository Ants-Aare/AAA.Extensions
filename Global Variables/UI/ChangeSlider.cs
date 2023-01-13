using UnityEngine;
using UnityEngine.UI;
using AAA.DataTypes;
using NaughtyAttributes;

namespace AAA.GlobalVariables.UI
{
    public class ChangeSlider : MonoBehaviour
    {
        [SerializeField] protected bool useImage;

        [SerializeField] [ShowIf("useImage")] protected Image image;
        [SerializeField] [HideIf("useImage")] protected Slider slider;

        public virtual void SetSliderProgress(RangeValue newValue)
        {
            if (useImage)
                image.fillAmount = newValue.GetProgress();
            else
                slider.value = newValue.GetProgress();
        }
    }
}