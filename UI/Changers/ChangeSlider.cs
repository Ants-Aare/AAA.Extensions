using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using AAA.Utility.DataTypes;

namespace AAA.UI
{
    public class ChangeSlider : MonoBehaviour
    {
        [TabGroup("Properties")][SerializeField] protected bool useImage;

        [TabGroup("References")][SerializeField] [ShowIf("useImage")] protected Image image = null;
        [TabGroup("References")][SerializeField] [HideIf("useImage")] protected Slider slider = null;

        public virtual void SetSliderProgress(FloatRangeValue newValue)
        {
            if (useImage)
                image.fillAmount = newValue.GetProgress();
            else
                slider.value = newValue.GetProgress();
        }
    }
}