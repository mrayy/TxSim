using UnityEngine;
using System.Collections;


namespace Simulator
{
public class MotorConnection  
{
	
	string	m_sourceJoint="";
	string	m_targetJoint="";
	bool	m_connected=false;

	public delegate void delg_OnConnectionChanged(MotorConnection m,bool c);
	public delegate void delg_OnTargetJointChanged(MotorConnection m,string c);
	public delegate void delg_OnSourceJointChanged(MotorConnection m,string c);

	public delg_OnConnectionChanged OnConnectionChanged;
	public delg_OnTargetJointChanged OnTargetJointChanged;
	public delg_OnSourceJointChanged OnSourceJointChanged;


	public MotorConnection()
	{
	}

	public void SetConencted(bool c)
	{
		m_connected=c;
		//FIRE_LISTENR_METHOD(OnConnectionChanged,(this,c));
		if(OnConnectionChanged!=null)
			OnConnectionChanged(this,c);
	}
	public bool IsConnected()
	{
		return m_connected && m_sourceJoint!="" && m_targetJoint!="";
	}
	
	public void SetSourceJoint(string joint)
	{
		m_sourceJoint=joint;
		//FIRE_LISTENR_METHOD(OnSourceJointChanged,(this,joint));
		if(OnSourceJointChanged!=null)
			OnSourceJointChanged(this,joint);
	}
	public string  GetSourceJoint()
	{
		return m_sourceJoint;
	}
	
	public void SetTargetJoint( string joint)
	{
		m_targetJoint=joint;
		//FIRE_LISTENR_METHOD(OnTargetJointChanged,(this,joint));
		if(OnTargetJointChanged!=null)
			OnTargetJointChanged(this,joint);
	}
	public string GetTargetJoint()
	{
		return m_targetJoint;
	}
}
}
