using UnityEngine;
using System.Collections;

public class LightPuzzleController : MonoBehaviour {

	private LightPuzzle lp;
	public GameObject[] ui_lights;
	public BitArray lights_on;

	public GameObject[] ui_levers;
	public BitArray levers_on;

	public Material off_mat;
	public Material on_mat;

	//array to keep track of the differences to update for levers
	private BitArray diffs;

	// Use this for initialization
	void Start () {
		lp = LightPuzzle.S;
		lights_on = new BitArray (5, true);
		levers_on = new BitArray (3, false);
		diffs = new BitArray (3, false);
	}
	
	// Update is called once per frame
	void Update () {
		bool le = CheckLevers ();
		bool li = CheckLights ();
		if (le || li) {
			UpdateUI ();
		}
	}

	//checks if there has been a difference
	bool BitArraysDiffer(BitArray data, BitArray control){
		for (int i = 0; i < data.Length; ++i) {
			if (data [i] != control [i]) {
				return true;
			}
		}
		return false;
	}

	//Makes the bit arrays uniform
	void SetBitArrays(BitArray data, BitArray control){
		for (int i = 0; i < data.Length; ++i) {
			control [i] = data [i];
		}
	}

	void GetLeverDifferences(){
		for (int i = 0; i < lp.levers.Length; ++i) {
			if (lp.levers [i] != levers_on [i]) {
				diffs [i] = true;
			} else {
				diffs [i] = false;
			}
		}
	}

	//checks if the levers need to be updated
	//returns true if the UI needs to be updated
	bool CheckLevers(){
		if (BitArraysDiffer (lp.levers, levers_on)) {
			GetLeverDifferences ();
			SetBitArrays (lp.levers, levers_on);
			return true;
		}
		GetLeverDifferences ();
		return false;
	}

	//checks if the lights need to be updated
	//return true if UI needs to be updated
	bool CheckLights(){
		if (BitArraysDiffer (lp.toggled_lights, lights_on)) {
			SetBitArrays (lp.toggled_lights, lights_on);
			return true;
		}
		return false;
	}

	//Update the lights
	void UpdateLights(){
		for (int i = 0; i < lights_on.Length; ++i) {
			if (lights_on [i]) {
				ui_lights[i].gameObject.GetComponent<Renderer>().material = on_mat;
			} else {
				ui_lights[i].gameObject.GetComponent<Renderer>().material = off_mat;
			}
		}
	}
		
	//Update the Levers
	void UpdateLevers(){
		for (int i = 0; i < diffs.Length; ++i) {
			if (diffs [i]) {
				//ui_levers [i].gameObject.transform.rotation = new Vector3 (0, 0, 125);
				ui_levers [i].gameObject.transform.Rotate(0, 0, 90);
			} 
		}
	}

	//Keeps the UI in sync with the data model
	void UpdateUI(){
		UpdateLights ();
		UpdateLevers ();
	}

    void OnTriggerStay(Collider other)
    {
		if (other.gameObject.tag == "Player" && !LightPuzzle.S.solved)
        {
            //Player1Controller.Instance.CloseToPuzzle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Player" && !LightPuzzle.S.solved)
        {
            //Player1Controller.Instance.CloseToPuzzle = false;
        }
    }
}
