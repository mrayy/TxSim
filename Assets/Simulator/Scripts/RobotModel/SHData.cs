

// Position information
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine;



public struct shmem_point {
     
	public   float[] pos;
     
	public   float[] ori;

	public void init(){
		pos = new float[3];
		ori = new float[3];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 3; ++i) {
			pos[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 3; ++i) {
			ori[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;

public struct shmem_coor {
	public shmem_point eye;
	public shmem_point shoulder;
	public shmem_point right_arm;
	public shmem_point left_arm;
	public shmem_point T_eye_lhand;
	public shmem_point T_eye_rhand;
	public shmem_point T_sh_lhand;
	public shmem_point T_sh_rhand;
	public void init(){
		eye.init();
		shoulder.init ();
		right_arm.init ();
		left_arm.init ();
		T_eye_lhand.init ();
		T_eye_rhand.init ();
		T_sh_lhand.init ();
		T_sh_rhand.init ();
	}

	public int Parse(byte[] data,int offset)
	{
		offset=eye.Parse (data, offset);
		offset=shoulder.Parse (data, offset);
		offset=right_arm.Parse (data, offset);
		offset=left_arm.Parse (data, offset);
		offset=T_eye_lhand.Parse (data, offset);
		offset=T_eye_rhand.Parse (data, offset);
		offset=T_sh_lhand.Parse (data, offset);
		offset=T_sh_rhand.Parse (data, offset);

		return offset;
	}
} ;


public struct shmem_target {
	public shmem_coor user;
	public shmem_coor kin;
	public shmem_coor end_effector;
	public void init(){
		user.init();
		kin.init ();
		end_effector.init ();
	}

	public int Parse(byte[] data,int offset)
	{
		offset=user.Parse (data, offset);
		offset=kin.Parse (data, offset);
		offset=end_effector.Parse (data, offset);
		return offset;
	}
} ;



public struct shmem_arms {
     
	public  float[] right;
     
	public  float[] left;
	public void init(){
		right = new float[7];
		left = new float[7];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 7; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 7; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_hands {
     
	public  float[] right;
     
	public  float[] left;
	public void init(){
		right = new float[16];
		left = new float[16];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 16; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 16; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_glove {
	public float[] right;
	public float[] left;
	public int r_gus;
	public int l_gus;
	public void init(){
		right = new float[14];
		left = new float[14];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 14; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 14; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		r_gus=BitConverter.ToInt32 (data, offset);offset += sizeof(int);
		l_gus=BitConverter.ToInt32 (data, offset);offset += sizeof(int);
		return offset;
	}
} ;


public struct shmem_force {
     
	public float[] right;
     
	public float[] left;
	public void init(){
		right = new float[3];
		left = new float[3];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 3; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 3; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_temp {
     
	public float[] right;
     
	public float[] left;
	public void init(){
		right = new float[3];
		left = new float[3];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 3; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 3; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_tactile {
     
	public float[] right;
     
	public float[] left;
	public void init(){
		right = new float[3];
		left = new float[3];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 3; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 3; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public  struct shmem_current {
     
	public float[] right;
     
	public float[] left;
	public void init(){
		right = new float[7];
		left = new float[7];
	}


	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 7; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 7; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_torque {
     
	public float[] right;
     
	public float[] left;
	public void init(){
		right = new float[7];
		left = new float[7];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 7; ++i) {
			right[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 7; ++i) {
			left[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;




// joint space / sensor space abd status messages


public struct shmem_shoulder {
     
	public float[] pos;
     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
	public float[] ori;
	public void init(){
		pos = new float[3];
		ori = new float[9];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 3; ++i) {
			pos[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 9; ++i) {
			ori[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		return offset;
	}
} ;


public struct shmem_hw_data {
	public float[] body;

	public  bool dviState; 
	
	public  bool fsState;  
	public void init(){
		body = new float[9];
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 9; ++i) {
			body[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		dviState = BitConverter.ToBoolean (data, offset);offset += 1;
		fsState = BitConverter.ToBoolean (data, offset);offset += 1;
		offset += 2;
		return offset;
	}
} ;


public  struct shmem_joints {
	public float[] kin_body;
	public float[] rt_body;
	public shmem_arms kin_arm;
	public shmem_arms rt_arm;
	public shmem_hands kin_hand;
	public shmem_hands rt_hand;
	public void init(){
		kin_body = new float[9];
		rt_body = new float[9];
		kin_arm.init ();
		rt_arm.init ();
		kin_hand.init ();
		rt_hand.init ();
	}

	public int Parse(byte[] data,int offset)
	{
		for (int i = 0; i < 9; ++i) {
			kin_body[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		for (int i = 0; i < 9; ++i) {
			rt_body[i]=BitConverter.ToSingle (data, offset);
			offset += sizeof(float);
		}
		offset=kin_arm.Parse (data, offset);
		offset=rt_arm.Parse (data, offset);
		offset=kin_hand.Parse (data, offset);
		offset=rt_hand.Parse (data, offset);

		return offset;
	}
} ;


public  struct shmem_sensors {
	public shmem_force force;
	public shmem_temp temp;
	public shmem_tactile tactile;
	public shmem_current currrent; 
	public shmem_torque torque; 
	public void init(){
		force.init();
		temp.init ();
		tactile.init ();
		currrent.init ();
		torque.init ();
	}

	public int Parse(byte[] data,int offset)
	{
		offset=force.Parse (data, offset);
		offset=temp.Parse (data, offset);
		offset=tactile.Parse (data, offset);
		offset=currrent.Parse (data, offset);
		offset=torque.Parse (data, offset);

		return offset;
	}
} ;


public  struct shmem_status {
	
	public  bool telexistence;
	
	public  bool connected; 
	
	public  bool calibration;  
	
	public  bool home; 
	
	public  bool initial_pose; 
	
	public  bool glove_reset; 
	
	public  bool finger_calibrate;
	 
	public  bool emergency;
	
	public  bool arm_reset;
	
	public  bool ik_reset;
	
	public  bool force_reset;
	
	public  bool thermal;
	
	public  bool dviState; 
	
	public  bool fsState; 
	
	public  bool logStart;
	public int state; 
	public void init(){
	}

	public int Parse(byte[] data,int offset)
	{
		telexistence=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		connected=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		calibration=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		home=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		initial_pose=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		glove_reset=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		finger_calibrate=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		emergency=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		arm_reset=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		ik_reset=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		force_reset=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		thermal=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		dviState=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		fsState=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		logStart=BitConverter.ToBoolean (data, offset);offset += sizeof(bool);
		state=BitConverter.ToInt32 (data, offset);offset += sizeof(int);
		offset += 1;
		return offset;
	}
} ;


// final TELESAR V data structure


public struct shmem_T5_data {
	public uint timestamp;
	public shmem_target target;
	public shmem_hw_data hardware;
	public shmem_shoulder shoulder;
	public shmem_glove glove; 
	public shmem_joints joints;
	public shmem_sensors sensors;
	public shmem_status status; 
	public void init(){
		target.init();
		hardware.init ();
		shoulder.init ();
		glove.init ();
		joints.init ();
		sensors.init ();
		status.init ();
	}

	public int Parse(byte[] data,int offset)
	{
		timestamp=BitConverter.ToUInt32 (data, offset);offset += sizeof(uint);
		offset=target.Parse (data, offset);
		offset=hardware.Parse (data, offset);
		offset=shoulder.Parse (data, offset);
		offset=glove.Parse (data, offset);
		offset=joints.Parse (data, offset);
		offset=sensors.Parse (data, offset);
		offset=status.Parse (data, offset);

		return offset;
	}
} ;


public class TelesarVSharedMemory
{
	byte[] dbuf;

	public shmem_T5_data data;//=new shmem_T5_data();
	SharedMemory.BufferReadWrite buffer;
	public TelesarVSharedMemory()
	{
		try
		{

			data.init();
			dbuf=new byte[1432];//FastStructure.SizeOf<shmem_T5_data>()];
			buffer=new SharedMemory.BufferReadWrite("T5MA_MMF_",dbuf.Length,false);
		}catch(Exception e) {
			Debug.Log (e.ToString());
		}
	}

	public void Destroy()
	{
		buffer.Close ();
	}
	public void Update () {
		if (buffer == null)
			return;
		buffer.AcquireReadLock ();
		buffer.Read (dbuf);
		buffer.ReleaseReadLock ();

		data.Parse (dbuf, 0);

	}
};