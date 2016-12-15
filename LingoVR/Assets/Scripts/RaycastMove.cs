using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaycastMove : MonoBehaviour {

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
		state = SteeringState.Neutral;
	}
		
    // FixedUpdate is not called every graphical frame but rather every physics frame
	void FixedUpdate () {

		if (!button.GetPress ()) {
			state = SteeringState.Neutral;
		}

		// If state is not steering
		if (state == SteeringState.Neutral) {

			// If the touchpad is pressed
			if (button.GetPress ()) {
				// Change state to steering forward
				var controller = GameObject.Find ("Real World Controller (left)").GetComponent<SimViveController>();
				controller.touchpadButton = false;
				state = SteeringState.Casting;
			}
		}

		// 
		else if (state == SteeringState.Casting) {
			print ("Casting");

			Vector3 origin, destination;
			var vrTracker = GameObject.Find ("Vive Controller (left)");
			origin = vrTracker.transform.position;
			destination = tracker.transform.forward;
			target = space.transform.position;

			RaycastHit hit;
			if (Physics.Raycast (origin, destination, out hit)) {
				target = hit.point;
				state = SteeringState.Moving;
			}
		}

		else if (state == SteeringState.Moving) {
			if (button.GetPressDown ()) {
				print ("Moving");
				print (target);
				print (space.transform.position);
				target.y = 0;
				if (Vector3.Distance (space.transform.position, target) > 1.0f) {
					var origin = space.transform.position;
					space.transform.position = Vector3.Lerp (origin, target, speed * Time.deltaTime);
				} else {
					state = SteeringState.Neutral;
				}
			} else {
				state = SteeringState.Neutral;
			}
		}
	}

}