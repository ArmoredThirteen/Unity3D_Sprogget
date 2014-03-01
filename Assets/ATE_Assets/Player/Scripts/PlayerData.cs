using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
	// required scene data
	private SceneData_Player theSceneData;

	// variables to use
	[System.NonSerialized]
	public int
		curHealth;
	[System.NonSerialized]
	public int
		maxHealth;
	[System.NonSerialized]
	public int
		curLives;
	[System.NonSerialized]
	public int
		maxLives;
	[System.NonSerialized]
	public int
		points;

	// Use this for initialization
	void Start() {
		theSceneData = GameObject.FindObjectOfType(typeof(SceneData_Player)) as SceneData_Player;

		// bail if no scene data
		if(theSceneData == null) {
			Debug.Log("Player object has no specified scene data!");
			return;
		}

		// assign the variables from the scene data
		curHealth = theSceneData.curHealth;
		maxHealth = theSceneData.maxHealth;
		curLives = theSceneData.curLives;
		maxLives = theSceneData.maxLives;
		points = theSceneData.points;
	}
	
	// Update is called once per frame
	void Update() {
		// make sure the scene data is updated
		theSceneData.curHealth = curHealth;
		theSceneData.curLives = curLives;
		theSceneData.points = points;
	}


	// removes lives
	public void RemoveLives(int amount) {
		// remove the lives
		curLives -= amount;
		
		// if you died
		if(curLives < 0) {
			// do death stuff
			return;
		}
	}

	public void Addlives(int amount) {
		// remove the lives
		curLives += amount;
		
		// if you died
		if(curLives > maxLives) {
			curLives = maxLives;
		}
	}

}
