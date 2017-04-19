using UnityEngine;
using System.Collections;

public class CanvasPuzzleController : Puzzle {
    public static CanvasPuzzleController Instance;

    public GameObject HUD;
    public GameObject Puzzle;
    public GameObject OxygenTank;
    public GameObject Beacon;
    public Color Green;


	// Use this for initialization
	void Start () {
        Instance = this;
        solved = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(ArrowCanvasController.Instance.score >= 10)
        {
            solved = true;
            HUD.SetActive(true);
            Puzzle.SetActive(false);
            OxygenTank.SetActive(true);
            Beacon.GetComponent<Light>().color = Green;
        }
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !solved)
        {
            Player1Controller.Instance.ArrowPuzzle = true;
            //HUD.SetActive(false);
            Puzzle.SetActive(true);
            OxygenTank.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player1Controller.Instance.ArrowPuzzle = false;
            //HUD.SetActive(true);
            Puzzle.SetActive(false);
            OxygenTank.SetActive(true);
        }
    }
}
