using System;
using UnityEngine;

namespace Utilities.Audio
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager instance;
        public AudioSource audioSource;
        public AudioClip[] sfxSource;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(string auidoName)
        {
            foreach (var clip in sfxSource)
            {
                if (clip.name.Contains(auidoName))
                {
                    // audioSource.clip = clip;
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }
}
