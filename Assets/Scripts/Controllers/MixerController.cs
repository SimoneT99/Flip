using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Controller del mixer, si occupa di agire direttamente sul mixer
public class MixerController : MonoBehaviour
{
    //Riferimento al mixer
    [SerializeField] private AudioMixer mixer;

    private float muted = -80f;

    public void setMaster(float value)
    {
        if (value != 0)
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }
        else
        {
            mixer.SetFloat("MasterVolume", muted);
        }
        /*
        float temp = Convert(muted, maxMasterDecibel, value);
        Debug.Log("Master: " + temp + "dB");
        mixer.SetFloat("MasterVolume", temp);
        */
    }
    public void setMusic(float value)
    {
        if (value != 0)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        }
        else
        {
            mixer.SetFloat("MusicVolume", muted);
        }
    }
    public void setSfx(float value)
    {
        if (value != 0)
        {
            mixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
        }
        else
        {
            mixer.SetFloat("EffectsVolume", muted);
        }
    }
}
