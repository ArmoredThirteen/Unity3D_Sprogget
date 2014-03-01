using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerData))]

public class DisplayLives : MonoBehaviour {
	public Texture2D livesImage;
	private PlayerData playerData;


	// Use this for initialization
	void Start () {
		// grab needed player data
		playerData = gameObject.GetComponent ("PlayerData") as PlayerData;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnGUI () {
		// log and bail if it has no image
		if (!livesImage) {
			Debug.LogError ("Cannot find DisplayLives.cs texture.");
			return;
		}

		// get the number of lives
		int livesCount = playerData.curLives;
		//livesCount = 100;

		// get different spacing data
		int livesImageLeft = 35;
		int livesImageWidth = livesImage.width / 2;
		int livesImageHeight = livesImage.height / 2;
		int livesCountLeft = livesImageLeft + livesImageWidth + 10;

		// make image and text label
		GUI.Label (new Rect (livesImageLeft, 15, livesImageWidth, livesImageHeight), livesImage);
		GUI.Label (new Rect (livesCountLeft, 30, 125, livesImageHeight), "<color=black><size=50> x" + livesCount + "</size></color>");

		// lives image is in top left, currently at 1/2 image size
		//GUI.DrawTexture (new Rect (20, 15, 50, 65), livesImage, ScaleMode.StretchToFill);
		// uses rich text to make it larger
		//GUI.Label (new Rect (70, 30, 50, 100), "<color=black><size=50> x" + playerData.lives + "</size></color>");
	}


}
