using System.Collections;
using UnityEngine;

//classe che implementa l'input dell'utente
class StandardPlayerInput : MonoBehaviour
{
    public VirtualCharacterController _virtualCharacterController;

    private void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        _virtualCharacterController.MoveXAxis(xMovement);
        _virtualCharacterController.MoveZAxis(zMovement);
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Jump"))
        {
            Debug.Log("inviato il salto");
            _virtualCharacterController.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Fire1"))
        {
            _virtualCharacterController.gravityflip();
        }
    }
}
