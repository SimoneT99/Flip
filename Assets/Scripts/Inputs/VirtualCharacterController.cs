using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//classe per separare gli input dal movimento vero e proprio tramite eventi
public class VirtualCharacterController : MonoBehaviour
{
    //evento per il movimento nell'asse y
    public event Action<float> onMoveZAxis;
    public void MoveZAxis(float entity)
    {
        if (onMoveZAxis != null)
        {
            onMoveZAxis(entity);
        }
    }

    //evento per il movimento nell'asse y
    public event Action<float> onMoveXAxis;
    public void MoveXAxis(float entity)
    {
        if (onMoveXAxis != null)
        {
            onMoveXAxis(entity);
        }
    }

    //evento per il salto
    public event Action onJump;
    public void Jump()
    {
        if (onJump != null)
        {
            Debug.Log("inviato il salto");
            onJump();
        }
    }

    public event Action onGravityflip;
    public void gravityflip()
    {
        if (onGravityflip != null)
        {
            onGravityflip();
        }
    }
}
