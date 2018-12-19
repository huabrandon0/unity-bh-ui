using UnityEngine.UI;

namespace BH.UI
{
    public class OptionsMenu : SimpleMenu<OptionsMenu>
    {
        public Slider Slider;

        public void OnMagicButtonPressed()
        {
            AwesomeMenu.Show(Slider.value);
        }
    }
}
