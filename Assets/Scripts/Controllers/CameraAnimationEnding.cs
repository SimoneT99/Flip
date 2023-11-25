using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Questa classe serve ad avvisare il controller della camera che l'animazione è finita
public class CameraAnimationEnding : StateMachineBehaviour
{
    //reference al camera controller
    private CameraController cameraController;

    //se si entra nello stato a cui questo script è attaccato allora scatta l'esecuzione di questo metodo
     public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
     {
        //prendiamo il riferimento all cameraController, non da fastidio perchè eseguito prima del gioco
        cameraController = animator.GetComponent<CameraController>();

        //avvisiamo il camera controller che l'animaione è finita
        cameraController.manageStartingAnimationEnding();
     }
}
