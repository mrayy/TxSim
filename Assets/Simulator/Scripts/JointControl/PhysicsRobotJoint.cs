using UnityEngine;
using System.Collections;

namespace Simulator
{

	public class PhysicsRobotJoint : IRobotJoint
	{

		public enum JointAxis
		{
			X,
Y,
Z
		}

		public JointAxis Axis;
		public bool InverseAxis;
		
		float _jointValue;

		ConfigurableJoint _jointHandle;
		//physics parameters for this joint
		public float Spring;
		public float Damping;

		void _createJoint ()
		{
			if (ParentJoint != null && ParentJoint.GetComponent<Rigidbody> () != null) {
				_jointHandle = gameObject.GetComponent<ConfigurableJoint> ();
				if (_jointHandle == null)
					_jointHandle = gameObject.AddComponent<ConfigurableJoint> ();

				_jointHandle.connectedBody = ParentJoint.GetComponent<Rigidbody> ();

				_jointHandle.connectedAnchor = transform.localPosition;
				/*
			JointSpring s= _jointHandle.spring;

			s.spring=this.Spring;
			s.damper=this.Damping;

			_jointHandle.spring=s;*/

				_jointHandle.xMotion = ConfigurableJointMotion.Locked;
				_jointHandle.yMotion = ConfigurableJointMotion.Locked;
				_jointHandle.zMotion = ConfigurableJointMotion.Locked;

				
				_jointHandle.angularXMotion = ConfigurableJointMotion.Locked;
				_jointHandle.angularYMotion = ConfigurableJointMotion.Locked;
				_jointHandle.angularZMotion = ConfigurableJointMotion.Locked;
				switch (Axis) {
				case JointAxis.X:
					_jointHandle.axis = Vector3.left;
					_jointHandle.angularXMotion = ConfigurableJointMotion.Limited;
					break;
				case JointAxis.Y:
					_jointHandle.axis = Vector3.up;
					_jointHandle.angularYMotion = ConfigurableJointMotion.Limited;
					break;
				case JointAxis.Z:
					_jointHandle.axis = Vector3.forward;
					_jointHandle.angularZMotion = ConfigurableJointMotion.Limited;
					break;
				}

				if (InverseAxis)
					_jointHandle.axis = -_jointHandle.axis;
			
			}

		}
		// Use this for initialization
		protected override void Start ()
		{
			base.Start ();
			_createJoint ();
		}
	
		// Update is called once per frame
		protected virtual void Update ()
		{
			base.Update ();
		}


	
		public override void _UpdateJoint ()
		{
		}

		protected override void _DebugRender ()
		{
		}

	
		public override Vector3 GetAbsPosition ()
		{
		
			return this.transform.position;
		}

		public override void SetValue (float ik, float real)
		{
			_jointValue = ik;
			_UpdateJoint ();
		}

		public override float GetValue ()
		{
			return _jointValue;
		}

		public override float GetRealValue ()
		{
			return GetValue ();
		}

		public override float GetIKValue ()
		{
			return GetValue ();
		}
	}
}
