using UnityEngine;
using System.Collections;

public class Clickable_NextScene : MonoBehaviour {
	public float maxClickDistance = 5.0f;


	void OnMouseDown() {
		int nextLevel = Application.loadedLevel + 1;
		Application.LoadLevel(nextLevel);
	}
}
