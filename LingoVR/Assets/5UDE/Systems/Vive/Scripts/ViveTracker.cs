using UnityEngine;
using System.Collections;

public class ViveTracker : Tracker {
	
    // Private variables
    SteamVR_TrackedObject trackedObject = null;

    // Called at the end of the program initialization
    protected override void Start () {
		
        // Get the Vive Controller from parent
        trackedObject = GetComponent<SteamVR_TrackedObject> ();
    }

    // Called every graphical frame
    protected override void Update () {
		
        // Check that tracked object is valid
		if (trackedObject != null && trackedObject.isActiveAndEnabled) {
			
			// Get the Vive Controller's input
			var device = SteamVR_Controller.Input ((int)trackedObject.index);
			// Check if it is valid
			valid = device.valid && device.connected && device.hasTracking && !device.outOfRange;
		}
	}

	// Public function to retrieve the tracker's position in the physical tracking space
	public override Vector3 GetPhysicalPosition () {

		// Ensure value is updated first
		Update ();
		// Return the local position relative to the Vive tracking space
		return transform.parent.parent.parent.InverseTransformPoint(transform.parent.position);
	}

	// Public function to retrieve the tracker's rotation in the physical tracking space
	public override Quaternion GetPhysicalRotation () {

		// Ensure value is updated first
		Update ();
		// Return the local rotation relative to the Vive tracking space
		return transform.parent.localRotation * transform.parent.parent.localRotation;
	}	

	// Called by the Vive Simulator
	public void Simulate (bool setValid, Vector3 physicalPosition, Quaternion physicalRotation) {
		
		// Simulate validity
		valid = setValid;

		// If valid, update simulated position and rotation
		if (valid) {
			transform.localPosition = physicalPosition;
			transform.localRotation = physicalRotation;
		}
	}
}