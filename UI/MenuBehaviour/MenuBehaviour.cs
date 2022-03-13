using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using AAA.Utility.Singleton;
using AAA.Utility.GlobalVariables;

namespace AAA.UI.MenuBehaviour
{
    public class MenuBehaviour : MonoBehaviour
    {
        [SerializeField] private BoolVariable isInMenu;

        [TabGroup("Events")][SerializeField] private UnityEvent onMenuEnabled = new UnityEvent();
        [TabGroup("Events")][SerializeField] private UnityEvent onMenuDisabled = new UnityEvent();

        [SerializeField] private GameObject firstSelected = null;

        [Button]
        [TabGroup("Buttons")]
        [HideInEditorMode]
        public virtual void EnableMenu()
        {
            if (isInMenu != null)
                isInMenu.Value = true;
            Debug.Log("Enabled Menu " + gameObject.name);
            if (firstSelected != null)
                EventSystem.current?.SetSelectedGameObject(firstSelected);
            onMenuEnabled.Invoke();
        }
        [Button]
        [TabGroup("Buttons")]
        [HideInEditorMode]
        public virtual void DisableMenu()
        {
            if (isInMenu != null)
                isInMenu.Value = false;
            onMenuDisabled.Invoke();
        }
    }
}