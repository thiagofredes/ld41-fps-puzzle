using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {	

    public AudioSource audioSource;

	protected override void OnContactDo(){
        FPSPlayerController.FPSPlayerInstance.AddHealth(2);
        audioSource.Play();
    }
}
