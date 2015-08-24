using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	static MusicPlayerScript instance = null;

	private AudioSource intro;
	private AudioSource loop;
	private int i;


	public static MusicPlayerScript Instance
	{
		get {return instance;}
	}

	// Use this for initialization
	void Start () 
	{
		int i = 0;
		StartAudio ();
	}

	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	void StartAudio()
	{
		intro = (AudioSource)gameObject.AddComponent<AudioSource> ();
		AudioClip myAudioClip;

		myAudioClip = (AudioClip)Resources.Load ("Music/Run Frankenstein Run Intro");
		intro.clip = myAudioClip;

		intro.Play ();
	}

	void Update()
	{
		if (!intro.isPlaying && i == 0)
		{
			i++;

			loop = (AudioSource)gameObject.AddComponent<AudioSource> ();
			AudioClip myAudioClip;
			
			myAudioClip = (AudioClip)Resources.Load ("Music/Run Frankenstein Run Loop");
			loop.clip = myAudioClip;
			loop.loop = true;

			loop.Play();
		}
	}
}
