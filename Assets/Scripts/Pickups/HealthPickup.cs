using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {	

	protected override void OnContactDo(){
        FPSPlayerController.FPSPlayerInstance.AddHealth(2);
    }
}
