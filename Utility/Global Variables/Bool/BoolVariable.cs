using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Bool Variable")]
    public class BoolVariable : GlobalVariable<bool>
    {
        public override void Save()
        {
            PlayerPrefs.SetInt(name, value ? 1 : 0);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetInt(name) == 1;
            }
            else
            {
                value = defaultValue;
            }

            isInitialized = true;
        }
    }
}