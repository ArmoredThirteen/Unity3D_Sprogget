using UnityEngine;
using System.Collections;

public class SceneData_Player : MonoBehaviour {
	public int curHealth = 1;
	public int maxHealth = 1;
	public int curLives = 5;
	public int maxLives = 100;
	public int points = 0;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
