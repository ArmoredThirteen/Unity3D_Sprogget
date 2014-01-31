using UnityEngine;
using System.Collections;

public class SceneData_Player : MonoBehaviour {
	public int maxHealth = 1;
	public int lives = 5;
	public int points = 0;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
