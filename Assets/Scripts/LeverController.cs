using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour
{

    public GameObject LightPuzzle;
	public GameObject LeverIntroPuzzle;
    private LightPuzzle LP;
	private LeverIntroPuzzle IP;

    void Awake()
    {
        LP = LightPuzzle.GetComponent<LightPuzzle>();
		IP = LeverIntroPuzzle.GetComponent<LeverIntroPuzzle> ();
    }

    public void ToggleSwitch()
    {
        string name = gameObject.name;
        LP.HandleInput(name);
    }

	public void ToggleLever(){
		IP.HandleInput();
	}
}
