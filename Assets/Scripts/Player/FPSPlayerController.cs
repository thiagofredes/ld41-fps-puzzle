using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : BaseGameObject
{
    public static FPSPlayerController FPSPlayerInstance{
        get {
            return _instance;
        }
    }

    public CharacterController characterController;

	public ShootController shootController;

    public float movementSpeed;

    public float gravity;

    private static FPSPlayerController _instance;

    
    void Awake(){
        _instance = this;
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

    public void Damage(float damage){
        Debug.Log("Damaged by " + damage);
    }
}
