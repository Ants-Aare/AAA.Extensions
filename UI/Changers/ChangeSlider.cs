using UnityEngine;
using UnityEngine.UI;
using AAA.Utility.DataTypes;
using NaughtyAttributes;

namespace AAA.UI
{
    public class ChangeSlider : MonoBehaviour
    {
        [SerializeField] protected bool useImage;

        [SerializeField] [ShowIf("useImage")] protected Image image = null;
        [SerializeField] [HideIf("useImage")] protected Slider slider = null;

        public virtual void SetSliderProgress(RangeValue newValue)
        {
            if (useImage)
                image.fillAmount = newValue.GetProgress();
            else
                slider.value = newValue.GetProgress();
        }
    }
}