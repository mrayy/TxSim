using UnityEngine;
using System.Collections;

public class TelesarVHandModel : MonoBehaviour
{

	public enum HandType
	{
		Left,
		Right
	}

	static int[] HandMapping = new int[] {
		14,
		15,
		15,

		11,
		12,
		13,

		9,
		10,
		10,

		6,
		7,
		7,

		0,
		1,
		2,
		3,
		4,
	};
	public HandType type;

	Simulator.IRobotJoint[] Joints;
	// Use this for initialization
	void Start ()
	{

		string prefix;
		if (type == HandType.Left)
			prefix = "L";
		else
			prefix = "R";
		prefix += "F_J";

		Joints = new Simulator.IRobotJoint[3 * 4 + 5];
		int index = 0;
		for (int i = 0; i < 4; ++i) {
			for (int j = 0; j < 3; ++j) {
				Transform t = transform.FindChildRecursive (prefix + i.ToString () + "_" + j.ToString ());//transform.FindChild(prefix + (i+1).ToString ());
				if (t != null) {
					Joints [index] = t.GetComponent<Simulator.IRobotJoint> ();
					Joints [index].ID = HandMapping [index];
				}
				++index;
			}
		}
		for (int i = 0; i < 5; ++i) {

			Transform t = transform.FindChildRecursive (prefix + "4_" + i.ToString ());//transform.FindChild(prefix + (i+1).ToString ());
			if (t != null) {
				Joints [index] = t.GetComponent<Simulator.IRobotJoint> ();
				Joints [index].ID = HandMapping [index];
			}
			++index;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ParseSharedMemory (TelesarVSharedMemory sh, bool RealtimeData)
	{

		for (int i = 0; i < Joints.Length; ++i) {
			if (type == HandType.Left) {
				Joints [i].SetValue (Mathf.Rad2Deg * sh.data.joints.kin_hand.left [HandMapping [i]],Mathf.Rad2Deg *sh.data.joints.rt_hand.left [HandMapping [i]]);
			} else {
				Joints [i].SetValue (Mathf.Rad2Deg * sh.data.joints.kin_hand.right [HandMapping [i]],Mathf.Rad2Deg *sh.data.joints.rt_hand.right [HandMapping [i]]);
			}
		}
	}
}
