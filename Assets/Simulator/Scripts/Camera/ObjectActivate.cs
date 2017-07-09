using UnityEngine;
using System.Collections;

public class ObjectActivate : MonoBehaviour {

	public GameObject[] objects;

	public int activeElement = 0;
	// Use this for initialization
	void Start () {
		activeElement = PlayerPrefs.GetInt ("ActiveElement");
		Activate (activeElement);
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt("ActiveElement",activeElement);
	}

	void Activate(int e)
	{
		activeElement = e;
		for (int i = 0; i < objects.Length; ++i) {
			if (i == e)
				objects [i].SetActive (true);
			else
				objects [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < objects.Length; ++i) {
			if (Input.GetKeyDown ((KeyCode)((int)KeyCode.Alpha1 + i)))
				Activate (i);
		}
	}
}
