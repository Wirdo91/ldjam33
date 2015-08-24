using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	public int powerUpType;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}

	public void Reset(PlayerController player)
	{
		switch (powerUpType) {
		case(1):
			//reset speed up
			player.Speed = 10;
			break;
		case(2):
			//reset invincibility
			Debug.Log("reset invinci");
			player.Invincible = false;
			break;
		case(3):
			//reset slowdown
			player.Speed = 10;
			break;
		case(4):
			//reset giant mode
			player.transform.localScale = new Vector3(2, 2, 0);
			player.transform.localScale = new Vector3(2, 2, 0);
			break;
		default:
			break;
		}

	}

	public void Affect(PlayerController player)
	{
		//Kun efter noget tid...



		switch (this.powerUpType) {
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
			player.Speed -= 0.5f;
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

	static public void SlowDown(PlayerController player)
	{
		player.Speed -= 1f;
		player.powerUpTimer = 1f;
	}

}
