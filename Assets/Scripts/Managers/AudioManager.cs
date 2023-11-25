using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Manager dell'audio, permette a tutti gli script esterni di gestire l'audio attraverso i metodi che questo mette a disposizione
public class AudioManager :  MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -presente in tutte le scene
    */
    private static AudioManager _audioManager;

    public static AudioManager audioManager { get { return _audioManager; } }


    void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //////////////////////////////////

    //riferimenti necessari
    [SerializeField] private MixerController _mixerController; //riferimento al MixerController che gestirà direttamente il mixer
    [SerializeField] private AudioSettings _audioSettings; //uno scriptable object contenente le impostazioni attuali dell'audio

    public void Start()
    {
        //Carichiamo dalle preferenze utente i volumi salvati
        _audioSettings.setMaster(LoadAudioPrefs("MasterVolume"));
        _audioSettings.setMusic(LoadAudioPrefs("MusicVolume"));
        _audioSettings.setSfx(LoadAudioPrefs("EffectsVolume"));

        //Settiamo nel mixer i valori caricati precedentemente, comunicandoli al MixerController
        setMasterLevel(_audioSettings.getMaster());
        Debug.Log("inizialized Master");
        setMusicLevel(_audioSettings.getMusic());
        Debug.Log("inizialized Music");
        setEffectsLevel(_audioSettings.getSfx());
        Debug.Log("inizialized Effects");
    }

    //Serie di metodi per ottenere i livelli attuali di audio, utili se bisogna mostrarli come nel caso degli slider nel menù opzioni
    public float getMusicLevel()
    {
        return _audioSettings.getMusic();
    }

    public float getEffectsLevel()
    {
        return _audioSettings.getSfx();
    }

    public float getMasterLevel()
    {
        return _audioSettings.getMaster();
    }

    //Serie di metodi per settarre i livelli attuali di audio, utili se bisogna modificarli attraverso altre classi
    public void setMusicLevel(float value)
    {
        _audioSettings.setMusic(value);
        _mixerController.setMusic(value);
        Debug.Log("Music value: " + value);
    }

    public void setEffectsLevel(float value)
    {
        _audioSettings.setSfx(value);
        _mixerController.setSfx(value);
        Debug.Log("Effects value: " + value);
    }

    public void setMasterLevel(float value)
    {
        _audioSettings.setMaster(value);
        _mixerController.setMaster(value);
        Debug.Log("Master value: " + value);
    }

    //Metodo per salvare le preferenze
    private void SaveAudioPrefs(string what, float value)
    {
        PlayerPrefs.SetFloat(what, value);
    }

    //Metodo per caricare le preferenze
    private float LoadAudioPrefs(string what)
    {
        return PlayerPrefs.GetFloat(what, 1f);
    }

    //Metodo per salvare tutte le impostazioni audio
    public void SaveAudioSettings()
    {
        SaveAudioPrefs("MasterVolume", _audioSettings.getMaster());
        SaveAudioPrefs("MusicVolume", _audioSettings.getMusic());
        SaveAudioPrefs("EffectsVolume", _audioSettings.getSfx());
    }

    //se l'oggetto viene distrutto prima salviamo le impostazioni audio.
    private void OnDestroy()
    {
        SaveAudioSettings();
    }
}
