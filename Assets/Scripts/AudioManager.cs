using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour {
	AudioSource audioSource;
	public float sliderval;
	public static float master=0.5f;
	public Sound[] sounds;
	public static AudioManager instance;
	public static bool musicmute;
	// Use this for initialization
	private string currentTheme;
	protected Sound _sound;
	void Awake () {
		if(instance==null)
		{
			instance=this;
		}
		else{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
		foreach(Sound s in sounds)
		{
			s.source=gameObject.AddComponent<AudioSource>();
			s.source.clip=s.clip;

			s.source.volume=s.volume/100;
			s.source.loop=s.Loop;
			s.source.playOnAwake=false;
		}
		
	}
	void Start()
	{
		int x=UnityEngine.Random.Range(1,4);
		currentTheme="Theme_"+x.ToString();
		Play(currentTheme,true);
		
		// Sound s=Array.Find(sounds,sound=>sound.name==currentTheme.ToString());
		// s.source.volume=s.volume*master/100;
		
		
		
		if(audioSource==null)
		{
			Debug.Log("audiosource null in start");
		}
	}
	void Update()
	{

		
		if(!audioSource.isPlaying)
		{	
		
		int x=UnityEngine.Random.Range(1,4);
		while(("Theme_"+x.ToString())==currentTheme)
		 {
		 	x=UnityEngine.Random.Range(1,4);
		 }
		currentTheme="Theme_"+x.ToString();
		Play(currentTheme,true);
		
		}
		for(int i=0;i<sounds.Length;i++)
			{	
				_sound=sounds[i];
				if(_sound.source.volume!=0)
				{	
					
					_sound.source.volume=_sound.volume*master/100;
				}
			}
	}
	// Update is called once per frame
	public void Play(string name, bool isTheme)
	{
		Sound s=Array.Find(sounds,sound=>sound.name==name);
		
		if(s==null)
		{
			Debug.LogWarning("Sound "+name +" not found");
		}

		s.source.Play();
		if(isTheme)
		{
			audioSource=s.source;
			musicmute=false;
		}
	}
	public void StopMusic()
	{
		Sound s=Array.Find(sounds,sound=>sound.name==currentTheme);
		s.source.volume=0;
		musicmute=true;
	}
	public void ResumeMusic()
	{
		Sound s=Array.Find(sounds,sound=>sound.name==currentTheme);
		s.source.volume=s.volume*master/100;
		musicmute=false;
	}
	public void StopSound(int x)
	{
		Sound s=sounds[x];
		s.source.volume=0;
	}
	public void StopAllSounds()
	{	
		for(int i=4;i<sounds.Length;i++)
		{
			StopSound(i);
		}
	}
	public void ResumeSound(int x)
	{
		Sound s=sounds[x];
		s.source.volume=s.volume*master/100;
	}
	public void ResumeAllSounds()
	{
		for(int i=4;i<sounds.Length;i++)
		{
			ResumeSound(i);
		}
	}
	public void MasterVolume(float x)
	{	Sound s;
			master=x;
			
		
			// for(int i=0;i<sounds.Length;i++)
			// {	
			// 	s=sounds[i];
			// 	if(s.source.volume!=0)
			// 	{	
					
			// 		s.source.volume=s.volume*master/100;
			// 	}
			// }
		
		
	}
}
