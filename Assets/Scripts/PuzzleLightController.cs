using UnityEngine;
using System.Collections;

public class PuzzleLightController : MonoBehaviour {

    public GameObject Puzzle;
    public Material Solved;

    private bool solved;

	// Use this for initialization
	void Awake () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(solved)
        {
            print("here");
           // GetComponent<Light>() = Solved;
        }
	}
}
