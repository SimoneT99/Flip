using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

/**
 * Classe controller della componente Third Person Camera da script
*/
public class CinemachineController : MonoBehaviour
{
    //Riferimento alla componente che gestisce la camera
    private CinemachineFreeLook _vcam;

    public void Start()
    {
        //prendiamo il riferimento
        _vcam = GetComponent<CinemachineFreeLook>();
    }

    /**
     * Se chiamato attiviamo il componente per la gestione della camera in terza persona
    */
    public void Enable()
    {
        _vcam.enabled = true;
    }

    /**
     * Se chiamato disattiviamo il componente per la gestione della camera in terza persona
    */
    public void Disable()
    {
        _vcam.enabled = false;
    }

    /**
     * Se chiamato setta il Target della camera sul transform dell'oggetto passato
    */
    public void setTarget(Transform _transformToTarget)
    {
        Debug.Log("settando il target: " + _transformToTarget.position);
        _vcam.m_LookAt = _transformToTarget;
    }

    /**
     * Se chiamato setta il Follow della camera sul transform dell'oggetto passato
    */
    public void setFollow(Transform _transformToFollow)
    {
        Debug.Log("settando il follow: " + _transformToFollow.position);
        _vcam.m_Follow =  _transformToFollow;
    }

    /**
     * Se chiamato centra la camera
    */
    public void CenterCameraOnTarget(Transform transform)
    {
        Debug.Log("value = " + transform.rotation.eulerAngles.y);
        _vcam.m_XAxis.Value = transform.rotation.eulerAngles.y;
    }
}

