using UnityEngine;
using System.Collections;

public class TriggerArea_RemoveLives : MonoBehaviour {
	public int removalCount = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter (Collider collider) {
		// get potential player data
		PlayerData playerData = collider.gameObject.GetComponent ("PlayerData") as PlayerData;

		// if it wasn't null
		playerData.RemoveLives (removalCount);
	}
}
