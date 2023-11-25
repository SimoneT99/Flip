using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //riferimenti all'animator e il componente camera brain
    private Animator _cameraAnimator;
    private CinemachineBrain _cinemachineBrain;
    // Start is called before the first frame update
    void Start()
    {
        _cameraAnimator = GetComponent<Animator>();
        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    /**
     * Se chiamato viene avviata l'animazione della camera
     */
    public void startCameraAnimation()
    {
        this._cameraAnimator.SetTrigger("StartCameraAnimation");
    }

    /**
     * Se chiamato avvisa questo CameraController che l'animazione è finita
     * L'avviso viene inoltrato al manager della telecamera
     */
    public void manageStartingAnimationEnding()
    {
        //disattiviamo l'Animator
        _cameraAnimator.enabled = false;

        //Avvisiamo il manager che l'animazione è finita
        CameraManager.cameraManager.CameraAnimationEnded();

        //attiviamo il camera brain per permettere alla camera di essere gestita da cinemachine
        enableCameraBrain();
    }

    /**
     * Se chiamato attiva il componente camera brain
    */
    public void enableCameraBrain()
    {
        this._cinemachineBrain.enabled = true;
    }

    /**
     * Se chiamato disattiva il componente camera brain
    */
    public void disableCameraBrain()
    {
        this._cinemachineBrain.enabled = false;
    }
}
