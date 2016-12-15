using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	// Reveal button states in the Inspector
	protected bool touch;
    protected bool touchDown;
    protected bool touchUp;
    protected bool press;
    protected bool pressDown;
    protected bool pressUp;

    // Called at the end of the program initialization
    protected virtual void Start () {
		
        // Create initial states
        touch = touchDown = touchUp = press = pressDown = pressUp = false;
	}

    // Called every graphical frame
	protected virtual void Update () {
		
		// Update button states here
	}

	// Public function to retrieve the button's current touch state
	public virtual bool GetTouch () {

		// Ensure value is updated first
		Update ();
		// Return the value
		return touch;
	}

	// Public function to retrieve whether the button was touched this frame
	public virtual bool GetTouchDown () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return touchDown;
	}

	// Public function to retrieve whether the button was no longer touched this frame
	public virtual bool GetTouchUp () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return touchUp;
	}

	// Public function to retrieve the button's current press state
	public virtual bool GetPress () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return press;
	}

	// Public function to retrieve whether the button was pressed this frame
	public virtual bool GetPressDown () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return pressDown;
	}

	// Public function to retrieve whether the button was no longer pressed this frame
	public virtual bool GetPressUp () {
		
		// Ensure value is updated first
		Update ();
		// Return the value
		return pressUp;
	}
}