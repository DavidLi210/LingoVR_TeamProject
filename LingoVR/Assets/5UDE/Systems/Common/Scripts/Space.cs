using UnityEngine;
using System.Collections;

public class Space : MonoBehaviour {
	
	// Called every graphical frame
	protected virtual void Update () {
		
		// Update virtual position info here
	}

	// Public function to retrieve the space's position in the virtual environment
	public virtual Vector3 GetVirtualPosition () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.position;
	}

	// Public function to retrieve the space's rotation in the virtual environment
	public virtual Quaternion GetVirtualRotation () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return transform.rotation;
	}	
}