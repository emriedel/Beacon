using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public GameObject Impact;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Impact.transform.position);
        transform.position += transform.forward * Time.deltaTime * 50f;
	}
}
