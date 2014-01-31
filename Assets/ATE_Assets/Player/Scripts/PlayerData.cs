using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
	public int maxHealth;
	public int lives;
	public int points;

	// Use this for initialization
	void Start () {
		// get the player's scene data object
		GameObject tempData_Object = GameObject.Find("SceneData_Player");
		if(tempData_Object == null) {
			Debug.Log("Scene has no player data!");
		}
		else {
			// get the tempData
			SceneData_Player tempData = (SceneData_Player)tempData_Object.GetComponent("SceneData_Player");
			// assign the variables from tempData
			maxHealth = tempData.maxHealth;
			lives = tempData.lives;
			points = tempData.points;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
