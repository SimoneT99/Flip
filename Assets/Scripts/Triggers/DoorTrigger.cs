using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Door doorToManage;
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("Player opening door");
            doorToManage.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("Player closing door");
            doorToManage.CloseDoor();
        }
    }
}

