using UnityEngine;
using System.Collections;

public class AngryMob : MonoBehaviour {

    PlayerController _player;

    float _baseSpeed;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();

        this.transform.position = new Vector3((_player.transform.position.x - 3), -4, 0);
        _baseSpeed = _player._speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (_player._speed == _baseSpeed)
            this.transform.Translate(Vector3.right * _baseSpeed * Time.deltaTime);
        else
            this.transform.Translate(Vector3.right * (_player._speed * .75f) * Time.deltaTime);
	}
}
