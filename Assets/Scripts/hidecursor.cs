using UnityEngine;
using System.Collections;

public class hidecursor : MonoBehaviour {
	bool s = false;
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (s == false) {
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
		if (s == true) {
			Screen.lockCursor = false;
			Cursor.visible = true;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			s = true;
		}
		if (Input.GetKeyDown (KeyCode.F1)) {
			s = false;
		}
	}
}
