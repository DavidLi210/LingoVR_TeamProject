using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : Element {

	// Trigger-related members
	public bool triggerEntered { get; protected set; }
	public bool triggerOngoing { get; protected set; }
	public bool triggerExited { get; protected set; }
	public List<Collider> enteredTriggers { get; protected set; }
	public List<Collider> ongoingTriggers { get; protected set; }
	public List<Collider> exitedTriggers { get; protected set; }

	// Private state-related variables
	protected bool onTrigger;

	// Called at the end of the program initialization
	protected override void Start () {

		// Create trigger states
		onTrigger = triggerEntered = triggerOngoing = triggerExited = false;
		// Create trigger lists
		enteredTriggers = new List<Collider> ();
		ongoingTriggers = new List<Collider> ();
		exitedTriggers = new List<Collider> ();

		// Call update to set the appropriate settings
		Update ();
	}

	// Updates the behaviors of the element's rigidbody and colliders
	protected override void UpdateBehaviors () {

		// Ensure physics do not control the rigidbody
		rigidbody.isKinematic = true;

		// Ensure all the colliders are set for triggers
		Collider[] colliders = gameObject.GetComponents<Collider> ();
		for (int i = 0; i < colliders.Length; i++) {
			colliders [i].isTrigger = true;
		}
	}

	// FixedUpdate is not called every graphical frame but rather every physics frame
	protected virtual void FixedUpdate () {

		// OnTrigger state has not been reset yet because FixedUpdate occurs first
		onTrigger = false;
	}
		
	// Called first by every OnTrigger function
	protected virtual void OnTriggerUpdate () {

		// If an OnTrigger function has not already been called this physics frame
		if (!onTrigger) {
			// One has now been called
			onTrigger = true;
			// Reset Trigger states
			triggerEntered = triggerOngoing = triggerExited = false;
			// Clear previous triggers entered
			enteredTriggers.Clear();
			// Clear previous triggers ongoing
			ongoingTriggers.Clear();
			// Clear previous triggers exited
			exitedTriggers.Clear();
		}
	}

	// Called when a collider has begun touching another collider
	protected virtual void OnTriggerEnter (Collider trigger) {

		// Update all the states
		OnTriggerUpdate ();

		// Avoid self triggering
		if (trigger.gameObject != gameObject) {
			// Update the current state value
			triggerEntered = true;
			// Keep track of the current trigger
			enteredTriggers.Add (trigger);
		}
	}

	// Called once per frame for every collider touching another collider
	protected virtual void OnTriggerStay (Collider trigger) {

		// Update all the states
		OnTriggerUpdate ();

		// Avoid self triggering
		if (trigger.gameObject != gameObject) {
			// Update the current state value
			triggerOngoing = true;
			// Keep track of the current trigger
			ongoingTriggers.Add (trigger);
		}
	}

	// Called when a collider has stopped touching another collider
	protected virtual void OnTriggerExit (Collider trigger) {

		// Update all the states
		OnTriggerUpdate ();

		// Avoid self triggering
		if (trigger.gameObject != gameObject) {
			// Update the current state value
			triggerExited = true;
			// Keep track of the current trigger
			exitedTriggers.Add (trigger);
		}
	}
}
