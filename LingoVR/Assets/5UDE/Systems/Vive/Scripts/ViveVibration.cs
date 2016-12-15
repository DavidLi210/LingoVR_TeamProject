using UnityEngine;
using System.Collections;

public class ViveVibration : Vibration {
	
    // Private variables
    SteamVR_TrackedObject trackedObject = null;

    // Called at the end of the program initialization
    protected override void Start () {
		
		// Get the Vive Controller from parent
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
    }

    // Public function to turn on vibration motor for current frame
    public override void Vibrate (float vibrationIntensity) {
		
		// Check that tracked object is valid
		if (trackedObject != null && trackedObject.isActiveAndEnabled) {
			
			// Get the Vive Controller's input 
			var device = SteamVR_Controller.Input ((int)trackedObject.index);

			// Ensure intensity is not over 100%
			if (vibrationIntensity > 1.0f) {
				vibrationIntensity = 1.0f;
			}

            // Or less than 0%
            else if (vibrationIntensity < 0.0f) {
				vibrationIntensity = 0.0f;
			}

			// Normalize intensity
			vibrationIntensity *= 3999.0f;
			// Activate Vive vibration motor
			device.TriggerHapticPulse ((ushort)vibrationIntensity);
		}
	}
}