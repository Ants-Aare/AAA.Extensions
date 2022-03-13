using System;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace AAA.UI
{
    public class ChangeText : MonoBehaviour
    {
        [TabGroup("Properties")][SerializeField] private int roundDecimals = 1;
        [HorizontalGroup("Properties/GroupPrefix")][SerializeField] private bool usePrefix;
        [HorizontalGroup("Properties/GroupPrefix")][SerializeField][LabelWidth(50f)][ShowIf("usePrefix")] private string prefix;
        [HorizontalGroup("Properties/GroupSuffix")][SerializeField] private bool useSuffix;
        [HorizontalGroup("Properties/GroupSuffix")][SerializeField][LabelWidth(50f)][ShowIf("useSuffix")] private string suffix;

        [TabGroup("References")][SerializeField] private TextMeshProUGUI textField;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if(textField == null)
                textField = GetComponent<TextMeshProUGUI>();
        }
#endif

        public virtual void SetText(float value)
        {
            string valueString = Math.Round((decimal)value, roundDecimals).ToString();
            SetText(valueString);
        }

        public virtual void SetText(string value)
        {
            if (textField == null)
            {
                Debug.LogError("Please assign a textmeshpro", gameObject);
                return;
            }

            if (usePrefix)
                value = prefix + value;
            if (useSuffix)
                value = value + suffix;

            textField.text = value;
        }
    }
}