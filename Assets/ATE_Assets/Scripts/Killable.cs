using UnityEngine;
using System.Collections;

#pragma warning disable 0219 // variable assigned but not used.

public class Killable : MonoBehaviour {
	public string dyingAnimation;
	public GameObject deathEffect;
	//public int killPoints = 0;

	[System.NonSerialized]
	public bool isDying = false;
	[System.NonSerialized]
	public float deathTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if it is dying and past death time
		if(isDying == true && Time.time >= deathTime) {
			Death();
			return;
		}
	}


	// call this to start the dying animation
	public void animateDying() {
		if(dyingAnimation != null) {
			animation.Play(dyingAnimation, PlayMode.StopAll);
		}
	}

	// call this to kill the particular object and their babies
	public void Death() {
		// destroy the object
		GameObject.Destroy(gameObject);

		// spawn the kill effect
		GameObject temp = null;
		if(deathEffect!= null)
			temp = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);

		// give the player points
		// points += killPoints
	}


}
