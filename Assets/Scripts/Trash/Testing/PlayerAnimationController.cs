using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{
    public Transform _myTransform;
    public CharacterController player;
    public Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = player.velocity;

        float zSpeed = Vector3.Dot(player.transform.forward, velocity);
        float xSpeed = Vector3.Dot(player.transform.right, velocity);

        _Animator.SetFloat("Forward", zSpeed, .0f, Time.deltaTime);
        _Animator.SetFloat("Horizontal", xSpeed, .0f, Time.deltaTime);

        //Debug.Log(velocity.magnitude + "nell'animator");
    }
}
