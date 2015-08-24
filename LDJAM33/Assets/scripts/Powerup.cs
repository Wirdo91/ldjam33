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
		case(3):
			//slowdown
			player.Speed -= 1;
			player.powerUpTimer = 1;
			break;
		case(4):
			//Giant mode
			player.transform.localScale += new Vector3(4, 4, 0);
			player.transform.localScale += new Vector3(4, 4, 0);
			break;
		default:
		
			break;
		}

		Destroy(this.gameObject);
	}

}
