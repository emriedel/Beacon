using UnityEngine;
using System.Collections;

public class BarrierInterpolator : MonoBehaviour {

	//public static BarrierInterpolator S;

	private float u;
	private Vector3 p0, p1;
	public bool ready = false;

	// Use this for initialization
	void Start () {
		//S = this;
		p0 = transform.position;
		p1 = new Vector3 (p0.x, -4, p0.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -3.9f) {
			Destroy (this.gameObject);
		}
		if (ready) {
			u = Time.time % 1.0f;
			//u = Mathf.Pow (u, 1);
			Vector3 p01 = (1 - u) * p0 + (u * p1);
			transform.position = p01;
		}
	}
}
