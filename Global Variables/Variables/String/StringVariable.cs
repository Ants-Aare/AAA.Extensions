using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    [CreateAssetMenu(menuName = "Variable/String Variable")]
    public class StringVariable : GlobalVariable<string, StringVariable>
    {
#if UNITTY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReload() => GenericDomainReload();
#endif
        protected override void SaveVariable() =>PlayerPrefs.SetString(name, value);
        protected override void LoadVariable()=> value = PlayerPrefs.GetString(name, defaultValue);
    }
}