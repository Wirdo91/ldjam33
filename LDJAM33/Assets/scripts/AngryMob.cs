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

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();

        this.transform.position = new Vector3((_player.transform.position.x - 3.5f), -4.3f, 0);
        _baseSpeed = _player.Speed;
	}

	// Update is called once per frame
	void Update () {
        if (_player.Speed == _baseSpeed)
            this.transform.Translate(Vector3.right * _baseSpeed * Time.deltaTime);
        else
            this.transform.Translate(Vector3.right * (_player.Speed * .75f) * Time.deltaTime);

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
