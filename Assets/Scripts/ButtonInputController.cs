using UnityEngine;
using System.Collections;

public class ButtonInputController : MonoBehaviour {

    public GameObject[] WinSpaces;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    void GetInput()
    {

        if (Input.GetButtonDown("Jump"))
        {
            GetClosestButton(0);
        }
        if (Input.GetButtonDown("Interact"))
        {
            GetClosestButton(2);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            GetClosestButton(1);
        }
        if (Input.GetButtonDown("Y Button"))
        {
            GetClosestButton(3);
        }
    }

    void GetClosestButton(int index)
    {
        GameObject winSpace = WinSpaces[index];
        RaycastHit hitInfo;
        if (Physics.Raycast(winSpace.transform.position, -winSpace.transform.forward, out hitInfo))
        {
            ArrowCanvasController.Instance.score++;
            Destroy(hitInfo.collider.gameObject);
        }
        else
        {
            ArrowCanvasController.Instance.score--;
        }
        print(ArrowCanvasController.Instance.score);
    }
}
