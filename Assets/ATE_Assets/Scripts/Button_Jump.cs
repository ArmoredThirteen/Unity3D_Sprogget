using UnityEngine;
using System.Collections;

public class Button_Jump : Button {
	public float flingVelocity = 10.0f;
	public Transform flingDirection;
	public int uses = -1; // set negative for infinite


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void OnTriggerEnter(Collider other) {
		if(!isActive || uses == 0)
			return;

		// if it has a rigid body
		if(other.GetComponent("Rigidbody") != null) {
			// get the direction vector
			Vector3 theDir = Vector3.Normalize(flingDirection.localPosition) * flingVelocity;

			// apply the new velocity
			other.rigidbody.velocity = theDir;

			// change uses
			if(uses > 0)
				uses -= 1;
		}
	}

}
