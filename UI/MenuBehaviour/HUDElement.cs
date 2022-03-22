using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace AAA.UI.MenuBehaviour
{
    public class HUDElement : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent onHUDEnabled = new UnityEvent();
        [SerializeField]
        private UnityEvent onHUDDisabled = new UnityEvent();
        private bool isEnabled = true;

        private void Start()
        {
            MenuManager.Instance?.RegisterHUDElement(this);
        }
        public void EnableHUD()
        {
            if(isEnabled)
                return;
            onHUDEnabled.Invoke();
            isEnabled = true;
        }
        public void DisableHUD()
        {
            if(!isEnabled)
                return;
            onHUDDisabled.Invoke();
            isEnabled = false;
        }
    }
}