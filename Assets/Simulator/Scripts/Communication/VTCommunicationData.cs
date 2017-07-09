using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Simulator
{
public class ControlInputValues
{
	public float KinValue;
	public float RealtimeValue;


	public ControlInputValues()
	{
		KinValue = 0;
		RealtimeValue = 0;
	}
	public ControlInputValues(float kin,float rt)
	{
		KinValue = kin;
		RealtimeValue = rt;
	}
}

public class CommunicationData  {

	protected List<DataPair> _data;

	public List<DataPair> Data
	{
		get{
			return _data;
		}
	}

	public string TargetName {
		set;
		get;
	}

	public class DataPair
	{
		public DataPair(){}
		public DataPair(string target,ControlInputValues[] d)
		{

			name=target;
			for(int i=0;i<d.Length;++i)
				values.Add(d[i]);
		}
		public string name="";
		public List<ControlInputValues> values=new List<ControlInputValues>();
	};


	public void AddData(string src,ControlInputValues[] d)
	{
		_data.Add(new DataPair(src,d));
	}

	public void ClearData()
	{
		_data.Clear ();
	}
}

public class CommunicationDataStreamer
{

		
	public static void WriteData(CommunicationData d,Stream s)
	{
		BinaryWriter w = new BinaryWriter (s);
		w.WriteStringNative (d.TargetName);
		w.Write (d.Data.Count);

		foreach (var v in d.Data) {
			w.WriteStringNative(v.name);
			w.Write(v.values.Count);
			foreach(var i in v.values)
			{
				w.Write(i.KinValue);
				w.Write(i.RealtimeValue);
			}
		}
	}
	public static void ReadData(CommunicationData ret,Stream s)
	{
		BinaryReader r = new BinaryReader (s);
		ret.ClearData ();
		ret.TargetName = r.ReadStringNative ();
		int cnt = r.ReadInt32 ();
		for (int i=0; i<cnt; ++i) {
			CommunicationData.DataPair d=new CommunicationData.DataPair();
			d.name=r.ReadStringNative();
			int v=r.ReadInt32();
			for(int j=0;j<v;++j)
			{
				ControlInputValues cv=new ControlInputValues();
				cv.KinValue=r.ReadSingle();
				cv.RealtimeValue=r.ReadSingle();
				d.values.Add(cv);
			}
			ret.AddData(d.name,d.values.ToArray());
		}
	}
};
}