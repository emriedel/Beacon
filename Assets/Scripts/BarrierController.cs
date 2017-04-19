using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {

	public GameObject[] lights;
	public BitArray lightsInfo;
	public Puzzle[] puzzles;
	public Material offMat;
	public Material onMat;

	public GameObject Door;

	private int light_index = 0;
	private bool isOpen = false;

	private BarrierInterpolator BI;

	// Use this for initialization
	void Start () {
		BI = new BarrierInterpolator ();
		lightsInfo = new BitArray (lights.Length, false);
		foreach (GameObject light in lights) {
			light.gameObject.GetComponent<Renderer> ().material = offMat;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check to see if any new puzzles have been solved
		for (int i = 0; i < puzzles.Length; ++i) {
			if (puzzles [i].solved) {
				lights [i].gameObject.GetComponent<Renderer> ().material = onMat;
				lightsInfo [i] = true;
			} else {
				lights [i].gameObject.GetComponent<Renderer> ().material = offMat;
				lightsInfo [i] = false;
			}
		}

		if (AllLightsOn ()) {
			isOpen = true;
		}
	}

	bool AllLightsOn(){
		for (int i = 0; i < lightsInfo.Length; ++i) {
			if (!lightsInfo [i]) {
				return false;
			}
		}
		return true;
	}

	//Door opens automatically when player is close
	void OnTriggerEnter(Collider col){
		if (isOpen && col.gameObject.tag == "Player") {
			Door.GetComponent<BarrierInterpolator>().ready = true;
		}
	}

	/*
	//uses interact button to open the door
	void OnTriggerStay(Collider col){
		if (isOpen && Input.GetButtonDown ("Interact")) {
			BarrierInterpolator.S.ready = true;
		}
	}
	*/

}
