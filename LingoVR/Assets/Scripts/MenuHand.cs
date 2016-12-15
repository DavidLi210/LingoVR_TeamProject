using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHand : MonoBehaviour {
	
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

	public AudioSource player;

	// Private interaction variables
	VirtualHandState state;
	FixedJoint grasp;

	bool played = false;

	// Called at the end of the program initialization
	void Start () {

		// Set initial state to open
		state = VirtualHandState.Open;

		// Ensure hand interactive is properly configured
		hand.type = AffectType.Virtual;


	}

	// FixedUpdate is not called every graphical frame but rather every physics frame
	void Update ()
	{
        var test = button.GetPress();
        print(test);
		if (button.GetPress())
        {
			if (!played) {
				played = true;
				player.Play ();
			}
			Invoke ("NextScene", 1.3f);
        }
	}

	void NextScene() {
		SceneManager.LoadScene ("Bedroom", LoadSceneMode.Single);
	}
}