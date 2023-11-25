using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe che andrà a gestire il movimento di un personaggio sfruttando gli eventi
public class BodyController : MonoBehaviour
{
    //Il controller virtuale da cui ottenere gli input
    public VirtualCharacterController _playerVirtualController;

    //Varie reference
    public Transform _transform;
    public CharacterController _characterController;
    public Transform _cameraTR;
    public Transform _groundCheck;
    private AudioSource _audioSource;

    //Vari Parametri importanti
    public float maxFallingSpeed = 20;
    public float maxSurvivableFallingSpeed = 15;
    public bool fallingDamageEnabled = true;

    public float maxSpeed = 5;
    public float turningSmoothing = 0.1f;

    public float maxHeight = 3f;
    public float gravity = -9.81f;
    public float ray = 0.4f;

    public LayerMask layerMaskToGroundOn;

    public AudioClip landing;
    public AudioClip flippingGravity;

    //Vari parametri di supporto
    private bool movementUnlocked = true;
    public bool isGrounded = false;
    private float turnVelocity;
    public bool isGroundCheckEnabled = true;
    private float movementVelocity;

    private bool canFlip = false;

    private float nextZ;
    private float nextX;
    private bool askedJump;
    private bool askedFlipGravity;

    public bool lastFrameGround = true;

    private Vector3 currentDirection;
    private Vector3 latestKnownmovement; //direction*entity

    private Vector3 movementVelocityV3;

    //Forse non necessari
    //TODO rimuovere per pulizia
    public Vector3 velocity = Vector3.zero;
    public void Start()
    {
        //colleghiamo gli eventi del virtual controller ad i metodi per gestirli
        _playerVirtualController.onMoveXAxis += moveXAxis; 
        _playerVirtualController.onMoveZAxis += moveZAxis;
        _playerVirtualController.onJump += jump;
        _playerVirtualController.onGravityflip += flipGravity;

        //Prendiamo alcuni riferimenti importanti
        _characterController = GetComponent<CharacterController>();
        _cameraTR = GameObject.Find("Main Camera").transform;
        _audioSource = GetComponent<AudioSource>();
        IEnumerator TriggerLanding = triggerLanding();
        StartCoroutine(TriggerLanding);
    }

    public void Update()
    {

        if (movementUnlocked) { // muoviamo il personaggio solo se il movimento è sbloccato
            doGroundCheck(); //eseguiamo il ground check

            if (isGrounded && !lastFrameGround) { //se stiamo atterrando
                if (maxSurvivableFallingSpeed < Math.Abs(velocity.y) && fallingDamageEnabled) //gestiamo i danni da caduta se attivi
                {
                    this.GetComponent<Killable>().kill();
                }
                Debug.Log("atterrando");
                PlayerLanding();
            }

            if (isGrounded && ((velocity.y < 0) && (gravity < 0) || (velocity.y > 0) && (gravity > 0))) //per fare in modo che il character controller rimanga attaccato al pavimento
            {
                velocity = new Vector3(0f, (gravity < 0 ? -2 : +2), 0f);
            }

            if (isGrounded && askedJump) //se il player tocca terra ed è stato richiesto il salto saltiamo
            {
                //Debug.Log("sto per saltare");
                velocity.y = Mathf.Sqrt(maxHeight * 2f * Mathf.Abs(gravity)) * (gravity < 0 ? +1 : -1);
                PlayerJumped();
            }

            if (!isGrounded && askedFlipGravity && canFlip) //se in aria , richiesto il flip e il flip è attivo si cambia la gravità
            {
                canFlip = false;
                this.gravity *= -1;
                this._transform.Rotate(_transform.forward, 180f);
                _audioSource.PlayOneShot(flippingGravity, 0.2f);
            }

            velocity.y += gravity * Time.deltaTime; //calcolo velocità verticale istantanea
            _characterController.Move(velocity * Time.deltaTime); //applicazione della velocità istantanea

            ///////////////////////Movimento nel piano orizzzontale/////////////////////////////////////

            Vector3 direction = new Vector3(nextX, 0, nextZ).normalized; //calcolo della direzione orizzontale in questo frame
            float entity = (float)Math.Sqrt(nextX * nextX + nextZ * nextZ); //calcolo dell'entità dello spostamento
            fixInRange(0, 1, entity, ref entity);

            //il player sta comunicando il movimento
            if (entity >= 0.1f)
            {
                //Gestire la rotazione con smoothing
                float targetDegres = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTR.eulerAngles.y;
                float degres = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetDegres, ref turnVelocity, turningSmoothing);


                currentDirection = Quaternion.Euler(0f, targetDegres, 0f) * Vector3.forward;
                _characterController.transform.rotation = Quaternion.Euler(_characterController.transform.rotation.eulerAngles.x, degres, _characterController.transform.rotation.eulerAngles.z);
                _characterController.Move(currentDirection.normalized * maxSpeed * Time.deltaTime * entity);
            }
            ///////////////////////
            Change2dSpeed(_transform.forward, _transform.right, currentDirection, entity);
            //////////////////////////////////////////////////////////////
            resetInput();
            lastFrameGround = isGrounded;
            if (!canFlip && isGrounded)
            {
                canFlip = true;
            }
        }
    }

    //Se chiamato blocca il movimento
    public void LockMovement()
    {
        movementUnlocked = false;
    }

    //metodi utility

    private void fixInRange(float min, float max, float number, ref float location)
    {
        location = number <= min ? min : number >= max ? max : number;
    }

    private void resetInput()
    {
        nextX = 0;
        nextZ = 0;
        askedFlipGravity = false;
        askedJump = false;
    }

    private void doGroundCheck()
    {

        isGrounded = Physics.CheckSphere(_groundCheck.position, ray, layerMaskToGroundOn);
    }

    ///////////////// metodi degli eventi ////////////////////////
    private void moveZAxis(float entity)
    {
        this.nextZ = (entity > 1 ? 1 : entity); //mettiamo al massimo 1 per il movimento della Z
    }
    private void moveXAxis(float entity)
    {
        this.nextX = (entity > 1 ? 1 : entity); //mettiamo al massimo 1 per il movimento della X
    }

    private void flipGravity()
    {
        this.askedFlipGravity = true;
    }

    private void jump()
    {
        Debug.Log("impostato askedJump True");
        this.askedJump = true;
    }

    ///////////////// eventi per la comunicazione con il livello inferiore (animazione) ////////////////////////

    public Action<Vector3, Vector3, Vector3, float> onChange2dSpeed;

    private void Change2dSpeed(Vector3 forward, Vector3 right, Vector3 direction, float entity)
    {
        if (onChange2dSpeed != null)
        {
            onChange2dSpeed(forward, right, direction, entity);
        }
    }

    public Action onPlayerJumped;
    private void PlayerJumped()
    {
        if (onChange2dSpeed != null)
        {
            onPlayerJumped();
        }
    }

    

    public Action onPlayerLanding;
    private void PlayerLanding()
    {
        _audioSource.PlayOneShot(landing, 0.3f);
        if (onPlayerLanding != null)
        {
            onPlayerLanding();
        }
    }
    //fix landing inconsistency

    private IEnumerator triggerLanding()
    {
        while (true) {
            yield return new WaitForSeconds(2);
            if (isGrounded)
            {
                if (onPlayerLanding != null)
                {
                    onPlayerLanding(); 
                }
            }
        }
    }

    //debug
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.DrawSphere(_groundCheck.position, ray);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

}
