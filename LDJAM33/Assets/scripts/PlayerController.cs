using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Game Objects")]
	public GameObject _player;
    [SerializeField]
    GameObject _particles;

    private Vector2 _force = new Vector2(30, 0);
	private Rigidbody2D _playerRigidbody;
	private bool _grounded;
    Camera _gameCamera;
	[SerializeField]
	private GameObject _canvasGroup;
	public float _speed;
	private bool _dead;
	public float powerUpTimer = 5;
	private bool _powerUped;
	private int health = 3;
	[SerializeField]
	private Image healthBar;


	void Start ()
    {
		_player = this.gameObject;
		_grounded = true;
		_playerRigidbody = GetComponent<Rigidbody2D> ();
        _gameCamera = Camera.main;
        _playerRigidbody.freezeRotation = true;
		_dead = false;
		_powerUped = false;
		Transform textbox = _canvasGroup.transform.FindChild("GameOver");
		textbox.gameObject.SetActive(false);

    }
	void Update ()
    {
        _gameCamera.transform.position = new Vector3(this.transform.position.x + 6, 0, -10);
        _particles.transform.position = new Vector3(this.transform.position.x + 14, 0, 0);
		_player.transform.Translate(Vector2.right * _speed * Time.deltaTime);
		CheckDeath ();
		if (_powerUped) 
		{
			powerUpTimer -= Time.deltaTime;
		}
		if (powerUpTimer <= 0) 
		{
			_speed = 10;
		}

        if (this._playerRigidbody.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
        }
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

		if (collision.gameObject.tag == "spike" && health > 0) 
		{
			//take dmg
			health -= 1;
			healthBar.fillAmount = 1/health;
			
			
		}

		if (collision.gameObject.tag == "platform")
		{
			_grounded = true;
		}
	}

	void ShowGameOver()
	{
		Transform textbox = _canvasGroup.transform.FindChild("GameOver");
		textbox.gameObject.SetActive(true);
		Destroy (player);
	}

	void CheckDeath()
	{

		if(player.transform.position.y <= -6)
		{

			//Call gameover
			ShowGameOver();
			_dead = true;
		}
		if(health == 0)
		{
			_dead = true;
			ShowGameOver();

		if(_player.transform.position.y <= -6)
		{
			//TODO: Call gameover
			_canvasGroup.alpha = 1;
			_dead = true;

		}

		if (Input.GetKeyDown(KeyCode.Space) && _dead == true)
		{
			//just restart scene
			Application.LoadLevel(Application.loadedLevel);		
		}
	}



	void OnTriggerEnter2D(Collider2D collider)
	{



		if (collider.GetComponent<Powerup> () != null) 
		{
			collider.GetComponent<Powerup>().affect(this);
			_powerUped = true;
		}
	}

	void Jump(Vector2 force)
	{
		_playerRigidbody.AddForce(force, ForceMode2D.Impulse);
	}
}
