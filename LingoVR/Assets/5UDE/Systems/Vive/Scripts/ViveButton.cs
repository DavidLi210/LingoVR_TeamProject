using UnityEngine;
using System.Collections;

public class ViveButton : Button {
	
    // Enumerate Vive button names
	public enum ViveButtonName {
        Grip,
        Menu,
        System,
        Touchpad,
        Trigger
    };

    // Reveal button name in the Inspector
    public ViveButtonName buttonName;

    // Private variables
    SteamVR_TrackedObject trackedObject = null;
	bool simulatedPress;

    // Called at the end of the program initialization
    protected override void Start () {
		
		// Get the Vive Controller from parent
		trackedObject = GetComponentInParent<SteamVR_TrackedObject> ();
		// Track simulated press events
		simulatedPress = false;
    }

    // Called every graphical frame
    protected override void Update ()
	{
		// Check that tracked object is valid
		if (trackedObject != null && trackedObject.isActiveAndEnabled) {
			
			// Get the Vive Controller's input
			var device = SteamVR_Controller.Input ((int)trackedObject.index);
			// Determine which button to check
			ulong buttonMask;

			// Get grip button
			if (buttonName == ViveButtonName.Grip) {
				buttonMask = SteamVR_Controller.ButtonMask.Grip;
			}

			// Get menu button
			else if (buttonName == ViveButtonName.Menu) {
				buttonMask = SteamVR_Controller.ButtonMask.ApplicationMenu;
			}

			// Get system button
			else if (buttonName == ViveButtonName.System) {
				buttonMask = SteamVR_Controller.ButtonMask.System;
			}

			// Get touchpad button
			else if (buttonName == ViveButtonName.Touchpad) {
				buttonMask = SteamVR_Controller.ButtonMask.Touchpad;
			}

			// Get trigger button by default
			else {
				buttonMask = SteamVR_Controller.ButtonMask.Trigger;
			}

			// Get the Vive Controller's button touch state
			touch = device.GetTouch (buttonMask);
			// Get the Vive Controller's button touch down state
			touchDown = device.GetTouchDown (buttonMask);
			// Get the Vive Controller's button touch up state
			touchUp = device.GetTouchUp (buttonMask);
			// Get the Vive Controller's button press state
			press = device.GetPress (buttonMask);
			// Get the Vive Controller's button press down state
			pressDown = device.GetPressDown (buttonMask);
			// Get the Vive Controller's button press up state
			pressUp = device.GetPressUp (buttonMask);
		}
	}

	// Called by the Vive Simulator to simulate pressing button
	public void simulatePressDown () {
		
		// If press state has changed
		if (!simulatedPress) {
			
			// Down states are true
			touchDown = pressDown = true;
		}

		// Touch and press states are true
		touch = press = true;
		// Simulated press state is true
		simulatedPress = true;
		// Up states are false
		touchUp = pressUp = false;
	}

	// Called by the Vive Simulator to simulate releasing button
	public void simulatePressUp () {
		
		// If press state has changed
		if (simulatedPress) {
			
			// Up states are true
			touchUp = pressUp = true;
		}

		// Touch and press states are false
		touch = press = false;
		// Simulated press state is false
		simulatedPress = false;
		// Down states are false
		touchDown = pressDown = false;
	}
}