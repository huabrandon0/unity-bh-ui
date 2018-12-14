using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class UIAudioManager : Singleton<UIAudioManager>
    {
        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip audioClip)
        {
            if (!audioClip)
                return;

            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}
