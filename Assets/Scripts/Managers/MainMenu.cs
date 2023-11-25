using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manager del Main Menu 
public class MainMenu : MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -controllo not null
    * -elimina duplicati
    */
    private static MainMenu _mainMenu;

    public static MainMenu mainMenu { get { return _mainMenu; } }


    private void Awake()
    {
        if (_mainMenu != null && _mainMenu != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _mainMenu = this;
        }
    }

    /////////////////////////////////////////////////////////////////////

    //reference allo scriptable object che gestisce la selezione del personaggio
    [SerializeField] private ActiveCharacter _activeCharacter;
    //Sliders
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider EffectsSlider;

    //Se chiamato carica la scena passata in input
    public void LoadLevel(string IDLivello)
    {
        Debug.Log("Loading level: " + IDLivello);
        SceneLoader.sceneLoader.LoadLevelProgressBar(IDLivello);
    }

    //Se chiamato esce dal gioco
    public void ExitGame()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }

    //Carica le impostazioni audio e le setta negli slider
    public void loadSettings()
    {
        //AudioSettings
        if (MasterSlider != null)
        {
            MasterSlider.value = AudioManager.audioManager.getMasterLevel();
        }
        if (MusicSlider != null)
        {
            MusicSlider.value = AudioManager.audioManager.getMusicLevel();
        }
        if (EffectsSlider != null)
        {
            EffectsSlider.value = AudioManager.audioManager.getEffectsLevel();
        }
    }

    //Metodi per gestire i valori degli slider nel caso vengano cambiati
    public void OnMasterValueChange(float value)
    {
        AudioManager.audioManager.setMasterLevel(value);
    }

    public void OnMusicValueChange(float value)
    {
        AudioManager.audioManager.setMusicLevel(value);
    }

    public void OnEffetcsValueChange(float value)
    {
        AudioManager.audioManager.setEffectsLevel(value);
    }

    //salva le impostazioni quando viene chiamato
    public void saveSettings()
    {
        AudioManager.audioManager.SaveAudioSettings();
    }

    
    //gestione del menù di selezione personaggio

    [SerializeField] private Image _characterPreview;
    [SerializeField] private Sprite Eve;
    [SerializeField] private Sprite Crypto;

    //seleziona il personaggio passato come stringa
    public void setCharcter(string character)
    {
        _activeCharacter.SetActiveCharacter(character);
        ManagePlayerPreview();
    }

    //setta le impostazioni del menù di selezione personaggio
    public void LoadCharacterSettings()
    {
        ManagePlayerPreview();
    }

    //Gestisce la preview
    public void ManagePlayerPreview()
    {
        string name = _activeCharacter.GetActiveCharacterName();
        if (name.Equals("EVE"))
        {
            _characterPreview.sprite = Eve;
        } else if (name.Equals("CRYPTO"))
        {
            _characterPreview.sprite = Crypto;
        }
    }
}
