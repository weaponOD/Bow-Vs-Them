using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public PlayerWeapon currentWeapon;

    [SerializeField]
    LayerMask hitMask;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //load arrow and hold

        }

        if (Input.GetButtonUp("Fire1"))
        {
            //Release Arrow

        }
    }

    void LoadArrow()
    {

    }

    void FireArrow()
    {

    }

    void Shoot()
    {
        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, hitMask))
        {
            //make this hit spot the target of our arrow
        }

    }

}
