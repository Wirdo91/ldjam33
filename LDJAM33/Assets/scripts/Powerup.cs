using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}
	public void affect(PlayerController player)
	{
		//Kun efter noget tid...



		switch (player.PowerUpType) {
		case(1):
			//speedup
			player.Speed += 10;
			break;
		case(2):
			//invincibility
			player.Invincible = true;
			break;
		default:
		
			break;
		}

		Destroy(this.gameObject);
	}

}
