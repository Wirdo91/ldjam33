using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject player;
	private Vector2 _force = new Vector2(0,40);
	private Rigidbody2D _playerRigidbody;
	private bool _grounded;
	public BoxCollider2D platform;
    Camera _gameCamera;

	public float _speed;
	// Use this for initialization

	void Start ()
    {
		player = this.gameObject;
		_grounded = true;
		_playerRigidbody = GetComponent<Rigidbody2D> ();
        _gameCamera = Camera.main;
	}

    void FixedUpdate()
    {

    }

	// Update is called once per frame
	void Update ()
    {

        _gameCamera.transform.position = new Vector3(this.transform.position.x + 6, 0, -10);
		player.transform.Translate(Vector2.right * _speed * Time.deltaTime);

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

	void Jump(Vector2 force)
	{
		_playerRigidbody.AddForce(force,ForceMode2D.Impulse);
	}
}
