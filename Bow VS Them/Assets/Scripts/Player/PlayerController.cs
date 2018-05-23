using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    float m_Speed = 5f;

    [SerializeField]
    float x_LookSensitivity = 3f;
    [SerializeField]
    float y_LookSensitivity = 10f;

    private PlayerMotor p_Motor;

	void Start () {
        p_Motor = GetComponent<PlayerMotor>();
	}
	
	void Update () {
        GetMovementInput();
        GetPlayerRotationInput();
        GetCameraRotationInput();
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
        p_Motor.Move(_velocity);
    }

    void GetPlayerRotationInput()
    {
        //Rotate character when looking left/right
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0) * x_LookSensitivity;

        p_Motor.Rotate(_rotation);
    }

    void GetCameraRotationInput()
    {
        //Rotate camera when looking up/down
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _camRotation = new Vector3(_xRot, 0, 0) * y_LookSensitivity;

        p_Motor.RotateCamera(_camRotation);
    }
}
