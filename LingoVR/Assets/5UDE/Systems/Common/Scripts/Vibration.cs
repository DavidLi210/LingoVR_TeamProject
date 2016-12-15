using UnityEngine;
using System.Collections;

public class Vibration : MonoBehaviour {
	
    // Reveal vibration info in the Inspector
    protected bool vibrate;
    protected float intensity;
    protected float duration;

    // Called at the end of the program initialization
	protected virtual void Start () {
		
        // Create initial vibration values
        vibrate = false;
        intensity = 0.5f;
        duration = 0.0f;
    }

    // Called every graphical frame
	protected virtual void Update () {
		
        // Determine if vibration is on and some duration exists
		if (vibrate && duration > 0.0f) {
			
            // Vibrate at current intensity
            Vibrate (intensity);
            // Reduce duration
            duration -= Time.deltaTime;
        }
        // Vibration is off or duration is up
		else {
			
            // Ensure vibration is off
            vibrate = false;
            // Ensure duration is zero
            duration = 0.0f;
        }
	}

	// Public function to retrieve the vibration's current state
	public virtual bool GetVibration () {

		// Ensure vibration is updated first
		Update ();
		// Return the vibration
		return vibrate;
	}

    // Public function to turn on vibration motor for current frame
	public virtual void Vibrate (float vibrationIntensity) {
		
        // Turn vibration motor on for one frame
    }

    // Public function to turn and keep on vibration motor
	public virtual void VibrateOn (float vibrationIntensity = 0.5f, float seconds = 1.0f) {
		
        // Turn vibration on
        vibrate = true;
        // Set intensity
        intensity = vibrationIntensity;
        // Set duration
        duration = seconds;
        // Call Update to start vibration
        Update ();
    }

    // Public function to turn and keep off vibration motor
	public virtual void VibrateOff () {
		
        // Turn vibration off
        vibrate = false;
        // End duration
        duration = 0.0f;
        // Call Update to end vibration
        Update ();
    }
}