using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    public Button Button;

    public GameObject WinSpace;
    public Canvas Canvas;
    private float LastTime;

    ArrowController(GameObject winSpace, Button button)
    {
        WinSpace = winSpace;
        Button = button;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        MoveArrow();
        //GetInput();
	}

    void MoveArrow()
    {
        float y = transform.position.y + 2;
        RectTransform canvas = Canvas.GetComponent<RectTransform>();
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        if (y > canvas.rect.height)
        {
            Destroy(gameObject);
        }
    }
}
