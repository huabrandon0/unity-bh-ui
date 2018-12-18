using UnityEngine;

namespace BH.UI
{
    public class MainMenu : SimpleMenu<MainMenu>
    {
        public void OnSettingsButtonPressed()
        {
            Close(SettingsMenu.Show);
        }
    }
}
