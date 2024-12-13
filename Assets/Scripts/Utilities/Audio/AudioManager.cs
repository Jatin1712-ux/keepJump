using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Audio
{
    public class AudioManager : MonoBehaviour
    { 
        private int _firstPlayInt;
        private float _bgFloat;
        //private float _sfxFloat;

        public Slider bgSlider;
        //public Slider sfxSlider;

        public AudioSource bgAudio;
        //public AudioSource sfxAudio;
        
        private void Start()
        {
            bgAudio = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<AudioSource>();
            //sfxAudio = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<AudioSource>();
            
            _firstPlayInt = PlayerPrefs.GetInt(Constants.FIRSTPLAY,0);

            if (_firstPlayInt == 0)
            {
                _bgFloat = 0.25f;
                //_sfxFloat = 0.75f;
                bgSlider.value = _bgFloat;
                //sfxSlider.value = _sfxFloat;

                PlayerPrefs.SetFloat(Constants.BGMUSICVALUE , _bgFloat);
                //PlayerPrefs.SetFloat(Constants.SFXVALUE, _sfxFloat);
                PlayerPrefs.SetInt(Constants.FIRSTPLAY, -1);
            }
            else
            {
                _bgFloat = PlayerPrefs.GetFloat(Constants.BGMUSICVALUE, 0);
                bgSlider.value = _bgFloat;
                //_sfxFloat = PlayerPrefs.GetFloat(Constants.SFXVALUE);
                //sfxSlider.value = _sfxFloat;
                bgAudio.volume = bgSlider.value;
                //sfxAudio.volume = sfxSlider.value;
                
                
            }
        }

        

        public void SaveSoundSettings()
        {
            PlayerPrefs.SetFloat(Constants.BGMUSICVALUE , bgSlider.value);
            //PlayerPrefs.SetFloat(Constants.SFXVALUE, sfxSlider.value);
            PlayerPrefs.Save();
           
        }
        

        public void UpdateSound()
        {
            bgAudio.volume = bgSlider.value;
           // sfxAudio.volume = sfxSlider.value;
            SaveSoundSettings();
        }

        private void OnDestroy() {
            SaveSoundSettings();
        }
    }
}
