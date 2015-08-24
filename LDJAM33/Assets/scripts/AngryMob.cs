using UnityEngine;
using System.Collections;

public class AngryMob : MonoBehaviour {

    PlayerController _player;

    System.Random random = new System.Random();

    [SerializeField]
    bool _frenzied = false;

    float _baseSpeed;

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
	}

	// Update is called once per frame
	void Update () {
        if (_player.Speed == _baseSpeed && !behind)
        {
            this.transform.Translate(Vector3.right * _baseSpeed * Time.deltaTime);
        }
        else if (behind)
        {
            this.transform.Translate(Vector3.right * (_player.Speed * .75f) * Time.deltaTime);
        }

        if (_player.transform.position.x - this.transform.position.x > _baseDistance)
        {
            behind = true;
        }
        else
        {
            behind = false;
        }


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
