using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	float timer = 5;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}
	public void affect(PlayerController player)
	{
		//Kun efter noget tid...

		player._speed += 10;

		Destroy(this.gameObject);
	}

}
