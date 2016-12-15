using UnityEngine;
using System.Collections;

public class ViveAxis : Axis {
	
    // Enumerate Vive axis names
    public enum ViveAxisName
    {
        Touchpad,
        Trigger
    };

    // Reveal button name in the Inspector
    public ViveAxisName axisName;

    // Private variables
    SteamVR_TrackedObject trackedObject = null;

    // Called at the end of the program initialization
    protected override void Start () {
		
        // Get the Vive Controller from parent
        trackedObject = GetComponentInParent<SteamVR_TrackedObject> ();
    }

    // Called every graphical frame
    protected override void Update () {
		
		// Check that tracked object is valid
		if (trackedObject != null && trackedObject.isActiveAndEnabled) {
			
			// Get the Vive Controller's input 
			var device = SteamVR_Controller.Input ((int)trackedObject.index);

			// Determine which axis to check
			Valve.VR.EVRButtonId axisMask;

			// Get trigger axis
			if (axisName == ViveAxisName.Trigger) {
				axisMask = Valve.VR.EVRButtonId.k_EButton_Axis1;
			} 

			// Get touchpad axis by default
			else {
				axisMask = Valve.VR.EVRButtonId.k_EButton_Axis0;
			}

			// Get the Vive Controller's axis values
			axis = device.GetAxis (axisMask);
		}
	}

	// Called by the Vive Simulator
	public void Simulate (Vector2 setAxis) {
		
		// Ensure the simulated X axis is valid
		if (setAxis.x < -1.0f) {
			setAxis.x = -1.0f;
		} 
		else if (setAxis.x > 1.0f) {
			setAxis.x = 1.0f;
		}

		// Ensure the simulated Y axis is valid
		if (setAxis.y < -1.0f) {
			setAxis.y = -1.0f;
		} 
		else if (setAxis.y > 1.0f) {
			setAxis.y = 1.0f;
		}

		// Set axis to simulated values
		axis = setAxis;
	}
}