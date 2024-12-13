using System;
using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Utilities.Audio
{
  public class SFXSlider : MonoBehaviour
  {
    public AudioMixer audioMixer;
    private float _vol;
    public Slider sfx;
    public void Start()
    {
      _vol = PlayerPrefs.GetFloat(Constants.SFXVALUE,-40f);
      sfx.value = _vol;
    }

    public void SetVolume(float volume)
    {
      sfx.value = volume;
      _vol = volume;
      audioMixer.SetFloat("Volume", volume);
      PlayerPrefs.SetFloat(Constants.SFXVALUE, volume);
    }
  }
}
