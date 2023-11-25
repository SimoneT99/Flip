using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] string tip;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] UIController uiController;

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            uiController.DisplayTip(tip);
        }
    }

}

