using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [CreateAssetMenu(fileName = "New UIButtonSoundEffects", menuName = "BH.UI/UIButtonSoundEffects", order = 4)]
    public class UIButtonSoundEffects : ScriptableObject
    {
        public AudioClip _playOnButtonDown;
        public AudioClip _playOnButtonUp;
        public AudioClip _playOnButtonEnter;
        public AudioClip _playOnButtonExit;
    }
}
