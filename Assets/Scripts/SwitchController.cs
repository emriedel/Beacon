using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    public GameObject Door;

    public GameObject LightPuzzleLight;
    public GameObject CanvasPuzzleLight;
    public GameObject RotorPuzzleLight;

    public Material OffRed;
    public Material OffGreen;
    public Material OnRed;
    public Material OnGreen;

    public GameObject LightPuzzle;
    public GameObject CanvasPuzzle;
    public GameObject RotorPuzzle;

    public bool open = false;

    private bool Light = false;
    private bool Canvas = false;
    private bool Rotor = false;

    void Awake()
    {
        LightPuzzleLight.GetComponent<Renderer>().material = OffRed;
        CanvasPuzzleLight.GetComponent<Renderer>().material = OffRed;
        RotorPuzzleLight.GetComponent<Renderer>().material = OffRed;
    }

    void Update()
    {
        if(LightPuzzle.GetComponent<LightPuzzle>().solved)
        {
            Light = true;
            LightPuzzleLight.GetComponent<Renderer>().material = OnGreen;
        }
        if (CanvasPuzzle.GetComponent<CanvasPuzzleController>().solved)
        {
            Canvas = true;
            CanvasPuzzleLight.GetComponent<Renderer>().material = OnGreen;
        }
        if (RotorPuzzle.GetComponent<LightPuzzle>().solved)
        {
            Rotor = true;
            RotorPuzzleLight.GetComponent<Renderer>().material = OnGreen;
        }

        if(Light && Canvas && Rotor)
        {
            open = true;
        }
    }

    public void DoorInteract()
    {
        if (Door.GetComponent<DoorController>().isOpen)
        {
            Door.GetComponent<DoorController>().CloseDoor();
        }    
        else
        {
            Door.GetComponent<DoorController>().OpenDoor();
        }
    }

	public void BarrierInteract(){
		if (!Door.GetComponent<DoorController> ().isOpen) {
			Door.GetComponent<DoorController> ().RemoveBarrier ();
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player1Controller.Instance.CloseToPuzzle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player1Controller.Instance.CloseToPuzzle = false;
        }
    }
}
