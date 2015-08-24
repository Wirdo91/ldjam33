using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject _player;
    [SerializeField]
    GameObject _particles;

    private Vector2 _force = new Vector2(0, 30);
    private Rigidbody2D _playerRigidbody;
    private bool _grounded;
    Camera _gameCamera;
    [SerializeField]
    private GameObject _canvasGroup;
	[SerializeField]
    private float _speed;
    private bool _dead;
	private int _powerUpType;
    public float powerUpTimer = 5;
    private bool _powerUped;
    private float _health = 3;
    [SerializeField]
    private Image healthBar;
	private bool _invincible;
	private float _invincibleTimer = 2;
	private bool hurt;
	private float maxHealth = 3;

	public bool Invincible {
		get {
			return _invincible;
		}
		set {
			_invincible = value;
		}
	}
	
	public float Speed {
		get {
			return _speed;
		}
		set {
			_speed = value;
		}
	}

	public int PowerUpType {
		get{
			return _powerUpType;
		}
		set{
			_powerUpType = value;
		}
	}

	public bool Dead {
		get{
			return _dead;
		}
		set{
			_dead = value;
		}
	}

	public float Health {
		get{
			return _health;
		}

		set{
			if(value == 0)
			{
				ShowGameOver();
			}
			_health = value;
			healthBar.fillAmount = Health/maxHealth;

		}
	}
    void Start()
    {

		//show menu
		//do not walk
		_player = this.gameObject;
        _grounded = true;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _gameCamera = Camera.main;
        _playerRigidbody.freezeRotation = true;
        _powerUped = false;

        


    }
    void Update()
    {
		//walk
		//the start wait untill you have pressed jump before you start

		if (_dead) {
			ShowGameOver();
			if (Input.GetKeyDown(KeyCode.Space))
			{
				//just restart scene
				Application.LoadLevel(Application.loadedLevel);
			}
			return;
		}
		
		_gameCamera.transform.position = new Vector3(this.transform.position.x + 4, -0.5f, -10);
        _particles.transform.position = new Vector3(this.transform.position.x + 14, 0, 0);
        _player.transform.Translate(Vector2.right * _speed * Time.deltaTime);
        
        if (_powerUped)
        {
            powerUpTimer -= Time.deltaTime;
        }
        if (powerUpTimer <= 0)
        {
            _speed = 10;
        }

		if (Invincible == true) 
		{
			_invincibleTimer -= Time.deltaTime;

		}
		if (_invincibleTimer <= 0) 
		{
			Invincible = false;
			_invincibleTimer = 2;
		}

        if (this._playerRigidbody.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
        }

		if (hurt && !Invincible) 
		{
			//take dmg
			Health -= 1;
			healthBar.fillAmount = Health / maxHealth;
			Invincible = true;
			Debug.Log("invis in last update" + Invincible);
			hurt = false;
		}

		CheckDeath();
    }

    void LateUpdate()
    {
		if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            Jump(_force);
            _grounded = false;
        }
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "spike" && Health > 0 && Invincible == false)
        {
			hurt = true;
        }

        if (collision.gameObject.tag == "platform")
        {
            _grounded = true;
        }
    }

    void ShowGameOver()
    {
        _canvasGroup.SetActive(true);
		_canvasGroup.transform.FindChild ("GameOver").gameObject.SetActive (true);
		FindObjectOfType<BackgroundController> ().enabled = false;
        //Destroy (_player);
        //destroy world gen?
    }

    void CheckDeath()
    {
		if (_dead) 
		{
			ShowGameOver();
		}
        if (Health == 0)
        {
			ShowGameOver();
            _dead = true;

        }           

        if (_player.transform.position.y <= -6)
        {
            //TODO: Call gameover
			ShowGameOver();
            _canvasGroup.SetActive(true);
            _dead = true;

        }


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Powerup>() != null)
        {
			//se om det er invici eller speed up
			this.PowerUpType = 1;
            collider.GetComponent<Powerup>().affect(this);
            _powerUped = true;
        }

        if (collider.name == "Angry Mob")
        {
            Health = 0;
            _dead = true;
        }
        else if (collider.GetComponent<WeaponMove>() != null)
            hurt = true;
    }

    void Jump(Vector2 force)
    {
        _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
