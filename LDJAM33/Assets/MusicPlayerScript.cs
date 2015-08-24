using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	static MusicPlayerScript instance = null;
	
	public static MusicPlayerScript Instance
	{
		get {return instance;}
	}

	// Use this for initialization
	void Start () 
	{
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

	}
}
