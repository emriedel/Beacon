using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public GameObject doorObj;
    public bool isOpen = false;

    public void OpenDoor()
    {
        doorObj.GetComponent<Animation>().Play("Open");
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
        doorObj.GetComponent<Animation>().Play("Close");
    }

	//Interpolates the Barrier away with BarrierInterpolator
	public void RemoveBarrier(){
		//BarrierInterpolator.S.ready = true;
		isOpen = true;
	}
}
