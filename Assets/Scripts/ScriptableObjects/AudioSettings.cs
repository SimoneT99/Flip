using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


[CreateAssetMenu()]
//Scriptable object per contenere le impostazioni audio durante il gioco
public class AudioSettings : ScriptableObject
{
    [SerializeField]
    private float master;
    [SerializeField]
    private float music;
    [SerializeField]
    private float sfx;


    public float getMaster()
    {
        return master;
    }
    public float getMusic()
    {
        return music;
    }
    public float getSfx()
    {
        return sfx;
    }

    public void setMaster(float value)
    {
        master = value<0 ? 0 : value > 1 ? 1 : value;
    }
    public void setMusic(float value)
    {
        music = value < 0 ? 0 : value > 1 ? 1 : value;
    }
    public void setSfx(float value)
    {
        sfx = value < 0 ? 0 : value > 1 ? 1 : value;
    }
}

