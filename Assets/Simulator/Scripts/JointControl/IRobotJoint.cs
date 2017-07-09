using UnityEngine;
using System.Collections;

namespace Simulator
{

	[ExecuteInEditMode]
	public abstract class IRobotJoint : MonoBehaviour
	{
	
		public enum JoinType
		{
			Prismatic,
			Revolute
		}

		//public float JointValue;
		

		public int ID;

		public bool isFixed = false;

		public JoinType Type;

		public float MinLimit;
		public float MaxLimit;

		public IRobotJoint CoupledJoint;
		public float coupleFactor = 1;

		public bool useIKValue = true;

		public IRobotJoint ParentJoint;


		// Use this for initialization
		protected virtual void Start ()
		{
			if (transform.parent != null)
				ParentJoint = transform.parent.GetComponent<IRobotJoint> ();

		}

		public abstract void _UpdateJoint ();

		protected abstract void _DebugRender ();

		// Update is called once per frame
		protected virtual void Update ()
		{
		
			//SetValue (JointValue);
			if (CoupledJoint != null)
				SetValue (CoupledJoint.Value * coupleFactor,CoupledJoint.Value * coupleFactor);
			_UpdateJoint ();

			_DebugRender ();

		}

		public abstract Vector3 GetAbsPosition ();

		public abstract void SetValue (float ik, float real);

		public abstract float GetRealValue ();

		public abstract float GetIKValue ();

		public abstract float GetValue ();

		public float Value {
			/*set {
				SetValue (value);
			}*/
			get {
				return GetValue ();
			}
		}
	}
}
