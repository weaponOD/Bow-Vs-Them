using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    bool allowXRotation = true;
    [SerializeField]
    bool invertXRotation = false;
    [SerializeField]
    bool invertYRotation = false;


    //Player velocity
    Vector3 p_Velocity = Vector3.zero;
    //Player rotation
    Vector3 p_Rotation = Vector3.zero;
    //Camera rotation
    Vector3 c_Rotation = Vector3.zero;

    //Cache
    Rigidbody rb;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    //gets a movement vector
    public void Move(Vector3 _velocity)
    {
        p_Velocity = _velocity;
    }

    //gets movement rotation vector
    public void Rotate(Vector3 _rotation)
    {
        p_Rotation = _rotation;
    }

    //gets a rotation vector
    public void RotateCamera(Vector3 _camRotation)
    {
        c_Rotation = _camRotation;
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }

    private void MovePlayer()
    {
        if (p_Velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + p_Velocity * Time.fixedDeltaTime);
        }
    }

    private void RotatePlayer()
    {
        //Rotate Player in Y
        if (invertYRotation)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(-p_Rotation));
        }
        else
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(p_Rotation));
        }

    }

    private void RotateCamera()
    {
        //If we dont want xRot, return
        if (!allowXRotation) return;

        //Rotate Camera in X, check for invert
        if (invertXRotation)
        {
            cam.transform.Rotate(c_Rotation);
        }
        else
        {
            cam.transform.Rotate(-c_Rotation);
        }

        
    }
}
