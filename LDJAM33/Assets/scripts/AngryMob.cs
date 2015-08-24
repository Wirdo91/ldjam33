using UnityEngine;
using System.Collections;

public class AngryMob : MonoBehaviour {

    PlayerController _player;

    System.Random random = new System.Random();

    [SerializeField]
    bool _frenzied = false;

    float _baseSpeed;

	float _speed;

    [SerializeField]
    GameObject _torchPrefab;
    [SerializeField]
    GameObject _pitchforkPrefab;

    float _baseDistance;

    bool behind = false;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
        this.transform.position = new Vector3((_player.transform.position.x - 3), -4, 0);
        _baseSpeed = _player.Speed;
        _baseDistance = _player.transform.position.x - this.transform.position.x;
		_speed = _baseSpeed;
	}

    float debugDistance;
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.right * _speed * Time.deltaTime);

		if (Random.Range (0, 100) == 13) 
		{
			_frenzied = true;
		}

        if (_player.Speed > _baseSpeed)
        {
			_speed = _player.Speed * .75f;
            behind = true;
        }
        else if (_player.transform.position.x - this.transform.position.x <= _baseDistance)
        {
			_speed = _baseSpeed;
            behind = false;
        }

        debugDistance = _player.transform.position.x - this.transform.position.x;

        if (_frenzied)
        {
            GameObject spawnedObject = null;
            switch(random.Next(0, 2))
            {
                case 0:
                    spawnedObject = Instantiate(_torchPrefab);
                    break;
                case 1:
                    spawnedObject = Instantiate(_pitchforkPrefab);
                    break;
            }
            if (spawnedObject != null)
                spawnedObject.transform.position = this.transform.position;

            _frenzied = false;
        }
	}
}
