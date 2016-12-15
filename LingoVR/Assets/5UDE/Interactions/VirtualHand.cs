using UnityEngine;
using System.Collections;

public class VirtualHand : MonoBehaviour {
	
	// Enumerate states of virtual hand interactions
	public enum VirtualHandState {
		Open,
		Touching,
		Holding
	};

	// Inspector parameters
	[Tooltip("The tracking device used for tracking the real hand.")]
	public Tracker tracker;

	[Tooltip("The interactive used to represent the virtual hand.")]
	public Affect hand;

	[Tooltip("The button required to be pressed to grab objects.")]
	public Button button;

	[Tooltip("The speed amplifier for thrown objects. One unit is physically realistic.")]
	public float speed = 1.0f;

	// Private interaction variables
	VirtualHandState state;
	FixedJoint grasp;

	// Called at the end of the program initialization
	void Start () {

		// Set initial state to open
		state = VirtualHandState.Open;

		// Ensure hand interactive is properly configured
		hand.type = AffectType.Virtual;
	}

	// FixedUpdate is not called every graphical frame but rather every physics frame
	void FixedUpdate ()
	{

		// If state is open
		if (state == VirtualHandState.Open) {
			
			// If the hand is touching something
			if (hand.triggerOngoing) {

				// Change state to touching
				state = VirtualHandState.Touching;
   			}

			// Process current open state
			else {

				// Nothing to do for open
			}
		}

		// If state is touching
		else if (state == VirtualHandState.Touching) {

			// If the hand is not touching something
			if (!hand.triggerOngoing) {

				// Change state to open
				state = VirtualHandState.Open;
			}

			// If the hand is touching something and the button is pressed
			else if (hand.triggerOngoing && button.GetPress ()) {

				// Fetch touched target
				Collider target = hand.ongoingTriggers [0];
				// Create a fixed joint between the hand and the target
				grasp = target.gameObject.AddComponent<FixedJoint> ();
				// Set the connection
				grasp.connectedBody = hand.gameObject.GetComponent<Rigidbody> ();

				// Change state to holding
				state = VirtualHandState.Holding;
			}

			// Process current touching state
			else {

				// Nothing to do for touching
			}
		}

		// If state is holding
		else if (state == VirtualHandState.Holding) {

			// If grasp has been broken
			if (grasp == null) {
				
				// Update state to open
				state = VirtualHandState.Open;
			}
				
			// If button has been released and grasp still exists
			else if (!button.GetPress () && grasp != null) {

				// Get rigidbody of grasped target
				Rigidbody target = grasp.GetComponent<Rigidbody> ();
				// Break grasp
				DestroyImmediate (grasp);

				// Apply physics to target in the event of attempting to throw it
				target.velocity = hand.velocity * speed;
				target.angularVelocity = hand.angularVelocity * speed;

				// Update state to open
				state = VirtualHandState.Open;
			}

			// Process current holding state
			else {

				// Nothing to do for holding
			}
		}
	}
}