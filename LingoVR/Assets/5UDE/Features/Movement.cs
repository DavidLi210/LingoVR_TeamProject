using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Inspector parameters
	public Route route;
	public bool play = true;
	public bool loop = false;
	public float speed = 1.0f;

	// Private variables
	private int target = 0;
	
	// Called every graphical frame
	void Update () {

		// If movement is playing and there's a valid route
		if (play && route.Length () > 0) {

			// Determine the object's distance to the target position in the route
			float distance = Vector3.Distance (transform.position, route.points [target].position);
			// Determine the object's direction to the target position in the route
			Vector3 direction = Vector3.Normalize (route.points [target].position - transform.position);

			// Move the object toward the target if speed is less than distance
			if ((speed * Time.deltaTime) < distance) {
				transform.position += direction * speed * Time.deltaTime;
			}

			// Otherwise move the object to the target and update target
			else {
				transform.position = route.points [target].position;
				// Move to next target
				target++;

				// Reset target if end is passed
				if (target >= route.Length ()) {
					target = 0;

					// Stop playing if not looping
					if (!loop) {
						play = false;
					}
				}
			}
		}
	}
}
