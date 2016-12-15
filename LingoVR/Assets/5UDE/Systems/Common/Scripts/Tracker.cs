using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour 
{
	// Reveal validity of tracking info in the Inspector
    protected bool valid;

    // Called at the end of the program initialization
	protected virtual void Start () {
		
		// Create initial tracking info
        valid = false;
	}

    // Called every graphical frame
	protected virtual void Update () {
		
		// Update virtual and physical tracking info here
	}

	// Public function to retrieve the tracker's position in the virtual environment
	public virtual Vector3 GetVirtualPosition () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.position;
	}

	// Public function to retrieve the tracker's rotation in the virtual environment
	public virtual Quaternion GetVirtualRotation () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.rotation;
	}	

	// Public function to retrieve the tracker's position in the physical tracking space
	public virtual Vector3 GetPhysicalPosition () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.localPosition;
	}

	// Public function to retrieve the tracker's rotation in the physical tracking space
	public virtual Quaternion GetPhysicalRotation () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.localRotation;
	}	

    // Public function to retrieve the tracker's validity
	public virtual bool IsValid () {
		
        // Ensure value is updated first
        Update ();
        // Return the value
        return valid;
    }
}