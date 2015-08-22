using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour
{
    [SerializeField]
    GameObject _platForm;
    [SerializeField]
    Vector2 _spawnEgde;

    ObjectPool _platforms;

    [SerializeField]
    GameObject _player;

	// Use this for initialization
	void Start ()
    {
        _platforms = new ObjectPool(_platForm, this.transform);
        for (int i = (int)_spawnEgde.x; i < -_spawnEgde.x + 1; i++)
        {
            _platforms.Spawn(new Vector3(i, _spawnEgde.y, 0));
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (_platforms.ActiveObject[0].transform.position.x < _spawnEgde.x + Mathf.Round(_player.transform.position.x))
        {
            _platforms.Despawn(_platforms.ActiveObject[0]);
        }
        if (Mathf.Round(_player.transform.position.x) + -_spawnEgde.x > _platforms.ActiveObject[_platforms.ActiveObject.Count - 1].transform.position.x)
        {
            _platforms.Spawn(new Vector3(Mathf.Round(_player.transform.position.x) + -_spawnEgde.x, _spawnEgde.y, 0));
        }
	}
}
