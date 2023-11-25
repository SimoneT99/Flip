using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Classe che gestirà il caricamento delle scene durante il gioco
class SceneLoader : MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -presente in tutte le scene
    */
    private static SceneLoader _sceneLoader;

    public static SceneLoader sceneLoader { get { return _sceneLoader; } }
  

    void Awake()
    {
        if(_sceneLoader == null)
        {
            _sceneLoader = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    ///////////////////////

    //Se chiamato carica la scena con nome ID in modo sincrono
    public void LoadLevel(string ID)
    {
        SceneManager.LoadScene(ID, LoadSceneMode.Single);
    }


    private Slider progressBar;
    //Se chiamato carica la scena di caricamento
    public void LoadLevelProgressBar(string ID)
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single); //Carichiamo la scena con la progress bar
        
        StartCoroutine(LoadSceneAsinc(ID));
    }

    private IEnumerator LoadSceneAsinc(string ID)
    {
        yield return null;
        Debug.Log("entrati nel caricamento");
        progressBar = LoadingScreenManager.loadingScreenManager.getPoogresBarReference(); //Cerchiamo la progress bar nella scena
        Debug.Log("caricata la porgress bar");
        AsyncOperation operation = SceneManager.LoadSceneAsync(ID);
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}


