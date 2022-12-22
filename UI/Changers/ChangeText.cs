using System;
using NaughtyAttributes;
using UnityEngine;
using TMPro;


namespace AAA.UI
{
    public class ChangeText : MonoBehaviour
    {
        [SerializeField] private int roundDecimals = 1;
        [BoxGroup("GroupPrefix")][SerializeField] private bool usePrefix;
        [BoxGroup("GroupPrefix")][SerializeField][ShowIf("usePrefix")] private string prefix;
        [BoxGroup("GroupSuffix")][SerializeField] private bool useSuffix;
        [BoxGroup("GroupSuffix")][SerializeField][ShowIf("useSuffix")] private string suffix;

        [SerializeField] private TextMeshProUGUI textField;

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