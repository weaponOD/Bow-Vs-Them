using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Camera Options")]
    [SerializeField]
    bool allowXRotation = true;
    [SerializeField]
    bool invertY = false;
    [SerializeField]
    bool invertX = false;

    //[Header("Player Options")]
    //[SerializeField]
    //bool isGrounded = true;

    //Player velocity
    Vector3 playerVelocity = Vector3.zero;
    //Player rotation
    Vector3 playerRotation = Vector3.zero;
    ////Jump Force
    //Vector3 jumpForce = Vector3.zero;

    //Camera rotation
    float cameraRotationX = 0;
    //Current Cam
    float current_cameraRotationX = 0;

    [SerializeField]
    float camRotY_Max;
    [SerializeField]
    float camRotY_Min;

    Vector3 checkOrigin = Vector3.zero;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    float checkDistance;
    [SerializeField]
    LayerMask floorMask;

    //Cache
    Rigidbody rb;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        //GetSphereCastInfo();
    }

    //gets a movement vector
    public void GetMovementVector(Vector3 _velocity)
    {
        playerVelocity = _velocity;
    }

    //gets movement rotation vector
    public void GetRotationVector(Vector3 _rotation)
    {
        playerRotation = _rotation;
    }

    //gets a rotation vector
    public void GetCameraRotation(float _camRotation)
    {
        cameraRotationX = _camRotation;
    }

    ////gets jump amount
    //public void GetJumpVector(Vector3 _jumpForce)
    //{
    //    jumpForce = _jumpForce;
    //}

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }

    private void MovePlayer()
    {
        //Walking
        if (playerVelocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + playerVelocity * Time.fixedDeltaTime);
        }

        //PlayerJump();
    }

    //private void PlayerJump()
    //{
    //    IsGroundedCheck();

    //    //if in the air, cannot jump
    //    if (!isGrounded) return;
        
    //    if (jumpForce != Vector3.zero)
    //    {
    //        rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            
    //    }
    //    IsGroundedCheck();
    //}

    //bool IsGroundedCheck()
    //{
    //    Debug.DrawRay(transform.position, new Vector3(0, (-1 * checkDistance), 0), Color.red);

    //    //sphercast downwards, if we hit "floor" layer, player is grounded
    //    if(Physics.SphereCast(new Ray(transform.position, Vector3.down), checkRadius, checkDistance, floorMask))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void RotatePlayer()
    {
        //Rotate Player in Y
        if (invertX)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(-playerRotation));
        }
        else
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(playerRotation));
        }
    }

    private void RotateCamera()
    {
        //If we dont want xRot, return
        if (!allowXRotation) return;

        //Rotate Camera in X, check for invert
        if (invertY)
        {
            current_cameraRotationX += cameraRotationX;
        }
        else
        {
            current_cameraRotationX -= cameraRotationX;
        }
        //Clamp and rotate
        current_cameraRotationX = Mathf.Clamp(current_cameraRotationX, camRotY_Min, camRotY_Max);
        cam.transform.localEulerAngles = new Vector3(current_cameraRotationX, 0, 0);
    }

    //void GetSphereCastInfo()
    //{
    //    checkOrigin = transform.position;

    //    float _height = GetComponent<CapsuleCollider>().height;
    //    float _width = GetComponent<CapsuleCollider>().radius/2;

    //    checkRadius = _width / 2 + 0.05f;
    //    checkDistance = _height / 2 + 0.05f;
    //}


}
