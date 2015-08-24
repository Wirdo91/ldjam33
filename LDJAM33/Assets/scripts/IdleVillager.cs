using UnityEngine;
using System.Collections;

public class IdleVillager : MonoBehaviour {

    void Start()
    {
        Init();
    }

    int currentAnimation = 0;
	public void Init () {
        currentAnimation = new System.Random().Next(0, 3);

        this.GetComponent<Animator>().SetInteger("Animation", currentAnimation);
	}
}
