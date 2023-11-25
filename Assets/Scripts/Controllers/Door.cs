using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Door : MonoBehaviour
{
    //reference all'animator e l'audio source per il suono
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    //campi che contengono le clip audio che verranno utilizzate
    [SerializeField] private AudioClip opening;
    [SerializeField] private AudioClip closing;
    public void Start()
    {
        //prendiamo le referenze
        _animator = GetComponent<Animator>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    //se chiamato la porta deve essere aperta
    public void OpenDoor()
    {
        //suono apertura
        _audioSource.PlayOneShot(opening);

        //trigger per lo stato dell'animator
        _animator.SetTrigger("Open");
    }


    //analogo a quello sopra
    public void CloseDoor()
    {
        _audioSource.PlayOneShot(closing);
        _animator.SetTrigger("Close");
    }
}

