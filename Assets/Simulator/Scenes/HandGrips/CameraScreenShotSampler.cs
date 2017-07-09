using UnityEngine;
using System.Collections;

public class CameraScreenShotSampler : MonoBehaviour {

	public Camera[] Cameras;
	public string prefix="Cam_";
	public KeyCode ShortCut = KeyCode.F11;
	// Use this for initialization
	Texture2D _screenshot ;
	RenderTexture _rt ;

	public int Width=1280;
	public int Height=720;

	void Start () {
		_rt = new RenderTexture (Width, Height, 32);
		_screenshot = new Texture2D (_rt.width, _rt.height, TextureFormat.ARGB32, false);
		_rt.antiAliasing = 8;
	}

	void Output(Camera c)
	{
		c.targetTexture = _rt;
		c.Render ();

		RenderTexture.active = _rt;
		_screenshot.ReadPixels (new Rect (0, 0, _rt.width, _rt.height), 0, 0);
		RenderTexture.active = null;

		c.targetTexture = null;
		//int width = (int)(gameObject.GetComponent<CylinderObject> ().diameter );
		//string path = string.Format ("{0}/screenshots/{1}_{2}mm_{3}.png", Application.dataPath,prefix,width, c.name);
		string path = string.Format ("{0}/screenshots/{1}_{2}.png", Application.dataPath,prefix, c.name);
		byte[] data = _screenshot.EncodeToPNG ();

		System.IO.File.WriteAllBytes (path, data);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (ShortCut)) {
			//gameObject.GetComponent<CylinderObject> ().StartCapture ();
			for (int i = 0; i < Cameras.Length; ++i) {
				Output (Cameras [i]);
			}

			//gameObject.GetComponent<CylinderObject> ().EndCapture();
		}
	}
}
