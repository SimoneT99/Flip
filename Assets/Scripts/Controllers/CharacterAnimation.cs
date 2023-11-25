using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe per gestire l'animazione del personaggio
public class CharacterAnimation : MonoBehaviour
{
    //il body controllre da cui ricevere gli eventi
    public BodyController bodyController;

    //l'animator con cui comunicare
    public Animator _Animator;

    //parametri per la corretta gestione delle animazioni
    float currentForward = 0;
    float currentLateral = 0;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();

        //associamo i metodi per gestire i trigger agli eventi del body controller
        bodyController.onChange2dSpeed += change2Danimation;
        bodyController.onPlayerJumped += triggerjump;
        bodyController.onPlayerLanding += triggerLanding;
    }

    void Update()
    {
        _Animator.SetFloat("Forward", currentForward, .1f, Time.deltaTime);
        _Animator.SetFloat("Horizontal", currentLateral, .1f, Time.deltaTime);
    }

    private void fixInRange(float min, float max, float number, ref float location)
    {
        location = number <= min ? min : number >= max ? max : number;
    }
    
    //Gestiamo trigger di salto
    private void triggerjump()
    {
        _Animator.SetTrigger("Jump");
    }

    //Gestiamo trigger di landing
    private void triggerLanding()
    {
        //controlliamo che siamo in midAir
        if (_Animator.GetCurrentAnimatorStateInfo(0).IsName("Mid Air") || _Animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping")) { 
            Debug.Log("setting land");
            _Animator.SetTrigger("Land");
        }
    }

    //Gestiamo il movimento orizzzontale
    private void change2Danimation(Vector3 forward, Vector3 right, Vector3 direction, float entity)
    {
        fixInRange(0, 1, entity, ref entity);
        currentForward = Vector3.Dot(forward, direction) * entity;
        currentLateral = Vector3.Dot(right, direction) * entity;
    }

    //Getiamo il trigger di morte
    public void PlayDeathAnimation()
    {
        _Animator.SetTrigger("Death");
    }
}
