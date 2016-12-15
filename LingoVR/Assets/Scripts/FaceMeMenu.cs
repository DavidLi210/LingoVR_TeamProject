using UnityEngine;
using System.Collections;

public class FaceMeMenu : MonoBehaviour {
	public GameObject vive = null;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate() {
		RotateTo();
	}
	public void RotateTo()
	{
		if (vive!=null) {
			float current = this.transform.eulerAngles.y;
			this.transform.LookAt(vive.transform);
			float target = this.transform.eulerAngles.y;
			float next = Mathf.MoveTowardsAngle(current, target+180, Time.deltaTime * 120);
			this.transform.eulerAngles = new Vector3(0, next, 0);
		}

	}
}
