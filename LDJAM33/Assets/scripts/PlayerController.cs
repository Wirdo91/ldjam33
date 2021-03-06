﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject _player;
    [SerializeField]
    GameObject _particles;

    private Vector2 _force = new Vector2(0, 50);
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
	private bool _hurt;
	private float maxHealth = 3;
	private Animator _animator;
	private float _score;
	private Text _scoreText;
	private Powerup _powerUp;

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
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);

		//walk
		//the start wait untill you have pressed jump before you start

		if (Dead) {
			CheckDeath();

			return;
		}

		_score = this.transform.position.x;
		_scoreText = GameObject.Find("score").GetComponent<Text>();
		_scoreText.text = "score: " + ((int)_score * 10);

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
			_hurt = false;
		}
		if (_invincibleTimer <= 0) 
		{
			Invincible = false;
			_invincibleTimer = 2;
		}
        if (powerUpTimer <= 0)
		{
			_powerUp.Reset(this);
			Speed = 10;
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

		if (_hurt && !Invincible) 
		{
			//take dmg
			Health -= 1;
			healthBar.fillAmount = Health / maxHealth;
			//be invincible
			Invincible = true;
			_hurt = false;
			//be slower
			this.Speed--;
			_powerUped = true;
			powerUpTimer = 1;
		}

		CheckDeath();
    }

    void LateUpdate()
    {
		if (Input.GetKeyDown(KeyCode.Space) && _grounded && !Dead)
        {
            Jump(_force);
            _grounded = false;
        }
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "platform")
        {
			if(_animator != null)
			{
				_animator.SetTrigger("NotFalling");
				_animator.ResetTrigger("IsFalling");
				_animator.ResetTrigger("IsJumping");

			}
			_grounded = true;
			
		}
	}

    bool ScoreSaved = false;
	void ShowGameOver()
    {
        _canvasGroup.SetActive(true);
		_canvasGroup.transform.FindChild ("GameOver").GetComponent<Text> ().enabled = true;
		FindObjectOfType<BackgroundController> ().enabled = false;

        if (!ScoreSaved)
        {
            FindObjectOfType<Highscore>().AddScore((int)_score * 10);
            ScoreSaved = true;
        }
        //Destroy (_player);
        //destroy world gen?
    }

    void CheckDeath()
    {
        if (Health == 0)
        {
            Dead = true;

        }  
		if (Dead) 
		{
			ShowGameOver();
            _animator.SetBool("IsDead", true);
			if (Input.GetKeyDown(KeyCode.Return))
			{
				//just restart scene
				Application.LoadLevel(Application.loadedLevel);
			}
		}         

        if (_player.transform.position.y <= -6)
        {
            //TODO: Call gameover
			ShowGameOver();
            _canvasGroup.SetActive(true);
            Dead = true;

        }


    }

    void OnTriggerEnter2D(Collider2D collider)
    {

		if (collider.gameObject.tag == "spike" && Health > 0 && Invincible == false)
		{
			_hurt = true;

			//call powerup and be slower

			//be slower

		}

		
		if (collider.GetComponent<Powerup>() != null)
        {
			//se om det er invici eller speed up

			_powerUp = collider.GetComponent<Powerup>();
			_powerUp.Affect(this);
			_powerUped = true;
        }

        if (collider.name == "Angry Mob") {
			Health = 0;
			Dead = true;
		} else if (collider.GetComponent<WeaponMove> () != null) {
			_hurt = true;

		}
    }

    void Jump(Vector2 force)
    {
        _animator.SetTrigger("IsJumping");
        _animator.ResetTrigger("IsFalling");
        _animator.ResetTrigger("NotFalling");
        _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
