using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Float Variable")]
    public class FloatVariable : GlobalVariable<float>
    {
        public override void Save()
        {
            PlayerPrefs.SetFloat(name, value);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetFloat(name);
            }
            else
            {
                value = defaultValue;
            }

            isInitialized = true;
        }

        public bool ClampValue(float min, float max)
        {
            if (value > max)
            {
                value = max;
                OnChanged?.Invoke();
                return true;
            }
            else if (value < min)
            {
                value = min;
                OnChanged?.Invoke();
                return true;
            }
            return false;
        }
    }
}