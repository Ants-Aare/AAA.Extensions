using UnityEngine;
using UnityEngine.SceneManagement;

namespace AAA.UI.MenuBehaviour
{
    public class SwitchMenu : MonoBehaviour
    {
        public GameObject targetMenu;

        public void EnableMenu(GameObject targetMenu)
        {
            if (targetMenu == null)
            {
                Debug.LogError("Please assign a valid menu");
                return;
            }
            MenuManager.Instance.SetMenuToLoad(targetMenu);
            MenuManager.Instance.EnableMenu();
        }
        public void SwitchToMenu(GameObject targetMenu)
        {
            if (targetMenu == null)
            {
                Debug.LogError("Please assign a valid menu");
                return;
            }

            if (MenuManager.Instance.isMenuEnabled)
                MenuManager.Instance.SwitchToMenu(targetMenu);
            else
                MenuManager.Instance.menuToLoad = targetMenu;
        }

        public void SwitchToMenu()
        {
            SwitchToMenu(targetMenu);
        }
        public void EnableMenu()
        {
            MenuManager.Instance.EnableMenu();
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