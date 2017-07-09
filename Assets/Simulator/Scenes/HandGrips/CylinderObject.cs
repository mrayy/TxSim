using UnityEngine;
using System.Collections;

public class CylinderObject : MonoBehaviour {

	public float height=0.1f;
	public float diameter=0.065f;

	public GameObject TargetObject;

	// Use this for initialization
	void Start () {
		MeshFilter mf= TargetObject.GetComponent<MeshFilter> ();
		mf.mesh = MeshGenerator.GenerateCylinder (height, diameter*0.001f/2.0f, diameter*0.001f/2.0f, 30, 1);
		TargetObject.GetComponent<CapsuleCollider> ().height = height+diameter*0.001f;
		TargetObject.GetComponent<CapsuleCollider> ().radius= diameter*0.001f/2.0f;
	}


	public bool Physics=false;
	// Update is called once per frame
	void Update () {
		CapsuleCollider c = TargetObject.GetComponent<CapsuleCollider> ();
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
