using UnityEngine;
using System.Collections;

public class DisplayEnabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("displays connected: " + Display.displays.Length);
		Display.displays [0].Activate ();
		if(Display.displays.Length>1)
			Display.displays [1].Activate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
