using UnityEngine;

namespace BH.UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] Menu _startMenu;
        Menu _currentlyOpened;

        void Start()
        {
            OpenMenu(_startMenu);
        }

        public void OpenMenu(Menu menu)
        {
            if (_currentlyOpened)
            {
                _currentlyOpened.Close(() => {
                    menu.Open(); _currentlyOpened = menu;
                });
            }
            else
            {
                menu.Open();
                _currentlyOpened = menu;
            }
        }
    }
}
