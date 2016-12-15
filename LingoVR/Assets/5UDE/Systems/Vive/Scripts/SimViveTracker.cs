using UnityEngine;
using System.Collections;

public class SimViveTracker : MonoBehaviour {

	// Tracker to simulate
	[HideInInspector]
	public ViveTracker viveTracker;

	// Private variable
	bool valid = true;

	// Called every graphical frame
	void Update () {

		// Validate the tracker
		ValidateTracker ();
		// Simulate the tracker
		viveTracker.Simulate(valid, transform.localPosition, transform.localRotation);
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
