using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Bool Variable")]
    public class BoolVariable : GlobalVariable<bool>
    {
        protected override void SaveVariable() => PlayerPrefs.SetInt(name, value ? 1 : 0);
        protected override void LoadVariable() => value = PlayerPrefs.GetInt(name) == 1;
        
        
    }
}