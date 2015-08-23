using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldGen : MonoBehaviour
{
    System.Random RandomGen = new System.Random();
    [Header("Spawnable Objects")]
    [SerializeField]
    GameObject _platform;
    [SerializeField]
    GameObject _spike;


    ObjectPool _floorPlatform;
    ObjectPool _platforms;
    ObjectPool _spikes;

    [Space(1)]
    [Header("Player and Offset")]
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Vector2 _spawnEgde;
    [SerializeField]
    float _offset;

    int _platformLevel = 0;

    float _spawnTimer = 0;
    float _waitTimer = 2;

	// Use this for initialization
	void Start ()
    {
        _floorPlatform = new ObjectPool(_platform, this.transform);
        _platforms = new ObjectPool(_platform, this.transform);
        _spikes = new ObjectPool(_spike, this.transform);


        for (int i = (int)_spawnEgde.x; i < -_spawnEgde.x + 1; i++)
        {
            _floorPlatform.Spawn(new Vector3(i + _offset, _spawnEgde.y, 0));
        }
	}

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_floorPlatform.ActiveObject.Count != 0 && _floorPlatform.ActiveObject[0].transform.position.x < _spawnEgde.x + _offset + Mathf.Round(_player.transform.position.x))
        {
            _floorPlatform.Despawn(_floorPlatform.ActiveObject[0]);
        }
        if (_floorPlatform.ActiveObject.Count != 0 && Mathf.Round(_player.transform.position.x) + _offset + -_spawnEgde.x > _floorPlatform.ActiveObject[_floorPlatform.ActiveObject.Count - 1].transform.position.x)
        {
            _floorPlatform.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _offset + -_spawnEgde.x, _spawnEgde.y, 0));
        }

        if (_platforms.ActiveObject.Count != 0 && _platforms.ActiveObject[0].transform.position.x < _spawnEgde.x + _offset + Mathf.Round(_player.transform.position.x))
        {
            _platforms.Despawn(_platforms.ActiveObject[0]);
        }

        if (_spikes.ActiveObject.Count != 0 && _spikes.ActiveObject[0].transform.position.x < _spawnEgde.x + _offset + Mathf.Round(_player.transform.position.x))
        {
            _spikes.Despawn(_spikes.ActiveObject[0]);
        }

        if (_spawnTimer >= _waitTimer)
        {
            CreateObstacle();
        }
    }

    void CreateObstacle()
    {
        int gen = RandomGen.Next(0, 2);

        switch (gen)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        _platforms.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _offset + -_spawnEgde.x + i + j * 6, _spawnEgde.y + j * 2, 0));
                    }
                }
                _waitTimer = 3;
                break;
            case 1:
                for (int j = 0; j < 4; j++)
                {
                    _spikes.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + _offset + -_spawnEgde.x + ((float)j / 2.0f), _spawnEgde.y + 0.84f , 0));
                }
                _waitTimer = 1;
                break;
            //case 2:
            //    break;
            //case 3:
            //    break;
            //case 4:
            //    break;
            //case 5:
            //    break;
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
