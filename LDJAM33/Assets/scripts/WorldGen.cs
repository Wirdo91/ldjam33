﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldGen : MonoBehaviour
{
    System.Random RandomGen = new System.Random();
    [Header("Spawnable Objects")]
    [SerializeField]
    GameObject _floorPlatform;
    [SerializeField]
    GameObject _platform;
    [SerializeField]
    GameObject _spike;
    [SerializeField]
    GameObject _villager;
    [SerializeField]
    GameObject _sawblade;
    [SerializeField]
    GameObject _movingSawBlade;
	[SerializeField]
	int _powerUpSpawnTimer = 2;
	[SerializeField]
	GameObject[] _powerUps;


    ObjectPool _floorPlatforms;
    ObjectPool _platforms;
    ObjectPool _spikes;
    ObjectPool _villagers;
    ObjectPool _sawblades;
    ObjectPool _movingSawBlades;

    [Space(1)]
    [Header("Player and Offset")]
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Vector2 _spawnEgde;
    [SerializeField]
    float _cameraOffset;
    [SerializeField]
    float _floorOffset;

    int _platformLevel = 0;

    float _spawnTimer = 0;
    float _waitTimer = 2;

	// Use this for initialization
    void Start()
    {
        _floorPlatforms = new ObjectPool(_floorPlatform, this.transform);
        _platforms = new ObjectPool(_platform, this.transform);
        _spikes = new ObjectPool(_spike, this.transform);
        _villagers = new ObjectPool(_villager, this.transform);
        _sawblades = new ObjectPool(_sawblade, this.transform);
        _movingSawBlades = new ObjectPool(_movingSawBlade, this.transform);


        for (int i = (int)(_spawnEgde.x / _floorOffset); i < (-_spawnEgde.x * 2.0f) / _floorOffset; i++)
        {
            _floorPlatforms.Spawn(new Vector3(i * 8, _spawnEgde.y, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetComponent<PlayerController>().Dead || !_player.GetComponent<PlayerController>().enabled)
            return;

        _spawnTimer += Time.deltaTime;

        //FLOOR
        if (_floorPlatforms.ActiveObject.Count != 0 && _floorPlatforms.ActiveObject[0].transform.position.x / 8 + 1 < Mathf.Round(_player.transform.position.x / 8))
        {
            _floorPlatforms.Despawn(_floorPlatforms.ActiveObject[0]);
        }
        if (_floorPlatforms.ActiveObject.Count != 0 && Mathf.Round((_player.transform.position.x + _floorOffset) / _floorOffset) >= (_floorPlatforms.ActiveObject[_floorPlatforms.ActiveObject.Count - 1].transform.position.x) / _floorOffset)
        {
            _floorPlatforms.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + (_floorOffset * 2) + (_floorOffset / 2), _spawnEgde.y, 0));
        }

        //PLATFORMS
        if (_platforms.ActiveObject.Count != 0 && _platforms.ActiveObject[0].transform.position.x < _spawnEgde.x + Mathf.Round(_player.transform.position.x))
        {
            _platforms.Despawn(_platforms.ActiveObject[0]);
        }

        //SPIKES
        if (_spikes.ActiveObject.Count != 0 && _spikes.ActiveObject[0].transform.position.x < _spawnEgde.x + _cameraOffset + Mathf.Round(_player.transform.position.x))
        {
            _spikes.Despawn(_spikes.ActiveObject[0]);
        }

        //Villages
        if (_villagers.ActiveObject.Count != 0 && _villagers.ActiveObject[0].transform.position.x < _spawnEgde.x + _cameraOffset + Mathf.Round(_player.transform.position.x))
        {
            _villagers.Despawn(_villagers.ActiveObject[0]);
        }

        //Sawblade
        if (_sawblades.ActiveObject.Count != 0 && _sawblades.ActiveObject[0].transform.position.x < _spawnEgde.x + _cameraOffset + Mathf.Round(_player.transform.position.x))
        {
            _sawblades.Despawn(_sawblades.ActiveObject[0]);
        }

        //MovingSawblade
        if (_movingSawBlades.ActiveObject.Count != 0 && _movingSawBlades.ActiveObject[0].transform.position.x < _spawnEgde.x + _cameraOffset + Mathf.Round(_player.transform.position.x))
        {
            _movingSawBlades.Despawn(_movingSawBlades.ActiveObject[0]);
        }

        if (_spawnTimer >= _waitTimer)
        {
            CreateObstacle();
        }
    }

    void CreateObstacle()
    {
        int gen = RandomGen.Next(0,6);
		_powerUpSpawnTimer--;
        switch (gen) {
		case 0:
			for (int i = 0; i < 3; i++) {
				for (int j = 1; j < 4; j++) {
                    _platforms.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + i + j * 8, _spawnEgde.y + j * 1.5f + 0.5f, 0));
						
				}
			}
			if (_powerUpSpawnTimer <= 0) {
				int place = RandomGen.Next (1, 4);
				int index = RandomGen.Next(0, _powerUps.Length);
				Instantiate (_powerUps[index], new Vector3 (Mathf.Round (_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + 2 + place * 8, _spawnEgde.y + place * 1.5f + 2, 0),Quaternion.identity);

				_powerUpSpawnTimer = RandomGen.Next (0, 3);
			}
                _waitTimer = 3;
                break;
            case 1:
                for (int j = 0; j < 4; j++)
                {
                    _spikes.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2) + ((float)j / 2.0f), _spawnEgde.y + 0.84f , 0));
                }
                _waitTimer = 1.5f;
                if (_powerUpSpawnTimer <= 0)
                {
                    int place = RandomGen.Next(1, 4);
                    int index = RandomGen.Next(0, _powerUps.Length);
                    Instantiate(_powerUps[index], new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2) + ((float)3 / 2.0f), _spawnEgde.y + 0.84f + 2, 0), Quaternion.identity);

                    _powerUpSpawnTimer = RandomGen.Next(0, 3);
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        if (j == 2)
                        {
                            _spikes.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + i + j * 6, _spawnEgde.y + 3.2f, 0));
                        }
                        else
                        {
                            _spikes.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + i + j * 6, _spawnEgde.y + 2.3f, 0), Quaternion.Euler(0, 0, 180));
                        }
                        _platforms.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + i + j * 6, _spawnEgde.y + 2.8f, 0));
                    }
                }
                _waitTimer = 3;
                if (_powerUpSpawnTimer <= 0)
                {
                    int place = RandomGen.Next(1, 4);
                    int index = RandomGen.Next(0, _powerUps.Length);
                    Instantiate(_powerUps[index], new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + -_spawnEgde.x + (8 * 6), _spawnEgde.y + 2.8f + 1, 0), Quaternion.identity);

                    _powerUpSpawnTimer = RandomGen.Next(0, 3);
                }
                break;
            case 3:
                _villagers.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2), _spawnEgde.y + 0.84f, 0)).GetComponent<IdleVillager>().Init();
                _waitTimer = 1;
                _powerUpSpawnTimer++;
                break;
            case 4:
                _sawblades.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2), _spawnEgde.y + 0.5f, 0), Quaternion.Euler(0, 180, 0));
                _waitTimer = 1;
                _powerUpSpawnTimer++;
                break;
            case 5:
                _movingSawBlades.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2), _spawnEgde.y + 3f, 0));
                _waitTimer = 1;
                if (_powerUpSpawnTimer <= 0)
                {
                    int place = RandomGen.Next(1, 4);
                    int index = RandomGen.Next(0, _powerUps.Length);
                    Instantiate(_powerUps[index], new Vector3(Mathf.Round(_player.transform.position.x) + _cameraOffset + (-_spawnEgde.x * 2), _spawnEgde.y + 3f + UnityEngine.Random.Range(-2f, 3), 0), Quaternion.identity);

                    _powerUpSpawnTimer = RandomGen.Next(0, 3);
                }
                break;
            //case 6:
            //    break;
            //case 7:
            //    break;
            //case 8:
            //    break;
            //case 9:
            //    break;
        }

        _spawnTimer = 0;
    }
}
