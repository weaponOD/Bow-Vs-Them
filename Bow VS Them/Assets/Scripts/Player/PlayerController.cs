using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [Header("Player Settings")]
    [SerializeField]
    float m_Speed = 5f;
    [SerializeField]
    float jump_Force = 1000f;

    //[Header("Spring Settings")]
    //[SerializeField]
    //private float jointSpring = 20;
    //[SerializeField]
    //private float jointMaxForce = 40;

    [Header("Camera Settings")]
    [SerializeField]
    float x_LookSensitivity = 3f;
    [SerializeField]
    float y_LookSensitivity = 10f;

    private PlayerMotor playerMotor;
    private ConfigurableJoint joint;
    

	void Start () {
        playerMotor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        //Set default spring position (ground)
        //SetJointSettings(jointSpring);
	}
	
	void Update () {
        GetMovementInput();
        GetPlayerRotationInput();
        GetCameraRotationInput();
       // GetJumpInput();
    }

    void GetMovementInput()
    {
        //raw input
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        //considering current rotation when moving
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * m_Speed;

        //Apply movement
        playerMotor.GetMovementVector(_velocity);
    }

    void GetPlayerRotationInput()
    {
        //Rotate character when looking left/right
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0) * x_LookSensitivity;

        playerMotor.GetRotationVector(_rotation);
    }

    void GetCameraRotationInput()
    {
        //Rotate camera when looking up/down
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _camRotation = _xRot * y_LookSensitivity;

        playerMotor.GetCameraRotation(_camRotation);
    }

    //void GetJumpInput()
    //{
    //    //if we haven't jumped, set to nil
    //    Vector3 _jumpForce = Vector3.zero;

    //    if (Input.GetButton("Jump"))
    //    {
    //        _jumpForce = Vector3.up * jump_Force;
    //        SetJointSettings(0);
    //    }
    //    else
    //    {
    //        SetJointSettings(jointSpring);
    //    }

    //    playerMotor.GetJumpVector(_jumpForce);
    //}

    //void SetJointSettings(float _jointSpring)
    //{
    //    joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
    //}
}
