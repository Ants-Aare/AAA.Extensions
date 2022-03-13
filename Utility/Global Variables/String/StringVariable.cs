using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/String Variable")]
    public class StringVariable : GlobalVariable<string>
    {
        public override void Save()
        {
            PlayerPrefs.SetString(name, value);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetString(name);
            }
            else
            {
                value = defaultValue;
            }

            isInitialized = true;
        }
    }
}