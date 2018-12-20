using UnityEngine;

namespace BH.UI
{
    public class MainMenu : SimpleMenu<MainMenu>
    {
        public void OnSettingsButtonPressed()
        {
            Hide(callback: () =>
            {
                SettingsMenu.Show();
            });
        }
    }
}
