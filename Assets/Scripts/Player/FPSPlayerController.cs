using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : BaseGameObject
{

    public CharacterController characterController;

	public ShootController shootController;

    public float movementSpeed;

    public float gravity;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float sidestep = Input.GetAxis("Horizontal");
        Vector3 movement = forward * FPSCamera.CameraForwardVectorOnGround +
                            sidestep * FPSCamera.CameraRightVectorOnGround;

        transform.forward = FPSCamera.CameraForwardVectorOnGround;

        if (Input.GetMouseButton(0)){
			shootController.Shoot();
		}

            characterController.Move(Time.deltaTime * (movementSpeed * movement - gravity * Vector3.up));
    }
}
