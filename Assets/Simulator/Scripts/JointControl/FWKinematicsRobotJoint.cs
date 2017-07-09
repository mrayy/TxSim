using UnityEngine;
using System.Collections;

namespace Simulator
{

	public class FWKinematicsRobotJoint : IRobotJoint
	{

		public enum JointAxis
		{
			X,
Y,
Z
		}

		public JointAxis Axis;
		public bool InverseAxis;
		[SerializeField]
		float _jointValue;
		float _ikValue;
		float _realValue;

		public bool Manual = false;


		// Use this for initialization
		protected override void Start ()
		{
			base.Start ();
		}
	
		// Update is called once per frame
		protected virtual void Update ()
		{
			if (CoupledJoint != null)
				_SetValue (CoupledJoint.Value * coupleFactor,CoupledJoint.Value * coupleFactor);
			//else if (Manual)
			//	SetValue (JointValue);
			base.Update ();
		}


	
		public override void _UpdateJoint ()
		{
			float v = _jointValue;
			if (InverseAxis)
				v = -v;
			switch (Axis) {
			case JointAxis.X:
				this.transform.localRotation = Quaternion.Euler (v, 0, 0);
				break;
			case JointAxis.Y:
				this.transform.localRotation = Quaternion.Euler (0, v, 0);
				break;
			case JointAxis.Z:
				this.transform.localRotation = Quaternion.Euler (0, 0, v);
				break;
			}
		}

		protected override void _DebugRender ()
		{
		}

	
		public override Vector3 GetAbsPosition ()
		{
		
			return this.transform.position;
		}


		public override void SetValue (float ik,float real)
		{
			if (CoupledJoint != null)
				return;
			_SetValue (ik,real);
		}

		void _SetValue (float ik, float real)
		{
			_ikValue = ik;//*(InverseAxis?-1:1);
			_realValue = real;//*(InverseAxis?-1:1);
			if (useIKValue)
				_jointValue = _ikValue;
			else
				_jointValue = _realValue;
			if (_jointValue > MaxLimit)
				_jointValue = MaxLimit;
			if (_jointValue < MinLimit)
				_jointValue = MinLimit;

			//	_jointValue = v;
			_UpdateJoint ();
		}

		public override float GetRealValue ()
		{
			return _realValue;
		}

		public override float GetIKValue ()
		{
			return _ikValue;
		}

		public override float GetValue ()
		{
			return _jointValue;
		}
	}

}
