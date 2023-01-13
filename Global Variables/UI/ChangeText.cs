using System;
using System.Globalization;
using AAA.Editor.Runtime.Attributes;
using NaughtyAttributes;
using UnityEngine;
using TMPro;


namespace AAA.GlobalVariables.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ChangeText : MonoBehaviour
    {
        [SerializeField] private int roundDecimals = 1;

        [BoxGroup("GroupPrefix")] [SerializeField]
        private bool usePrefix;

        [BoxGroup("GroupPrefix")] [SerializeField] [ShowIf("usePrefix")]
        private string prefix;

        [BoxGroup("GroupSuffix")] [SerializeField]
        private bool useSuffix;

        [BoxGroup("GroupSuffix")] [SerializeField] [ShowIf("useSuffix")]
        private string suffix;

        [Self] [SerializeField] private TextMeshProUGUI textField;

        public virtual void SetText(float value)
        {
            var valueString = Math.Round((decimal)value, roundDecimals).ToString(CultureInfo.InvariantCulture);
            SetText(valueString);
        }

        public virtual void SetText(string value)
        {
            if (usePrefix)
                value = prefix + value;
            if (useSuffix)
                value += suffix;

            textField.text = value;
        }
    }
}