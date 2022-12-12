using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Integer Variable")]
    public class IntVariable : GlobalVariable<int>
    {
        public override void Save()
        {
            PlayerPrefs.SetInt(name, value);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetInt(name);
            }
            else
            {
                value = defaultValue;
            }

            isInitialized = true;
        }
        public static IntVariable operator -(IntVariable a, int b)
        {
            a.Value = a.value - b;
            return a;
        }
        public static IntVariable operator +(IntVariable a, int b)
        {
            a.Value = a.value + b;
            return a;
        }
        public static IntVariable operator ++(IntVariable a)
        {
            a.Value++;
            return a;
        }
        public static IntVariable operator --(IntVariable a)
        {
            a.Value--;
            return a;
        }
    }
}