using UnityEngine;
using System.Collections;

public class LeverIntroPuzzle : Puzzle {

	public static LeverIntroPuzzle S;
	public GameObject lever;
    public GameObject Beacon;
    public Color Green;
	private bool leverState = false;

	void Awake(){
		S = this;
	}

	public void HandleInput(){
		if (leverState) {
			solved = false;
		} else {
			solved = true;
            Beacon.GetComponent<Light>().color = Green;
		}
		leverState = !leverState;
		lever.gameObject.transform.Rotate (0, 0, 90);
	}
}
