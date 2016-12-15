using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleMove : MonoBehaviour {

	// Enumerate the states of steering
	public enum SteeringState {
		Neutral,
		Casting,
		Moving
	};

	// Inspector parameters
	[Tooltip("The tracking device used to determine absolute direction for steering.")]
	public Tracker tracker;

	[Tooltip("A button required to be pressed to activate steering.")]
	public Button button;  

	[Tooltip("The space that is translated by this interaction. Usually set to the physical tracking space.")]
	public Space space;

	[Tooltip("The median speed for movement expressed in meters per second.")]
	public float speed = 1.0f;

	private Vector3 target;

	// Private interaction variables
	private SteeringState state;

	// Called at the end of the program initialization
	void Start () {
		// Set initial steering state to not steering
	}

	// FixedUpdate is not called every graphical frame but rather every physics frame
	void FixedUpdate () {

		// If the touchpad is pressed
		if (button.GetPress ()) {
			// Change state to steering forward
			var controller = GameObject.Find ("Real World Controller (left)").GetComponent<SimViveController> ();
			controller.touchpadButton = false;
			Vector3 origin, destination;
			var vrTracker = GameObject.Find ("Vive Controller (left)");
			origin = vrTracker.transform.position;
			destination = tracker.transform.forward;
			var spaceOrigin = space.transform.position;
			Vector3 dir = destination - origin;
			space.transform.position.Set(spaceOrigin.x + dir.x * 0.1f, spaceOrigin.y, spaceOrigin.z + dir.z * 0.1f);
		}
	}
}