﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject player;
	private Vector2 _force = new Vector2(0,40);
	private Rigidbody2D _playerRigidbody;
	private bool _grounded;
    Camera _gameCamera;
	[SerializeField]
	private CanvasGroup _canvasGroup;
	public float _speed;
	private bool _dead;
	[SerializeField]
	private GameObject spawner;

	void Start ()
    {
		player = this.gameObject;
		_grounded = true;
		_playerRigidbody = GetComponent<Rigidbody2D> ();
        _gameCamera = Camera.main;
        _playerRigidbody.freezeRotation = true;
		_dead = false;


    }

    void FixedUpdate()
    {

    }

	void Update ()
    {
        _gameCamera.transform.position = new Vector3(this.transform.position.x + 6, 0, -10);
		player.transform.Translate(Vector2.right * _speed * Time.deltaTime);
		CheckDeath ();

	}

    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _grounded = false;
            Jump(_force);

        }
    }


	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "platform")
		{
			_grounded = true;
		}
	}

	void CheckDeath()
	{
		if(player.transform.position.y <= -6)
		{
			//Call gameover
			_canvasGroup.alpha = 1;
			_dead = true;


		}

		if (Input.GetKeyDown(KeyCode.Space) && _dead == true)
		{
			Application.LoadLevel("RunnerScene");
		}
	}


	void Jump(Vector2 force)
	{
		_playerRigidbody.AddForce(force,ForceMode2D.Impulse);
	}
}
