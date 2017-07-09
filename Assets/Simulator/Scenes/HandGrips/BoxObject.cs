using UnityEngine;
using System.Collections;

public class BoxObject : MonoBehaviour {

	public float height=0.1f;
	public float width=0.1f;

	public GameObject TargetObject;

	// Use this for initialization
	void Start () {
		MeshFilter mf= TargetObject.GetComponent<MeshFilter> ();
		mf.mesh = MeshGenerator.GenerateBox(width,height,width);
		TargetObject.GetComponent<BoxCollider> ().size = new Vector3 (width, height, width);
	}


	public bool Physics=false;
	// Update is called once per frame
	void Update () {
		BoxCollider c = TargetObject.GetComponent<BoxCollider> ();
		TargetObject.GetComponent<Rigidbody> ().isKinematic = !Physics;
		if (TargetObject.GetComponent<Rigidbody> ().isKinematic)
		{
			if(c.isTrigger==false) 
				c.isTrigger = true;
		} else if(c.isTrigger==true)
			c.isTrigger = false;

	}

	public void StartCapture()
	{
		TargetObject.GetComponent<MeshRenderer> ().material.color = Color.Lerp (Color.white, Color.red, 0.3f);
	}
	public void EndCapture()
	{
	}

}
