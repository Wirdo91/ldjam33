using UnityEngine;
using System.Collections;

public class WeaponMove : MonoBehaviour {

    float _speed = 10f;

	void Start() 
	{
		_speed += FindObjectOfType<PlayerController> ().Speed/2;

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if (!this.GetComponent<SpriteRenderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
	}

}
