using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Simulator
{

public interface IRobotCommunicatorListener
{
	void OnDataReceived(RobotCommunicator c, string robotName, string target, List<ControlInputValues> values);
};

public class RobotCommunicator:ListenerContainer<IRobotCommunicatorListener>,ICommunicationLayerListener
{
	Dictionary<int,MotorConnection> m_motorConnectors=new Dictionary<int, MotorConnection>();
	
	ICommunicationLayer m_communicator=null;


	~RobotCommunicator()
	{
		ClearConnectors ();
	}
	public void SetCommunicatorLayer(ICommunicationLayer c)
	{
		if(m_communicator!=null)
			m_communicator.RemoveListener(this);
		m_communicator=c;
		if(m_communicator!=null)
			m_communicator.AddListener(this);
	}
	
	public  void OnSchemeChanged(ICommunicationLayer  layer, List<string> names)
	{
	}
	public  void OnDataArrived(ICommunicationLayer layer,CommunicationData d)
	{
			
		foreach(var m in m_motorConnectors)
		{
			if(!m.Value.IsConnected())
				continue;
			foreach(var data in d.Data)
			{
				if(m.Value.GetSourceJoint()==data.name)
				{
					_listeners.ForEach (p => p.OnDataReceived (this,d.TargetName,m.Value.GetTargetJoint(),data.values));
				}
			}
		}
	}
	public  void OnClosed(ICommunicationLayer l)
	{
	}
	
	public Dictionary<int,MotorConnection> GetMotorConnections(){return m_motorConnectors;}

	public MotorConnection AddConnection(string src,string target,bool connected=true)
	{
		MotorConnection connection=new MotorConnection();
		connection.SetSourceJoint(src);
		connection.SetTargetJoint(target);
		connection.SetConencted(connected);
		
		m_motorConnectors[new Guid(target).GetHashCode()]=connection;
		return connection;
	}
	public void RemoveConnection(string target)
	{
		m_motorConnectors.Remove (new Guid (target).GetHashCode ());
	}
	public void ClearConnectors()
	{
		m_motorConnectors.Clear ();
	}
}

}