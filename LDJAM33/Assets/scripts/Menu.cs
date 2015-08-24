using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	
	private GameObject _player;
	PlayerController Player;

	// Use this for initialization
	void Start () {

		_player = GameObject.FindGameObjectWithTag("Player");
		Player = _player.GetComponent<PlayerController>();
		Player.enabled = false;

		FindObjectOfType<AngryMob> ().enabled = false;
		//check if space is pressed
		//if yes then spawn the player


		this.gameObject.SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			this.gameObject.SetActive(false);
			
			Player.Dead = false;
			FindObjectOfType<BackgroundController> ().Reset();

			FindObjectOfType<AngryMob> ().enabled = true;
			Player.enabled = true;
		}
	}
}
