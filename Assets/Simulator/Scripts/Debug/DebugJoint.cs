using UnityEngine;
using System.Collections;
using Simulator;
using System.Collections.Generic;

public class DebugJoint : MonoBehaviour {

	IRobotJoint _joint;

	public bool _overrideVisiblity=false;
	public bool _visible=false;
	public float threshold = 0.1f;
	List<DebugJoint> children=new List<DebugJoint>();

	static void RecursiveCollectChildren(Transform t,List<DebugJoint> joints)
	{
		for (int i = 0; i < t.childCount; ++i) {
			var c = t.GetChild (i);
			var j=c.GetComponent<DebugJoint> ();
			if (j != null)
				joints.Add (j);
			else
				RecursiveCollectChildren (c.transform,joints);
		}
	}
	// Use this for initialization
	void Start () {
		_joint= GetComponent<IRobotJoint> ();
		RecursiveCollectChildren (transform,children);
	}
	// Update is called once per frame
	void Update () {
		if (_joint) {
			float diff=Mathf.Abs(_joint.GetRealValue()-_joint.GetIKValue());
			if (diff >= threshold) {
				_visible = true;
			} else
				_visible = false;

			foreach (var c in children)
				c._overrideVisiblity = _visible || _overrideVisiblity;
		}

		transform.ChangeVisibility (_visible || _overrideVisiblity, false);
	}
}
