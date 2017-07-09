using UnityEngine;
using System.Collections;

public class TelesarVArmModel : MonoBehaviour {

	public enum ArmType
	{
		Left,
		Right
	}

	public ArmType type;

	public Simulator.IRobotJoint[] Joints;

	public TelesarVHandModel Hand;

	// Use this for initialization
	void Start () {

		string prefix;
		if (type == ArmType.Left)
			prefix = "L";
		else
			prefix = "R";
		prefix += "Arm_J";

		Joints=new Simulator.IRobotJoint[7];
		for (int i = 0; i < Joints.Length; ++i) {
			
			Transform t = transform.FindChildRecursive(prefix+(i+1).ToString());//transform.FindChild(prefix + (i+1).ToString ());
			if(t!=null)
			{
				Joints [i]=t.GetComponent<Simulator.IRobotJoint>();
			}
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ParseSharedMemory(TelesarVSharedMemory sh,bool RealtimeData)
	{
		Hand.ParseSharedMemory (sh,RealtimeData);
		for (int i = 0; i < Joints.Length; ++i) {
			if (type == ArmType.Left){
				Joints [i].SetValue (Mathf.Rad2Deg * sh.data.joints.kin_arm.left [i],Mathf.Rad2Deg * sh.data.joints.rt_arm.left [i]);
			}
			else {
				Joints [i].SetValue (Mathf.Rad2Deg * sh.data.joints.kin_arm.right [i],Mathf.Rad2Deg * sh.data.joints.rt_arm.right [i]);
			}


		}
	}
}
