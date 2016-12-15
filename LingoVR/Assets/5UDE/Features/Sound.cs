using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	// Inspector parameters
	[Tooltip("The sound clip you want to play.")]
	public AudioClip audioClip;

	[Tooltip("Play the sound.")]
	public bool play = true;

	[Tooltip("Loop the sound.")]
	public bool loop = false;

	[Tooltip("Mute the sound.")]
	public bool mute = false;

	[Tooltip("Play the sound after delaying a number of seconds.")]
	public float delay = 0.0f;

	[Tooltip("The volume of the sound. Default is 1.0 or 100%.")]
	public float volume = 1.0f;

	[Tooltip("The minimum distance before the sound begins to fade.")]
	public float minDistance = 5.0f;

	[Tooltip("The maximum distance that the sound can be heard from.")]
	public float maxDistance = 500.0f;

	// Private variables
	protected AudioSource audioSource;

	// Called at the start of the program initialization
	void Awake () {

		// Add an audiosource to this gameobject
		audioSource = gameObject.AddComponent<AudioSource> ();
	}


	// Called at the end of the program initialization
	void Start () {

		// Call the update function to handle setting up the sound
		Update ();
	}

	// Called every graphical frame
	void Update () {

		// Set the audiosource's clip if it has changed
		if (audioSource.clip != audioClip) {
			audioSource.clip = audioClip;
		}

		// Set the audiosource's loop setting
		audioSource.loop = loop;
		// Set the audiosource's volume
		audioSource.volume = volume;
		// Set the audiosource's minimum distance
		audioSource.minDistance = minDistance;
		// Set the audiosource's maximum distance
		audioSource.maxDistance = maxDistance;
		// Set the audiosource's blend setting to 3D
		audioSource.spatialBlend = 1.0f;

		// Reduce volume to 0 if the audiosource is muted
		if (mute) {
			audioSource.volume = 0.0f;
		}

		// Play the sound when specified
		if (play) {
			play = false;
			audioSource.PlayDelayed (delay);
		}
	}
}
