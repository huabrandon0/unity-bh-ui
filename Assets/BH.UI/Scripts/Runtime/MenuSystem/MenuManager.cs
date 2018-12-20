using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BH.UI
{
    public class MenuManager : MonoBehaviour
    {
        public MainMenu MainMenuPrefab;
        public SettingsMenu SettingsMenuPrefab;
        public Setting1Menu Setting1MenuPrefab;
        public Setting2Menu Setting2MenuPrefab;

        private Stack<Menu> menuStack = new Stack<Menu>();

        public static MenuManager Instance { get; set; }

        private void Awake()
        {
            Instance = this;

		    MainMenu.Show();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

	    public void CreateInstance<T>() where T : Menu
	    {
		    var prefab = GetPrefab<T>();

		    Instantiate(prefab, transform);
	    }

	    public void OpenMenu(Menu menu, bool animate = true, NoArgDelegate callback = null)
        {
            if (menuStack.Count > 0 && menuStack.Peek() == menu)
                return;

            // Deactivate top menu.
            if (menuStack.Count > 0)
            {
			    if (menu.DisableMenusUnderneath)
			    {
				    foreach (var m in menuStack)
				    {
                        m.gameObject.SetActive(false);

					    if (m.DisableMenusUnderneath)
						    break;
				    }
			    }

                var topCanvas = menu.GetComponent<Canvas>();
                var previousCanvas = menuStack.Peek().GetComponent<Canvas>();
			    topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
            }
            
            menuStack.Push(menu);

            if (menu._animatedElementOverlay)
                menu._animatedElementOverlay.Enter(callback);
            else
                callback.Invoke();
        }

        private T GetPrefab<T>() where T : Menu
        {
            // Get prefab dynamically, based on public fields set from Unity.
		    // You can use private fields with SerializeField attribute too.
            var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var field in fields)
            {
                var prefab = field.GetValue(this) as T;
                if (prefab != null)
                {
                    return prefab;
                }
            }

            throw new MissingReferenceException("Prefab not found for type " + typeof(T));
        }
	    
	    public bool CloseMenu(Menu menu, bool animate = true, NoArgDelegate callback = null)
	    {
		    //if (menuStack.Count == 0)
		    //{
			   // Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty.", menu.GetType());
			   // return false;
		    //}

		    //if (menuStack.Peek() != menu)
		    //{
			   // Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", menu.GetType());
			   // return false;
		    //}

            if (!menuStack.Contains(menu))
            {
                //Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not in the stack.", menu.GetType());
                return false;
            }

            // Close non-target menus.
            while (menuStack.Peek() != menu)
                CloseTopMenu();

            // Close target menu.
            CloseTopMenu(animate, callback);
            return true;
	    }

        public void CloseTopMenu(bool animate = true, NoArgDelegate callback = null)
        {
            var topMenu = menuStack.Pop();

            void CloseTopMenuHelper()
            {
                if (topMenu.DestroyWhenClosed)
                    Destroy(topMenu.gameObject);
                else
                    topMenu.gameObject.SetActive(false);

                // Reactivate the top menu.
                // If a reactivated menu is an overlay we need to activate the menu under it.
                foreach (var menu in menuStack)
                {
                    menu.gameObject.SetActive(true);

                    if (menu.DisableMenusUnderneath)
                        break;
                }

                if (callback != null)
                    callback.Invoke();
            }

            if (topMenu._animatedElementOverlay && animate)
                topMenu._animatedElementOverlay.Exit(CloseTopMenuHelper);
            else
                CloseTopMenuHelper();
        }

        private void Update()
        {
            // On Android the back button is sent as Esc
            if (Input.GetKeyDown(KeyCode.Escape) && menuStack.Count > 0)
            {
                menuStack.Peek().OnBackPressed();
            }
        }
    }
}
