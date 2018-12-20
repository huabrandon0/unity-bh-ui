using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BH.UI
{
    public class SettingsMenu : SimpleMenu<SettingsMenu>
    {
        public void OnBackButtonPressed()
        {
            Hide(callback: () =>
            {
                MainMenu.Show();
            });
        }

        public void OnSetting1Pressed()
        {
            Setting2Menu.Hide(false);
            Setting1Menu.Show();
        }

        public void OnSetting2Pressed()
        {
            Setting1Menu.Hide(false);
            Setting2Menu.Show();
        }
    }
}
