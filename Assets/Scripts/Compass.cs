using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Compass : MonoBehaviour {

    public GameObject Needle;
    public GameObject Target;

    void Start()
    {

    }

    void Update()
    {
        Vector3 north = Target.transform.position - Needle.transform.position;
        north.y = Needle.transform.position.y;
        Quaternion Look = Quaternion.LookRotation(north, Vector3.up);
        Needle.transform.rotation = Quaternion.RotateTowards(Needle.transform.rotation, Look, 90.0f * Time.deltaTime);
    }
}
