using UnityEngine;
using System.Collections;

public class SceneData_BindCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		// exit condition for locked cursor
		if (Input.GetKeyDown ("escape"))
			Screen.lockCursor = false;
	}


	void OnMouseDown () {
		Screen.lockCursor = true;
	}
}
