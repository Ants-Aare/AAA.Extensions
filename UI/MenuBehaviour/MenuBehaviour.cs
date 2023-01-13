using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using AAA.Utility.Singleton;
using AAA.GlobalVariables.Variables;

namespace AAA.UI.MenuBehaviour
{
    public class MenuBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEvent onMenuEnabled = new UnityEvent();
        [SerializeField] private UnityEvent onMenuDisabled = new UnityEvent();

        [SerializeField] private BoolVariable isInMenu;
        [SerializeField] private GameObject firstSelected = null;

        internal virtual void OnMenuEnabled()
        {
            if (isInMenu != null)
                isInMenu.Value = true;
            Debug.Log("Enabled Menu " + gameObject.name);
            if (firstSelected != null)
                EventSystem.current?.SetSelectedGameObject(firstSelected);
            onMenuEnabled.Invoke();
        }

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