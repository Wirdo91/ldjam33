using UnityEngine;
using System.Collections;

public class MovingSaw : MonoBehaviour
{
    bool left = true;
	// Update is called once per frame
	void Update ()
    {
        if (transform.localPosition.y <= -2.5f && left)
        {
            left = false;
        }
        else if (transform.localPosition.y >= 2.5f && !left)
        {
            left = true;
        }

        if (!left)
        {
            this.transform.Translate(Vector3.up * 5 * Time.deltaTime, Space.World);
        }
        else
        {
            this.transform.Translate(Vector3.down * 5 * Time.deltaTime, Space.World);
        }

	}
}
