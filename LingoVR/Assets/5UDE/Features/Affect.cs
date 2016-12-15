using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Public enumeration of the types of affects
public enum AffectType {
	Virtual,
	Physical
};

public class Affect : Element {

	// Members available in the Inspector
	public AffectType type;
	public bool interactivesOnly = true;

	// Trigger-related members
	public bool triggerEntered { get; protected set; }
	public bool triggerOngoing { get; protected set; }
	public bool triggerExited { get; protected set; }
	public List<Collider> enteredTriggers { get; protected set; }
	public List<Collider> ongoingTriggers { get; protected set; }
	public List<Collider> exitedTriggers { get; protected set; }

	// Collision-related members
	public bool collisionEntered { get; protected set; }
	public bool collisionOngoing { get; protected set; }
	public bool collisionExited { get; protected set; }
	public List<Collision> enteredCollisions { get; protected set; }
	public List<Collision> ongoingCollisions { get; protected set; }
	public List<Collision> exitedCollisions { get; protected set; }

	// Private state-related variables
	protected bool onCollision;
	protected bool onTrigger;

	// Called at the end of the program initialization
	protected override void Start () {

		// Ensure physics do not control the rigidbody
		rigidbody.isKinematic = true;

		// Create collision states
		onCollision = collisionEntered = collisionOngoing = collisionExited = false;
		// Create collision lists
		enteredCollisions = new List<Collision> ();
		ongoingCollisions = new List<Collision> ();
		exitedCollisions = new List<Collision> ();

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

		// If the affect is virtual and passes through objects
		if (type == AffectType.Virtual) {

			// Ensure physics do not control the rigidbody
			rigidbody.isKinematic = true;

			// Ensure all the colliders are set for triggers
			Collider[] colliders = gameObject.GetComponents<Collider> ();
			for (int i = 0; i < colliders.Length; i++) {
				colliders [i].isTrigger = true;
			}
		}		
		// If the affect is physical and creates collisions with objects
		else if (type == AffectType.Physical) {

			// Ensure physics do not control the rigidbody
			rigidbody.isKinematic = true;

			// Ensure all the colliders are set for collisions
			Collider[] colliders = gameObject.GetComponents<Collider> ();
			for (int i = 0; i < colliders.Length; i++) {
				colliders [i].isTrigger = false;
			}
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
			// Avoid non-interactives unless not required
			if (trigger.gameObject.GetComponent<Interactive> () != null || interactivesOnly == false) {
				// Update the current state value
				triggerEntered = true;
				// Keep track of the current trigger
				enteredTriggers.Add (trigger);
			}
		}
	}

	// Called once per frame for every collider touching another collider
	protected virtual void OnTriggerStay (Collider trigger) {

		// Update all the states
		OnTriggerUpdate ();

		// Avoid self triggering
		if (trigger.gameObject != gameObject) {
			// Avoid non-interactives unless not required
			if (trigger.gameObject.GetComponent<Interactive> () != null || interactivesOnly == false) {
				// Update the current state value
				triggerOngoing = true;
				// Keep track of the current trigger
				ongoingTriggers.Add (trigger);
			}
		}
	}

	// Called when a collider has stopped touching another collider
	protected virtual void OnTriggerExit (Collider trigger) {

		// Update all the states
		OnTriggerUpdate ();

		// Avoid self triggering
		if (trigger.gameObject != gameObject) {
			// Avoid non-interactives unless not required
			if (trigger.gameObject.GetComponent<Interactive> () != null || interactivesOnly == false) {
				// Update the current state value
				triggerExited = true;
				// Keep track of the current trigger
				exitedTriggers.Add (trigger);
			}
		}
	}
		
	// Called first by every OnCollision function
	protected virtual void OnCollisionUpdate () {

		// If an OnCollision function has not already been called this physics frame
		if (!onCollision) {
			// One has now been called
			onCollision = true;
			// Reset collision states
			collisionEntered = collisionOngoing = collisionExited = false;
			// Clear previous collisions entered
			enteredCollisions.Clear();
			// Clear previous collisions ongoing
			ongoingCollisions.Clear();
			// Clear previous collisions exited
			exitedCollisions.Clear();
		}
	}

	// Called when a collider has begun touching another collider
	protected virtual void OnCollisionEnter (Collision collision) {

		// Update all the states
		OnCollisionUpdate ();
		// Update the current state value
		collisionEntered = true;
		// Keep track of the current collision
		enteredCollisions.Add(collision);
	}

	// Called once per frame for every collider touching another collider
	protected virtual void OnCollisionStay (Collision collision) {

		// Update all the states
		OnCollisionUpdate ();
		// Update the current state value
		collisionOngoing = true;
		// Keep track of the current collision
		ongoingCollisions.Add(collision);
	}

	// Called when a collider has stopped touching another collider
	protected virtual void OnCollisionExit (Collision collision) {

		// Update all the states
		OnCollisionUpdate ();
		// Update the current state value
		collisionExited = true;
		// Keep track of the current collision
		exitedCollisions.Add(collision);
	}
}
