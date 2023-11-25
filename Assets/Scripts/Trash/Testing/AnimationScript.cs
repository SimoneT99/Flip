using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationScript : MonoBehaviour
{

    public Transform _TR;
    public Animator _Animator;
    public NavMeshAgent _navMeshAgent;

    public float treshold = .75f;
    // Start is called before the first frame update
    void Start()
    {
        _TR = GetComponent<Transform>();
        _Animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = _navMeshAgent.velocity;

        float zSpeed = Vector3.Dot(_TR.forward, velocity.normalized);
        float xSpeed = Vector3.Dot(_TR.right, velocity.normalized);

        _Animator.SetFloat("Forward", zSpeed, .0f, Time.deltaTime);
        _Animator.SetFloat("Horizontal", xSpeed, .0f, Time.deltaTime);
    }
}
