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
        [TabGroup("Events")][SerializeField] private UnityEvent onMenuEnabled = new UnityEvent();
        [TabGroup("Events")][SerializeField] private UnityEvent onMenuDisabled = new UnityEvent();

        [SerializeField] private BoolVariable isInMenu;
        [SerializeField] private GameObject firstSelected = null;

        [Button("Enable Menu")][TabGroup("Buttons")][HideInEditorMode]
        internal virtual void OnMenuEnabled()
        {
            if (isInMenu != null)
                isInMenu.Value = true;
            Debug.Log("Enabled Menu " + gameObject.name);
            if (firstSelected != null)
                EventSystem.current?.SetSelectedGameObject(firstSelected);
            onMenuEnabled.Invoke();
        }
        [Button("Disable Menu")][TabGroup("Buttons")][HideInEditorMode]
        internal virtual void OnMenuDisabled()
        {
            if (isInMenu != null)
                isInMenu.Value = false;
            onMenuDisabled.Invoke();
        }

        public void DisableMenu()
        {
            MenuManager.Instance.DisableMenu();
        }
    }
}