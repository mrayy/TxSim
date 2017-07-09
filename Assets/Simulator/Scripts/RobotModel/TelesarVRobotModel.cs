using UnityEngine;
using System.Collections;

namespace Simulator
{

public class TelesarVRobotModel : MonoBehaviour 
{
	TelesarVSharedMemory _sharedMemory;

	public Simulator.IRobotJoint[] Joints;

	public bool UseRealtimeData = false;

	public TelesarVArmModel LeftArm;
	public TelesarVArmModel RightArm;

	// Use this for initialization
	void Start () {

		_sharedMemory = new TelesarVSharedMemory ();

		string prefix = "Body_J";
		Joints=new Simulator.IRobotJoint[9];
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
		_sharedMemory.Update ();

			for (int i = 0; i < Joints.Length; ++i) {
				Joints [i].SetValue (Mathf.Rad2Deg * _sharedMemory.data.joints.kin_body [i],Mathf.Rad2Deg * _sharedMemory.data.joints.rt_body [i]);
			}
			if(LeftArm!=null)
				LeftArm.ParseSharedMemory (_sharedMemory,UseRealtimeData);
			if(RightArm!=null)
				RightArm.ParseSharedMemory (_sharedMemory,UseRealtimeData);
	}
}

}