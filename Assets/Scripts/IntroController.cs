using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroController : MonoBehaviour {

    public GameObject Impact;
    public GameObject Crash;

    private bool impact;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * 50 * Time.deltaTime;

        if(impact)
        {
            transform.LookAt(Crash.transform.position);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        print("ello");
        if(other.gameObject == Impact)
        {
            print("here");
            impact = true;
        }
        if(other.gameObject == Crash)
        {
            SceneManager.LoadScene("__IntroScene");
        }
    }
}
