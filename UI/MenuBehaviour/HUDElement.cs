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

        private void Start()
        {
            MenuManager.Instance?.RegisterHUDElement(this);
        }
        public void EnableHUD()
        {
            onHUDEnabled.Invoke();
        }
        public void DisableHUD()
        {
            onHUDDisabled.Invoke();
        }
    }
}