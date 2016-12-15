using UnityEngine;
using System.Collections;

public class SimVive : MonoBehaviour {

	// Called at the end of the program initialization
	void Start () {

		// Get transform for Vive input devices
		Transform devices = GameObject.Find ("Vive Input").transform;

		// Turn on anything turned off by SteamVR 
		for (int i = 0; i < devices.childCount; i++) {
			devices.GetChild (i).gameObject.SetActive (true);
		}

		// Turn off all SteamVR components
		GameObject.Find ("Vive Input").GetComponent<SteamVR_ControllerManager> ().enabled = false;
		GameObject.Find ("Vive Controller (left)").GetComponent<SteamVR_TrackedObject> ().enabled = false;
		GameObject.Find ("Vive Controller (right)").GetComponent<SteamVR_TrackedObject> ().enabled = false;
		GameObject.Find ("Vive HMD").GetComponent<SteamVR_TrackedObject> ().enabled = false;
		GameObject.Find ("Vive Camera").GetComponent<SteamVR_Camera> ().enabled = false;
		GameObject.Find ("Vive Listener").GetComponent<SteamVR_Ears> ().enabled = false;
	}
}
