using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager :  MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -controllo not null
    * -elimina duplicati
    */
    private static CameraManager _cameraManager;

    public static CameraManager cameraManager { get { return _cameraManager; } }


    private void Awake()
    {
        if (_cameraManager != null && _cameraManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _cameraManager = this;
        }
    }


    //////////////////////////////////


    [SerializeField] private CinemachineController _cinemachineController;
    [SerializeField] private CameraController _cameraController;

    //Se chiamato chiediamo al manager di settare il Transform passato come Target della Third Person Camera, si passa la richiesta al CinemachineController che se ne occuperà
    public void SetThisAsTarget(Transform transform)
    {
        _cinemachineController.setTarget(transform);
    }

    //Se chiamato chiediamo al manager di settare il Transform passato come Follow della Third Person Camera, si passa la richiesta al CinemachineController che se ne occuperà
    public void SetThisAsFollow(Transform transform)
    {
        _cinemachineController.setFollow(transform);
    }

    //Se chiamato chiediamo al manager di settare il Transform passato come Follow e Target della Third Person Camera, si passa la richiesta al CinemachineController che se ne occuperà
    public void SetThisAsTargetAndFollow(Transform transform)
    {
        _cinemachineController.setTarget(transform);
        _cinemachineController.setFollow(transform);
    }

    //Se chiamato chiediamo al manager di centrare la camera sul transform passato
    public void CenterCameraOnTarget(Transform transform)
    {
        _cinemachineController.CenterCameraOnTarget(transform);
    }

    //Se chiamato chiediamo di attivare la Third Person Camera
    public void EnableCameraBrain()
    {
        _cinemachineController.Enable();
    }

    //Se chiamato chiediamo di disattivare la Third Person Camera
    public void DisableCameraBrain()
    {
        _cinemachineController.Disable();
    }

    //Se chiamato il manager viene avvisato che l'animazione della camera è terminata
    public void CameraAnimationEnded()
    {
        LevelManagerBasic.levelManagerBasic.NotifyCameraAnimationEnded();
    }

    //Se chiamato il manager si occupa di far partire l'animazione, comunicando la richiesta al controler della camera
    public void StartCameraAnimation() {
        _cameraController.startCameraAnimation();
    }
}
