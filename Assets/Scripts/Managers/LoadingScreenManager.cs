using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Manager della schermata di caricamento, serve per avere un riferimento agli elementi della sschermata di caricamento
 */
public class LoadingScreenManager : MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -controllo not null
    * -elimina duplicati
    */
    private static LoadingScreenManager _loadingScreenManagerr;

    public static LoadingScreenManager loadingScreenManager { get { return _loadingScreenManagerr; } }


    private void Awake()
    {
        if (_loadingScreenManagerr != null && _loadingScreenManagerr != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _loadingScreenManagerr = this;
        }
    }

    /////////////////////////////

    [SerializeField] private Slider _progressBar;

    //Se chiamato ritorna il riferimento alla progress bar
    public Slider getPoogresBarReference()
    {
        return this._progressBar;
    }
}
