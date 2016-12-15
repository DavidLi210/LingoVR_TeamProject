using UnityEngine;
using System.Collections;

public class Axis : MonoBehaviour {
	
    // Reveal axis values in the Inspector
    protected Vector2 axis;

    // Called at the end of the program initialization
    protected virtual void Start () {
		
		// Create initial axis values
		axis = new Vector2 ();
	}

    // Called every graphical frame
    protected virtual void Update () {
		
        // Update axis values here
    }

    // Public function to retrieve the axis value
    public virtual Vector2 GetAxis () {
		
        // Ensure value is updated first
        Update ();
        // Return the value
        return axis;
    }
}