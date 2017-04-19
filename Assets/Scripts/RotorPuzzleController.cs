using UnityEngine;
using System.Collections;

public class RotorPuzzleController : Puzzle {

	public TextMesh[] ui_rotors;

	// Update is called once per frame
	void Update () {
		SetRotors ();
	}

	void SetRotors(){
		for (int i = 0; i < ui_rotors.Length; ++i) {
			ui_rotors [i].text = RotorPuzzle.S.rotors [i].num.ToString ();
		}
	}
}
