using System;
using UnityEngine;
using UnityEngine.UI;
using AAA.DataTypes;
using AAA.Editor.Runtime.Attributes;
using NaughtyAttributes;
using UnityEditor;

namespace AAA.GlobalVariables.UI
{
    public class ChangeSlider : MonoBehaviour
    {
        [SerializeField] protected bool useImage;

        [Self][SerializeField] [ShowIf("useImage")] protected Image image;
        [Self][SerializeField] [HideIf("useImage")] protected Slider slider;

        public virtual void SetSliderProgress(RangeValue newValue)
        {
            if (useImage)
                image.fillAmount = newValue.GetProgress();
            else
                slider.value = newValue.GetProgress();
        }
    }
}