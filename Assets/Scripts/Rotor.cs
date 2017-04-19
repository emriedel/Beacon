using UnityEngine;
using System.Collections;

public class Rotor : MonoBehaviour {
	public int num = 0;

	public Rotor(int n){
		num = n;
	}

	public void Increment(){
		print ("Incrementing");
		num = (num + 1) % 10;
	}
}
