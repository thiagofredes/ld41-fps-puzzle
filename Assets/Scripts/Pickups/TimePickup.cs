using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : Pickup
{

    public AudioSource audioSource;

    public AudioClip timeClip;

    public int seconds;

    protected override void OnContactDo()
    {
        if (_canContact)
        {
            _canContact = false;
            GameManager.IncreaseTimer(seconds);
            audioSource.PlayOneShot(timeClip);
            
            pickupCanvas.enabled = false;
            foreach(Renderer pickupRenderer in pickupRenderers)
                pickupRenderer.enabled = false;

            StartCoroutine(WaitForSoundToPlay());
        }
    }

    private IEnumerator WaitForSoundToPlay()
    {
        YieldInstruction endOfFrame = new WaitForEndOfFrame();
        while (audioSource.isPlaying)
            yield return endOfFrame;
        Destroy(gameObject);
    }
}
