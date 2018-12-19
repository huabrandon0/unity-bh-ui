using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    public class SettingsMenu : SimpleMenu<SettingsMenu>
    {
        public void OnBackButtonPressed()
        {
            Hide(() =>
            {
                MainMenu.Show();
            });
        }
    }
}
