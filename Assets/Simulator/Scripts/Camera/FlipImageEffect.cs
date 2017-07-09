using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FlipImageEffect : MonoBehaviour {


	public Shader Shader = null;

	public bool Flip = true;

	static Material m_Material = null;
	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material(Shader);
				m_Material.hideFlags = HideFlags.DontSave;
			}
			return m_Material;
		}
	}

	protected void OnDisable() {/*
		if ( m_Material ) {
			DestroyImmediate( m_Material );
		}*/
	}

	// Use this for initialization
	void Start () {
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
		// Disable if the shader can't run on the users graphics card
		if (!Shader || !material.shader.isSupported) {
			enabled = false;
			return;
		}

		Flip =PlayerPrefs.GetInt ("Flip")==0?false:true;
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt("Flip",Flip ? 0 : 1);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F))
			Flip = !Flip;
	}

	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if(Flip)
			Graphics.Blit(source, destination,m_Material);
		else 
			Graphics.Blit(source, destination);

	}
}
