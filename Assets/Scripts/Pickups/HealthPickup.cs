using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {	

    public AudioSource audioSource;

    public AudioClip healthClip;

	protected override void OnContactDo(){
        FPSPlayerController.FPSPlayerInstance.AddHealth(2);
        audioSource.PlayOneShot(healthClip);
        StartCoroutine(WaitForSoundToPlay());
    }

    private IEnumerator WaitForSoundToPlay(){
        YieldInstruction endOfFrame = new WaitForEndOfFrame();
        while(audioSource.isPlaying)
            yield return endOfFrame;
        Destroy(gameObject);
    }
}
