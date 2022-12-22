using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.Singleton;

namespace AAA.UI.MenuBehaviour
{
    public class MenuManager : GenericSingleton<MenuManager>
    {
        public bool isMenuEnabled = false;

        [SerializeField] private bool enableMenuOnStart = false;
        [SerializeField] private bool instantiateAsChild = false;
        [SerializeField] private GameObject startMenu = null;

        [SerializeField] private UnityEvent onMenuEnabled, onMenuDisabled = new UnityEvent();

        private MenuBehaviour currentMenu = null;
        private GameObject currentMenuPrefab = null;
        private GameObject previousMenuPrefab = null;

        private List<HUDElement> hudElements = new List<HUDElement>();

        private void Start()
        {
            if(enableMenuOnStart)
                EnableMenu(startMenu);
        }

        public void EnableMenu(GameObject menuPrefab, bool disableHUD = true)
        {
            if(!isMenuEnabled)
            {
                if(disableHUD)
                {
                    DisableHUD();
                }
                onMenuEnabled.Invoke();
                isMenuEnabled = true;
            }

            SwitchToMenu(menuPrefab);
        }

        public void DisableMenu()
        {
            if(!isMenuEnabled)
            {
                Debug.Log("The Menu is already disabled, no need to disable it again.");
                return;
            }

            onMenuDisabled.Invoke();

            previousMenuPrefab = null;
            currentMenuPrefab = null;

            currentMenu?.OnMenuDisabled();
                
            isMenuEnabled = false;

            EnableHUD();
        }

        private void SwitchToMenu(GameObject targetMenuPrefab)
        {
            if(targetMenuPrefab == currentMenuPrefab)
            {
                Debug.LogError("You are trying to switch to the same menu twice");
                return;
            }

            previousMenuPrefab = currentMenuPrefab;
            currentMenuPrefab = targetMenuPrefab;

            if(currentMenu != null)
                currentMenu.OnMenuDisabled();

            if(instantiateAsChild)
                currentMenu = Instantiate(targetMenuPrefab, transform).GetComponent<MenuBehaviour>();
            else
                currentMenu = Instantiate(targetMenuPrefab).GetComponent<MenuBehaviour>();

            if(currentMenu != null)
                currentMenu.OnMenuEnabled();
        }

        public void MenuBack()
        {
            if(previousMenuPrefab == null)
            {
                DisableMenu();
                return;
            }
            SwitchToMenu(previousMenuPrefab);
            previousMenuPrefab = null;
        }

        public void RegisterHUDElement(HUDElement hudElement)
        {
            if(hudElement != null)
                hudElements.Add(hudElement);
        }

        public void EnableHUD()
        {
            foreach (var hudElement in hudElements)
            {
                hudElement.EnableHUD();
            }
        }

        public void DisableHUD()
        {
            foreach (var hudElement in hudElements)
            {
                hudElement.DisableHUD();
            }
        }
    }
}