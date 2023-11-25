using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UIController : MonoBehaviour
{

    

    public GameObject Game;
    public GameObject Menu;
    public GameObject EndingScreen;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject nextLevel;


    public GameObject TipWindow;
    public TMP_Text Tips;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;

    [SerializeField] private TMP_Text BestTime;
    [SerializeField] private TMP_Text YourTime;

    private bool isMenuOpen;

    public bool isMenuOpenable = true;

    [SerializeField] private AudioClip tipSound;
    [SerializeField] private AudioClip endingSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private EventSystem UIeventSystem;

    private IEnumerator keepTipAlive;
    public void start()
    {
        isMenuOpen = Menu.active;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Menu") && isMenuOpenable) {
            SwitchOpenClose();
        }
    }

    public void disableOpeningMenu()
    {
        isMenuOpenable = false;
    }

    public void enableOpeningMenu()
    {
        isMenuOpenable = true;
    }

    public void SwitchOpenClose()
    {
        Debug.Log("switchOpenClose");
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen == true)
        {
            Menu.SetActive(true);
            Game.SetActive(false);
            UIeventSystem.SetSelectedGameObject(backButton);
            LevelManagerBasic.levelManagerBasic.PauseGame();
        }
        else
        {
            Back();
        }
    }

    public void LoadScene(string ID)
    {
        //LevelManagerBasic.levelManagerBasic.unPauseGame();
        SceneLoader.sceneLoader.LoadLevelProgressBar(ID);
    }

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

    public void Back()
    {
        isMenuOpen = false;
        Menu.SetActive(false);
        Game.SetActive(true);
        LevelManagerBasic.levelManagerBasic.unPauseGame();
    }

    public void OpenEndingScreen()
    {
        LevelManagerBasic.levelManagerBasic.PauseGame();
        Debug.Log("ending screen");
        isMenuOpenable = false;
        EndingScreen.SetActive(true);
        isMenuOpen = false;
        Menu.SetActive(false);
        Game.SetActive(false);
        _audioSource.PlayOneShot(endingSound, 0.1f);
        BestTime.text = Timer.timer.getBest().ToString("0.00") + "s";
        YourTime.text = Timer.timer.getTime().ToString("0.00") + "s";
        UIeventSystem.SetSelectedGameObject(nextLevel);
    }

    

    public void DisplayTip(String tip)
    {
        if (KeepTipAlive_isRunning) {
            StopCoroutine(keepTipAlive);
            KeepTipAlive_isRunning = false;
        }
        
        openTipWindow();
        Tips.text = tip;
        keepTipAlive = KeepTipAlive();
        StartCoroutine(keepTipAlive);
    }

    private bool KeepTipAlive_isRunning = false;
    private IEnumerator KeepTipAlive()
    {
        KeepTipAlive_isRunning = true;
        yield return new WaitForSeconds(10f);
        Tips.text = "";
        closeTipWindow();
        KeepTipAlive_isRunning = false;
        yield break;
    }

    private void openTipWindow()
    {
        _audioSource.PlayOneShot(tipSound);
        TipWindow.active = true;
    }
    private void closeTipWindow()
    {
        TipWindow.active = false;
    }

    public void SaveSettings()
    {
        AudioManager.audioManager.SaveAudioSettings();
    }
}
