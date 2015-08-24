using UnityEngine;
using System.Collections;

public class SawBlade : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(Vector3.back * 2.5f);
	}
}
