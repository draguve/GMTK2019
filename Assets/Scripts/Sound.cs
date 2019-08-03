using UnityEngine.Audio;
using UnityEngine;



[System.Serializable]
public class Sound  {
	public AudioClip clip;
	public float volume;
	public string name;
	public bool Loop;
	
	[HideInInspector]
	public AudioSource source;

	// Use this for initialization
	
}
