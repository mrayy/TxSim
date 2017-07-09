using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Simulator;

[CustomEditor(typeof(RobotJointHolder))]
public class RobotJointHolderEditor : Editor {

	private RobotJointHolder m_Target;


	public void OnEnable() {
		m_Target = target as RobotJointHolder;
	}

	public override void OnInspectorGUI() {
		if(m_Target == null)
			return;


		m_Target.Control=EditorGUILayout.Toggle ("Control", m_Target.Control);
		for(int i=0;i<m_Target.Joints.Length;++i)
		{
			if (m_Target.Joints [i].Joint.CoupledJoint == null) {
				
				m_Target.Joints [i].Value = EditorGUILayout.Slider (m_Target.Joints [i].Joint.name, m_Target.Joints [i].Value, m_Target.Joints [i].Joint.MinLimit, m_Target.Joints [i].Joint.MaxLimit, null);

			}
		}
	}
}
