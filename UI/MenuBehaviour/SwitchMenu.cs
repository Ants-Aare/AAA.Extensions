using UnityEngine;
using UnityEngine.SceneManagement;

namespace AAA.UI.MenuBehaviour
{
    public class SwitchMenu : MonoBehaviour
    {
        [SerializeField] private GameObject targetMenu;
        [SerializeField] private bool disableHUD = true;
        public void EnableMenu(GameObject targetMenu)
        {
            if (targetMenu == null)
            {
                Debug.LogError("Please assign a valid menu");
                return;
            }
            MenuManager.Instance.EnableMenu(targetMenu, disableHUD);
        }
        public void EnableMenu()
        {
            EnableMenu(targetMenu);
        }
        public void DisableMenu()
        {
            MenuManager.Instance.DisableMenu();
        }

        public void MenuBack()
        {
            MenuManager.Instance.MenuBack();
        }
    }
}