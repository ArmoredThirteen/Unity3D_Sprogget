using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	protected bool isActive = true;
	

	void ToggleActive() {
		isActive = !isActive;
	}
	
	void ActiveOn() {
		isActive = true;
	}

	void ActiveOff() {
		isActive = false;
	}

}
