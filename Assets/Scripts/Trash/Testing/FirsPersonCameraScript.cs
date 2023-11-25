using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirsPersonCameraScript : MonoBehaviour
{
    private Transform myTransform;  //classic myTransform che indica le coordinate della telecamera
    public Transform target; //a field used to move the camera in a position
    public float alfaKeyboard = 1; //constant for the movement of the keyboard
    public float alfaMouse = 1; //constant for the movement of the mouse

    private float xRot, yRot; //woorkaround to solve the rotation on euler notation

    public bool invertXaxis = false, invertYaxis = false; //we can use them to invert the axis of the camera;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        xRot = myTransform.rotation.x;
        yRot = myTransform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        //control if the given buttons are pressed, meaning that the user want to invert the axys
        if (Input.GetKeyDown(KeyCode.T)) //get key down returns true if the button has ben pressed down (only for the first frame)
        {
            invertXaxis = !invertXaxis;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            invertYaxis = !invertYaxis;
        }

        //we calculate the costants for the invertion
        int xDir = 1, yDir = 1;

        if (invertXaxis)
        {
            xDir *= -1;
        }
        if (invertYaxis)
        {
            yDir *= -1;
        }

        if (Input.GetMouseButton(0))
        {
            xRot = xRot + Input.GetAxis("Mouse X") * xDir * alfaMouse;
            yRot = yRot + Input.GetAxis("Mouse Y") * yDir * alfaMouse;

            myTransform.rotation = Quaternion.Euler(yRot, xRot, 0);
        }
        if (Input.GetKey(KeyCode.K))
        {

            myTransform.rotation = Quaternion.Euler(yRot, xRot, 0);
        }
    }
}
