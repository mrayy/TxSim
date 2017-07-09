﻿using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

 internal static class Utilities  {
	public static float AxisSize=0.1f;

	public static void DrawAxis(Matrix4x4 frame)
	{
		Vector3 X = frame.GetColumn (0)*AxisSize;
		Vector3 Y = frame.GetColumn (1)*AxisSize;
		Vector3 Z = frame.GetColumn (2)*AxisSize;
		Vector3 pos = frame.GetColumn (3);
		Debug.DrawLine (pos,pos+X,Color.red);
		Debug.DrawLine (pos,pos+Y,Color.green);
		Debug.DrawLine (pos,pos+Z,Color.blue);
	}

	public static void ChangeVisibility(this Transform transform,bool visible,bool recursive)
	{
		var r = transform.GetComponent<MeshRenderer> ();
		if (r)
			r.enabled = visible;
		for (int i = 0; i < transform.childCount; ++i) {
			var c = transform.GetChild (i);
			r = c.GetComponent<MeshRenderer> ();
			if (r)
				r.enabled = visible;
			if (recursive)
				ChangeVisibility (c,visible,recursive);
		}
	}

		public static void FromMatrix4x4(this Transform transform, Matrix4x4 matrix)
		{
			transform.localScale = matrix.GetScale();
			transform.localRotation = matrix.GetRotation();
			transform.localPosition = matrix.GetPosition();
		}


		private static float _copysign(float sizeval, float signval)
		{
			return Mathf.Sign(signval) == 1 ? Mathf.Abs(sizeval) : -Mathf.Abs(sizeval);
		}

		public static Quaternion GetRotation(this Matrix4x4 matrix)
		{/*
			var qw = Mathf.Sqrt(1f + matrix.m00 + matrix.m11 + matrix.m22) / 2;
			var w = 4 * qw;
			var qx = (matrix.m21 - matrix.m12) / w;
			var qy = (matrix.m02 - matrix.m20) / w;
			var qz = (matrix.m10 - matrix.m01) / w;
			
			return new Quaternion(qx, qy, qz, qw);*/

			Quaternion q = new Quaternion();
			q.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 + matrix.m11 + matrix.m22)) / 2;
			q.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 - matrix.m11 - matrix.m22)) / 2;
			q.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 + matrix.m11 - matrix.m22)) / 2;
			q.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 - matrix.m11 + matrix.m22)) / 2;
			q.x = _copysign(q.x, matrix.m21 - matrix.m12);
			q.y = _copysign(q.y, matrix.m02 - matrix.m20);
			q.z = _copysign(q.z, matrix.m10 - matrix.m01);
			return q;
		}
		
		public static Vector3 GetPosition(this Matrix4x4 matrix)
		{
			var x = matrix.m03;
			var y = matrix.m13;
			var z = matrix.m23;
			
			return new Vector3(x, y, z);
		}
		
		public static Vector3 GetScale(this Matrix4x4 m)
		{
			var x = Mathf.Sqrt(m.m00 * m.m00 + m.m01 * m.m01 + m.m02 * m.m02);
			var y = Mathf.Sqrt(m.m10 * m.m10 + m.m11 * m.m11 + m.m12 * m.m12);
			var z = Mathf.Sqrt(m.m20 * m.m20 + m.m21 * m.m21 + m.m22 * m.m22);
			
			return new Vector3(x, y, z);
		}
	public static byte[] GetBytes(string str)
	{
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}
	
	public static string GetString(byte[] bytes)
	{
		char[] chars = new char[bytes.Length / sizeof(char)];
		System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
		return new string(chars);
	}
	public static string LocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}


	public static string ToExportString(this Quaternion q)
	{
		return string.Format ("{0},{1},{2},{3}", q.w, q.x, q.y, q.z);
	}
	
	public static string ToExportString(this Vector2 q)
	{
		return string.Format ("{0},{1}",  q.x, q.y);
	}
	public static string ToExportString(this Vector3 q)
	{
		return string.Format ("{0},{1},{2}",  q.x, q.y, q.z);
	}

	public static Vector2 ParseVector2(string str)
	{
		try
		{
			string[] splits= str.Split (",".ToCharArray (), 2);
			if (splits.Length < 2)
				return Vector2.zero;
			return new Vector2(float.Parse(splits[0]),float.Parse(splits[1]));
		}catch
		{
			return Vector2.zero;
		}
	}
	public static Vector3 ParseVector3(string str)
	{
		try
		{
			string[] splits= str.Split (",".ToCharArray (), 3);
			if (splits.Length < 3)
				return Vector3.zero;
			return new Vector3(float.Parse(splits[0]),float.Parse(splits[1]),float.Parse(splits[2]));
		}catch
		{
			return Vector3.zero;
		}
	}
	public static Vector4 ParseVector4(string str)
	{
		try
		{
			string[] splits= str.Split (",".ToCharArray (), 4);
			if (splits.Length < 4)
				return Vector4.zero;
			return new Vector4(float.Parse(splits[0]),float.Parse(splits[1]),float.Parse(splits[2]),float.Parse(splits[3]));
		}catch
		{
			return Vector4.zero;
		}
	}
	public static Quaternion ParseQuaternion(string str)
	{
		try
		{
			string[] splits= str.Split (",".ToCharArray (), 4);
			if (splits.Length < 4)
				return Quaternion.identity;
			return new Quaternion(float.Parse(splits[1]),float.Parse(splits[2]),float.Parse(splits[3]),float.Parse(splits[0]));
		}catch
		{
				return Quaternion.identity;
		}
	}

	public static string ReadStringNative(this BinaryReader reader)
	{
		int len=reader.ReadInt32 ();
		if (len == 0)
			return "";
		byte[] data=reader.ReadBytes (len);
		return Encoding.UTF8.GetString (data);
	}
	public static void WriteStringNative(this BinaryWriter writer,string s)
	{
		writer.Write (s.Length);
		writer.Write (Encoding.UTF8.GetBytes(s.ToCharArray ()));
	}


	public static Transform FindChildRecursive(this Transform parent,string name)
	{
		if (parent.name == name)
			return parent;
		foreach (Transform t in parent) {
			Transform r = t.FindChildRecursive (name);
			if (r != null)
				return r;
		}
		return null;
	}



	static float[] boxesForGauss(float sigma, int n)  // standard deviation, number of boxes
	{
		var wIdeal = Mathf.Sqrt((12*sigma*sigma/n)+1);  // Ideal averaging filter width 
		int wl = (int)Mathf.Floor(wIdeal); 
		if(wl%2==0) wl--;
		var wu = wl+2;

		var mIdeal = (12*sigma*sigma - n*wl*wl - 4*n*wl - 3*n)/(-4*wl - 4);
		var m = mIdeal;
		// var sigmaActual = Math.sqrt( (m*wl*wl + (n-m)*wu*wu - n)/12 );

		float[] sizes =new float[n]; 
		for(var i=0; i<n; i++) sizes[i]=(i<m?wl:wu);
		return sizes;
	}


	public static void GaussBlur (float[,] scl, float[,] tcl, int w, int h, float r) {
		var bxs = boxesForGauss(r, 3);
		boxBlur_3 (scl, tcl, w, h, (bxs[0]-1)/2);
		boxBlur_3 (tcl, scl, w, h, (bxs[1]-1)/2);
		boxBlur_3 (scl, tcl, w, h, (bxs[2]-1)/2);
	}
	static void boxBlur_3 (float[,] scl, float[,] tcl, int w, int h, float r) {
		for(var i=0; i<scl.GetLength(0); i++) 
			for(var j=0; j<scl.GetLength(1); j++) 
				tcl[i,j] = scl[i,j];
		boxBlurH_3(tcl, scl, w, h, r);
		boxBlurT_3(scl, tcl, w, h, r);
	}
	static void boxBlurH_3 (float[,] scl, float[,] tcl, int w, int h, float r) {
		for(var i=0; i<h; i++)
			for(var j=0; j<w; j++)  {
				float val = 0;
				for(var ix=j-r; ix<j+r+1; ix++) {
					int x = (int)Mathf.Min(w-1, Mathf.Max(0, ix));
					val += scl[x,i];
				}
				tcl[j,i] = val/(r+r+1);
			}
	}   
	static void boxBlurT_3 (float[,] scl, float[,] tcl, int w, int h, float r) {
		for(var i=0; i<h; i++)
			for(var j=0; j<w; j++) {
				float val = 0;
				for(var iy=i-r; iy<i+r+1; iy++) {
					int y = (int)Mathf.Min(h-1, Mathf.Max(0, iy));
					val += scl[j,y];
				}
				tcl[j,i] = val/(r+r+1);
			}
	}
}
