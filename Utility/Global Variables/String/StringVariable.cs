using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/String Variable")]
    public class StringVariable : GlobalVariable<string>
    {
        protected override void SaveVariable() =>PlayerPrefs.SetString(name, value);
        protected override void LoadVariable()=> value = PlayerPrefs.GetString(name, defaultValue);
    }
}