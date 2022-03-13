using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using AAA.Utility.Singleton;

namespace AAA.UI.MenuBehaviour
{
    public class MenuManager : GenericSingleton<MenuManager>
    {
        public bool isMenuEnabled = false;

        [SerializeField]
        private bool enableMenuOnStart = false;

        [Header("Events")]
        [SerializeField][FormerlySerializedAs("onMenuLoad")]
        private UnityEvent onMenuEnabled = new UnityEvent();
        [SerializeField][FormerlySerializedAs("onMenuUnload")]
        private UnityEvent onMenuDisabled = new UnityEvent();

        [Header("Menus")]
        [SerializeField]
        private GameObject startMenu = null;
        [SerializeField]
        private MenuBehaviour currentMenu = null;
        private GameObject currentMenuPrefab = null;
        [SerializeField]
        private GameObject previousMenuPrefab = null;
        public GameObject menuToLoad = null;

        [HideInInspector]
        public List<HUDElement> hudElements = new List<HUDElement>();

        private void Start()
        {
            if(enableMenuOnStart)
                Invoke("EnableMenu", 0.05f);
        }

        public void EnableMenu()
        {
            if(menuToLoad == null)
                EnableMenu(startMenu);
            else
            {
                EnableMenu(menuToLoad);
                menuToLoad = null;
            }
        }
        public void SetMenuToLoad(GameObject menuPrefab)
        {
            menuToLoad = menuPrefab;
        }
        public void EnableMenu(GameObject menuPrefab)
        {
            if(isMenuEnabled)
            {
                // Debug.Log("The Menu is already opened, no need to open it again.");
                return;
            }

            onMenuEnabled.Invoke();

            isMenuEnabled = true;
            SwitchToMenu(menuPrefab);

            foreach (var hudElement in hudElements)
            {
                hudElement.DisableHUD();
            }
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

            if(currentMenu != null)
                currentMenu.DisableMenu();
                
            menuToLoad = null;
            isMenuEnabled = false;

            foreach (var hudElement in hudElements)
            {
                hudElement.EnableHUD();
            }
        }

        public void SwitchToMenu(GameObject targetMenuPrefab)
        {
            if(!isMenuEnabled)
            {
                Debug.LogError("Please enable the menu before instantiating.");
                return;
            }
            if(targetMenuPrefab == currentMenuPrefab)
            {
                Debug.LogError("You are trying to switch to the same menu twice");
                return;
            }

            previousMenuPrefab = currentMenuPrefab;
            currentMenuPrefab = targetMenuPrefab;

            if(currentMenu != null)
                currentMenu.DisableMenu();

            currentMenu = Instantiate(targetMenuPrefab).GetComponent<MenuBehaviour>();

            if(currentMenu != null)
                currentMenu.EnableMenu();
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
    }
}