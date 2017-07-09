using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Simulator
{

	public interface ICommunicationLayerListener
	{
		 void OnDataArrived(ICommunicationLayer layer,CommunicationData data);
		 void OnClosed(ICommunicationLayer layer);
		 void OnSchemeChanged(ICommunicationLayer layer,List<string> names);

	}

public abstract class ICommunicationLayer:ListenerContainer<ICommunicationLayerListener> 
	{


	public abstract void Start();
	public abstract void Close();
	public abstract void Update(float dt);
	public abstract List<string> GetScheme();
	public abstract string InjectCommand(string  cmd, string args);
}

}

