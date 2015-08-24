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
	private Animator _animator;
	private float _score;
	private Text _scoreText;

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
		_animator = GetComponent<Animator> ();
		_score = 0;

		
		


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

		_score = this.transform.position.x;
		_scoreText = GameObject.Find("score").GetComponent<Text>();
		_scoreText.text = "score: " + (_score * 10);

		_gameCamera.transform.position = new Vector3(this.transform.position.x + 4, -0.5f, -10);
        _particles.transform.position = new Vector3(this.transform.position.x + 14, 0, 0);
        _player.transform.Translate(Vector2.right * _speed * Time.deltaTime);
        
        if (_powerUped)
        {
            powerUpTimer -= Time.deltaTime;
        }
		if (Invincible) 
		{
			_invincibleTimer -= Time.deltaTime;
		}
		if (_invincibleTimer <= 0) 
		{
			Invincible = false;
			_invincibleTimer = 2;
		}
        if (powerUpTimer <= 0)
		{
			switch (_powerUpType) {
			case(1):
				//reset speed up
				this.Speed = 10;
				break;
			case(2):
				//reset invincibility
				Debug.Log("reset invinci");
				Invincible = false;
				break;
			case(3):
				//reset slowdown
				this.Speed = 10;
				break;
			case(4):
				//reset giant mode
				this.transform.localScale = new Vector3(2, 2, 0);
				this.transform.localScale = new Vector3(2, 2, 0);
				break;
			default:
			break;
			}
			//reset timer when the power up is done
			powerUpTimer = 5;
			_powerUped = false;
        }

		if(gameObject.transform.position.y > -3)
		{
            _animator.SetTrigger("IsFalling");
            _animator.ResetTrigger("IsJumping");
            _animator.ResetTrigger("NotFalling");
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
			//be invincible
			Invincible = true;
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
            _animator.SetTrigger("NotFalling");
            _animator.ResetTrigger("IsFalling");
            _animator.ResetTrigger("IsJumping");
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
        if (Health == 0)
        {
            _dead = true;

        }  
		if (_dead) 
		{
			ShowGameOver();
            _animator.SetBool("IsDead", true);
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
        _animator.SetTrigger("IsJumping");
        _animator.ResetTrigger("IsFalling");
        _animator.ResetTrigger("NotFalling");
        _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
