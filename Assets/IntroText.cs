using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroText : MonoBehaviour {

	void Start () {
		Invoke ("startGame", 20f);
	}

	void startGame () {
		Application.LoadLevel("__MainScene");
	}

}
