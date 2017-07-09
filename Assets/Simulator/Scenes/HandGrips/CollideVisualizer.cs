using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollideVisualizer : MonoBehaviour {

	List<Collider> _colliders;
	// Use this for initialization
	void Start () {
		_colliders = new List<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	int collCount=0;

	public int GetCollCount(){
		return collCount;
	}


	void OnDrawGizmos()
	{
		if (_colliders != null) {
			foreach (var c in _colliders) {

				Gizmos.DrawSphere (c.bounds.center, 0.01f);
			}
		}
	}
	void OnTriggerEnter(Collider c)
	{
		_colliders.Add (c);
		collCount++;
		GetComponent<MeshRenderer> ().material.color = Color.Lerp(Color.white,Color.red,Mathf.Clamp01(collCount/10.0f));
	}
	void OnTriggerExit(Collider c)
	{
		_colliders.Remove (c);
		collCount--;
		GetComponent<MeshRenderer> ().material.color = Color.Lerp(Color.white,Color.red,Mathf.Clamp01(collCount/10.0f));

	}
}
