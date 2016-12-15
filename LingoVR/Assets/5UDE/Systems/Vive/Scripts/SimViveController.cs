using UnityEngine;
using System.Collections;

public class SimViveController : MonoBehaviour {

	// Simulate controller buttons and axes
	public bool triggerButton = false;
	private Vector2 triggerAxis = new Vector2 ();
	public bool touchpadButton = false;
	public Vector2 touchpadAxis = new Vector2 ();
	public bool gripButton = false;
	public bool menuButton = false;
	public bool systemButton = false;
	public bool vibrating = false;

	// Vive components to simulate
	[HideInInspector] 
	public ViveTracker viveTracker;
	[HideInInspector]
	public ViveButton viveTriggerButton, viveTouchpadButton, viveGripButton, viveMenuButton, viveSystemButton;
	[HideInInspector]
	public ViveAxis viveTriggerAxis, viveTouchpadAxis;
	[HideInInspector]
	public ViveVibration viveVibration;

	// Private variables
	bool valid = true;

	// Called every graphical frame
	void Update () {

		// Validate the tracker
		ValidateTracker ();
		// Simulate the tracker
		viveTracker.Simulate(valid, transform.localPosition, transform.localRotation);

		// Simulate the trigger button
		if (triggerButton) {
			viveTriggerButton.simulatePressDown ();
		} 
		else {
			viveTriggerButton.simulatePressUp ();
		}

		// Simulate the trigger axis
		if (triggerButton) {
			triggerAxis.x = 1.0f;
			viveTriggerAxis.Simulate(triggerAxis);
		}
		else {
			triggerAxis.x = 0.0f;
			viveTriggerAxis.Simulate(triggerAxis);
		}
			
		// Simulate the touchpad button
		if (touchpadButton) {
			viveTouchpadButton.simulatePressDown ();
		} 
		else {
			viveTouchpadButton.simulatePressUp ();
		}

		// Simulate the touchpad axis
		viveTouchpadAxis.Simulate(touchpadAxis);
			
		// Simulate the grip button
		if (gripButton) {
			viveGripButton.simulatePressDown ();
		} 
		else {
			viveGripButton.simulatePressUp ();
		}
			
		// Simulate the menu button
		if (menuButton) {
			viveMenuButton.simulatePressDown ();
		} 
		else {
			viveMenuButton.simulatePressUp ();
		}
			
		// Simulate the system button
		if (systemButton) {
			viveSystemButton.simulatePressDown ();
		} 
		else {
			viveSystemButton.simulatePressUp ();
		}

		// Simulate any vibrations
		if (vibrating) {
			vibrating = false;
		} 
		else {
			vibrating = viveVibration.GetVibration ();
		}
	}

	// Check that the tracker is within the expected physical range
	void ValidateTracker () {

		// Validate physical tracked position
		Vector3 physicalPosition = transform.localPosition;

		// Invalid if out of lateral range
		if (physicalPosition.x < -2.5f || 2.5f < physicalPosition.x) {
			valid = false;
		}
		// Invalid if out of longitudinal range
		else if (physicalPosition.z < -2.5f || 2.5f < physicalPosition.z) {
			valid = false;
		}
		// Otherwise validate within range and fix camera
		else {
			valid = true;
		}
	}
}
