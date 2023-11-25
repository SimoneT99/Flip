using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -presente in tutte le scene
    */
    private static Timer _timer;

    public static Timer timer { get { return _timer; } }


    void Awake()
    {
        if (_timer == null)
        {
            _timer = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    /////////////////////////////////
    private string currentLevel;
    private float time;
    private float bestTimeInThisLevel;


    //attributo per gesttire la terminazione della coroutine
    private IEnumerator countSubrutine;

    //coroutine per contare il tempo
    private IEnumerator count()
    {
        while (true)
        {
            time += Time.deltaTime;
            Debug.Log("Time passed: " + time);
            yield return null;
        }
    }

    //se chiamato comincia a contare settando il livello attuale
    public void startCounting(string level)
    {
        currentLevel = level;
        time = 0f;
        countSubrutine = count();
        StartCoroutine(countSubrutine);
    }

    //se chiamato smette di contare
    public void stopCounting()
    {
        StopCoroutine(countSubrutine);
    }

    //se chiamato ritorna il tempo attuale
    public float getTime()
    {
        return this.time;
    }

    //se chiamato ricominca a contare
    public void restartTime()
    {
        this.time = 0f;
    }

    //Se chiamato ritorna il tempo migliore del livello passato come argomento
    public float getBest(string level)
    {
        return PlayerPrefs.GetFloat(level + "BestTime");
    }

    //Se chiamato ritorna il tempo migliore del livello attuale
    public float getBest()
    {
        return PlayerPrefs.GetFloat(currentLevel + "BestTime");
    }

    //Se chiamato salva il tempo attuale
    public void saveTime()
    {
        if (PlayerPrefs.GetFloat(currentLevel + "BestTime", float.MaxValue) > time)
        {
            PlayerPrefs.SetFloat(currentLevel + "BestTime", time);
        }
    }
}
