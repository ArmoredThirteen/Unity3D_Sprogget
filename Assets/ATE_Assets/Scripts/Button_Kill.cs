using UnityEngine;
using System.Collections;

public class Button_Kill : Button {
	public Killable killVictim;

	public float killDelay = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void OnTriggerEnter(Collider other) {
		if(!isActive)
			return;
		
		// if it has a rigid body
		if(other.GetComponent("Rigidbody") != null) {
			// if there is no kill delay
			if(killDelay <= 0) {
				// tell it to die
				killVictim.Death();
			}
			// make sure it isn't already dying
			else if (killVictim.isDying == false){
				// set kill time and tell it to die
				killVictim.deathTime = Time.time + killDelay;
				killVictim.isDying = true;
				// activate the dying animation
				killVictim.animateDying();
			}
		}
	}

}
