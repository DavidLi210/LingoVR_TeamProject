using UnityEngine;
using System.Collections;

public class Ray_Hand : MonoBehaviour {
	
	public enum VirtualHandState {
		Aiming,
		Display,
		Learning,
		Drop
	};

	public enum SelectState{
		NOTHING_SELECTED_TO_DISPLAY,
		SELECT_AND_DISPLAY,
		LEARNING_DISPLAYED_OBJECT,
		FINISH_LEARNING_CLEAR
	};

	public enum HeadsetState{
		POINTING,
		MOVE_AWAY
	}

	// Inspector parameters
	[Tooltip("The tracking device used for tracking the real hand.")]
	public Tracker tracker;

	[Tooltip("The interactive used to represent the virtual hand.")]
	public Affect hand;

	[Tooltip("The button required to be pressed to grab objects.")]

	public Button learningButton;
	public Button displayButton;

	public RaycastHit hit;
	public RaycastHit headRay_hit;
	Ray ray;
	Ray rayFromHMD;
	//object references
	GameObject sonObj = null;
	GameObject displayedObj = null;
	GameObject learningObj = null;

	SelectState selectingState;

	//variables for ray(draw line) in the Game
	private Vector3 pos1;
	public Color c1 = Color.blue;
	public Color c2 = Color.black;
	LineRenderer lineRenderer;

	[Tooltip("The speed amplifier for thrown objects. One unit is physically realistic.")]
	public float speed = 1.0f;

	// Private interaction variables
	VirtualHandState state;
	FixedJoint grasp;

	// Called at the end of the program initialization
	void Start () {

		// Set initial state to aiming
		state = VirtualHandState.Aiming;

		// Ensure hand interactive is properly configured
		hand.type = AffectType.Virtual;

		//Original state of selection
		selectingState = SelectState.NOTHING_SELECTED_TO_DISPLAY;

		//draw ray
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.01F, 0.01F);
		lineRenderer.SetVertexCount(2);
	}

	// FixedUpdate is not called every graphical frame but rather every physics frame
	void FixedUpdate ()
	{
		if (state == VirtualHandState.Aiming) {
			//shoot ray
			ray = new Ray (hand.transform.position, hand.transform.forward);
			Physics.Raycast (ray, out hit, 100);

			//draw ray
			Vector3 pos = hit.point;
			pos1 = hand.transform.position;
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, pos1);
			lineRenderer.SetPosition (1, pos);
			//if the user finish learning or after display the ray hit something else, clean up state
			if (selectingState == SelectState.FINISH_LEARNING_CLEAR ||(displayedObj != null && displayedObj != hit.transform.gameObject)) {
				displayedObj.GetComponent<DisplayManager> ().destroyLabel ();
				displayedObj = null;
				learningObj = null;
				selectingState = SelectState.NOTHING_SELECTED_TO_DISPLAY;
				Debug.Log ("You move away. Nothing to display, nothing to learn.");

			}

			//if hitting legal object and trigger pressed, go to display mode
			if (Physics.Raycast (ray, out hit, 100) && displayButton.GetPress () && hit.transform.gameObject.tag != "unmovable") {
				Debug.DrawRay (hand.transform.position, hand.transform.forward);
				state = VirtualHandState.Display;
				selectingState = SelectState.SELECT_AND_DISPLAY;
				Debug.Log ("Going to Displaying Mode: " + hit.transform.gameObject.name);
			}
			//if displaying a object and grip pressed, go to learning mode
			else if (Physics.Raycast (ray, out hit, 100) && selectingState == SelectState.SELECT_AND_DISPLAY && displayedObj == hit.transform.gameObject && learningButton.GetPress ()) {
				Debug.DrawRay (hand.transform.position, hand.transform.forward);
				state = VirtualHandState.Learning;
				selectingState = SelectState.LEARNING_DISPLAYED_OBJECT;
				//Debug.Log ("Learning " + hit.transform.gameObject.name);
			}
			//else, keep aiming
			else {
				Debug.DrawRay (hand.transform.position, hand.transform.forward);
				state = VirtualHandState.Aiming;
			}

		}
		//display sounds and labels
		else if (state == VirtualHandState.Display) {
			//draw debug line
			ray = new Ray (hand.transform.position, hand.transform.forward);
			Physics.Raycast (ray, out hit, 100);
			Debug.DrawLine (hand.transform.position, hit.point);

			//get the collider of pointed object
			Collider target = hit.collider;

			// nothing selected but selecting is 1, we display objec 
			if (hit.transform.gameObject.tag != "unmovable" && selectingState == SelectState.SELECT_AND_DISPLAY) {
				//show label and sounds=============================================================================================================================================//
				Debug.Log ("Displaying" + hit.transform.gameObject.name);
				//det the display script object  attach to the object

				DisplayManager dis = hit.transform.gameObject.GetComponent ("DisplayManager") as DisplayManager;			
				if (dis) {
					dis.displayLabel ();
				}
					

				
				//record the displayed object
				displayedObj = target.gameObject;
				state = VirtualHandState.Aiming;
				/*command out for preliminary*/
				//sonObj = Instantiate (selectedObj);
				//sonObj.transform.localScale = new Vector3 (sonObj.transform.localScale.x/3, sonObj.transform.localScale.y/3, sonObj.transform.localScale.z/3);
				//Interactive script = sonObj.transform.GetComponent ("Interactive") as Interactive;
				//Destroy (script);
				//Destroy (sonObj.transform.GetComponent<Rigidbody> ());
				//sonObj.transform.position = new Vector3 (0f,0f,1.0f);
				//sonObj.transform.SetParent(Camera.main.transform, false);
				//grasp = target.gameObject.AddComponent<FixedJoint> ();				
				// do not set the connection
				//grasp.connectedBody = hand.gameObject.GetComponent<Rigidbody> ();
			}         	
		} else if (state == VirtualHandState.Learning) {
			//shoot ray check what is hit
			ray = new Ray (hand.transform.position, hand.transform.forward);

			Physics.Raycast (ray, out hit, 100);
			Collider target = hit.collider;
			Vector3 pos = hit.point;
			pos1 = hand.transform.position;
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, pos1);
			lineRenderer.SetPosition (1, pos);

			if(learningObj == null){
				
				learningObj = target.transform.gameObject;
			}
			//get into learning mode
			if (learningObj == displayedObj) {
				Debug.Log ("Learning " + learningObj.name);
				DisplayManager dis = learningObj.GetComponent<DisplayManager> () as DisplayManager;
				//hide the label
				dis.destroyLabel ();
				//open up the menue
				dis.displayMenu();

				rayFromHMD = new Ray (tracker.transform.position, tracker.transform.forward);
				Physics.Raycast (rayFromHMD, out headRay_hit, 100);
				Debug.DrawLine (tracker.transform.position, hit.point);

				if(displayButton.GetPress()){
					Physics.Raycast (ray, out hit, 100);
					target = hit.collider;

					switch (target.name) {
					case "Audio1Icon":
						dis.playExample (0);
						break;
					case "Audio2Icon":
						dis.playExample (1);
						break;
					case "Audio3Icon":
						dis.playExample (2);
						break;				
					}
					//Debug.Log ("AAA"+(headRay_hit.transform.gameObject.name));
					//if the X is pushed or user move away
					if(target.name == "Exit"){
						learningObj.GetComponent<DisplayManager> ().destroyMenu();
						selectingState = SelectState.FINISH_LEARNING_CLEAR;
						state = VirtualHandState.Aiming;
					}
				}

				//if(headRay_hit.transform.gameObject.name != ("Menu"+learningObj.name))
				//	state = VirtualHandState.Aiming;
			} else {
				state = VirtualHandState.Aiming;
			}
		}	
		else if (state == VirtualHandState.Drop) {
			DestroyImmediate(sonObj);
			DestroyImmediate (grasp);
			lineRenderer.enabled = false;
			state = VirtualHandState.Aiming;
		}			
	}
}