using UnityEngine;
using System.Collections;
using System;
using SharedMemory;
using System.Runtime.InteropServices;

public class SharedMemoryTest : MonoBehaviour {
	byte[] dbuf;

	shmem_T5_data data;//=new shmem_T5_data();
	SharedMemory.BufferReadWrite buffer;
	// Use this for initialization
	void Start () {
		try
		{
			
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.target));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.hardware));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.shoulder));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.glove));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.joints));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.sensors));
			Debug.Log (System.Runtime.InteropServices.Marshal.SizeOf (data.status));
			data.init();
			dbuf=new byte[1432];//FastStructure.SizeOf<shmem_T5_data>()];
			buffer=new SharedMemory.BufferReadWrite("T5MA_MMF_",dbuf.Length,false);
		}catch(Exception e) {
			Debug.Log (e.ToString());
		}
	}

	void OnDestroy()
	{
	//	buffer.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		if (buffer == null)
			return;
		buffer.AcquireReadLock ();
		buffer.Read (dbuf);
		buffer.ReleaseReadLock ();

		data.Parse (dbuf, 0);

		Debug.Log (data.joints.rt_arm.left[0].ToString());

		/*
		data [2] = (byte)((data [2]+1)%127);


		buffer.AcquireWriteLock ();
		buffer.Write<byte> (data);
		buffer.ReleaseWriteLock ();*/
	}
}
