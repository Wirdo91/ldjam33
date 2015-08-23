using UnityEngine;
using System.Collections;

public class AngryMob : MonoBehaviour {

    PlayerController _player;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();

	}
	
	// Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.right * (_player.transform.position.x - (_player._speed / 3));
	}
}
