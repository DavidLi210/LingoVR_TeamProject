using UnityEngine;
using System.Collections;

public class Route : MonoBehaviour {

	// Inspector parameter
	public Transform[] points;

	// Return the length of the route in points
	public int Length () {
		return points.Length;
	}
}
