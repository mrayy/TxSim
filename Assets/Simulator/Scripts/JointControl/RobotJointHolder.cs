using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Simulator
{

	[ExecuteInEditMode]
	public class RobotJointHolder : MonoBehaviour
	{

		[Serializable]
		public class JointInfo
		{
			public IRobotJoint Joint;
			public float Value;
		}

		public JointInfo[] Joints;
		public IRobotJoint Root;
		public bool Control = false;
		// Use this for initialization

		public JointInfo GetJoint (IRobotJoint j)
		{
			foreach (var o in Joints)
				if (o.Joint == j)
					return o;

			return null;
		}

		void Start ()
		{
			IRobotJoint[] joints = GetComponentsInChildren<IRobotJoint> ();
			List<JointInfo> jList = new List<JointInfo> ();
			if (Joints != null && Joints.Length != 0) {
				jList.AddRange (Joints);
			}
			//	Root = null;
			foreach (var j in joints) {
				if (GetJoint (j) != null)
					continue;
				if (j.isFixed)
					continue;
				JointInfo ifo = new JointInfo ();
				ifo.Joint = j;
				ifo.Value = 0;
				jList.Add (ifo);
			}

			foreach (var j in jList) {

				if (Root == null && j.Joint.ParentJoint == null) {
					Root = j.Joint;
					break;
				}
			}
			Joints = jList.ToArray ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Control && Joints != null) {
				foreach (var j in Joints) {
					j.Joint.SetValue (j.Value, j.Value);
					if (j.Joint.Value != j.Value)
						j.Joint.SetValue (j.Joint.Value, j.Joint.Value);
				}
			}
		}
	}
}
