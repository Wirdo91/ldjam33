﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject player;
	private Vector2 _force = new Vector2(0,50);
	private Rigidbody2D _playerRigidbody;
	private bool _grounded;
	public BoxCollider2D platform;

	public float _speed;
	// Use this for initialization

	void Start () {
		player = this.gameObject;
		_grounded = true;
		_playerRigidbody = GetComponent<Rigidbody2D> ();


	}
	
	// Update is called once per frame
	void Update () {

		player.transform.Translate(Vector2.right * _speed * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.Space) && _grounded) 
		{

			Jump(_force);

		}


	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "player") 
		{
			_grounded = true;
		}
	}

	void Jump(Vector2 force)
	{

		_playerRigidbody.AddForce (force,ForceMode2D.Impulse);
	}
}
