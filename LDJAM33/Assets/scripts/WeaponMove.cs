using UnityEngine;
using System.Collections;

public class WeaponMove : MonoBehaviour {

    [SerializeField]
    float _speed = 10f;
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector2.right * _speed * Time.deltaTime);
	}
}
