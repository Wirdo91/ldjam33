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
    public float _speed;
    private bool _dead;
    public float powerUpTimer = 5;
    private bool _powerUped;
    private float _health = 3;
    [SerializeField]
    private Image healthBar;
	private bool invincible;
	private float invincibleTimer = 2;
	private bool hurt;
	private float maxHealth = 3;

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
		}
	}
	Transform textbox;
    void Start()
    {
        _player = this.gameObject;
        _grounded = true;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _gameCamera = Camera.main;
        _playerRigidbody.freezeRotation = true;
        _dead = false;
        _powerUped = false;
        textbox = _canvasGroup.transform.FindChild("GameOver");
        textbox.gameObject.SetActive(false);

    }
    void Update()
    {

		if (_dead) {
			ShowGameOver();
			if (Input.GetKeyDown(KeyCode.Space))
			{
				//just restart scene
				Application.LoadLevel(Application.loadedLevel);
			}
			return;
		}
		
		_gameCamera.transform.position = new Vector3(this.transform.position.x + 6, 0, -10);
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

		if (invincible == true) 
		{
			invincibleTimer -= Time.deltaTime;

		}
		if (invincibleTimer <= 0) 
		{
			invincible = false;
		}

        if (this._playerRigidbody.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
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

		if (hurt) 
		{
			//take dmg
			//Just be invicible during 2 secs
			Health -= 1;
			healthBar.fillAmount = Health / maxHealth;
			invincible = true;
			hurt = false;
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "spike" && Health > 0 && invincible == false)
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
        textbox.gameObject.SetActive(true);
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
            collider.GetComponent<Powerup>().affect(this);
            _powerUped = true;
        }
    }

    void Jump(Vector2 force)
    {
        _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
