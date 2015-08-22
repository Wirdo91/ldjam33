using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    [SerializeField]
    Transform _middleBack;
    [SerializeField]
    Transform _forBack;

    [SerializeField]
    float _middleMoveSpeed;
    [SerializeField]
    float _forMoveSpeed;

    Transform _secondMiddleBack;
    Transform _secondForBack;

    Queue<Transform> _forBacks;
    Queue<Transform> _middleBacks;

    Transform tmp_middle;
	
    void Start()
    {
        _middleBacks = new Queue<Transform>();
        _middleBacks.Enqueue(_middleBack);

        _secondMiddleBack = ((GameObject)Instantiate(_middleBack.gameObject,
            Vector3.zero,
            Quaternion.identity)).transform;

        _secondMiddleBack.parent = _middleBack.parent;
        _secondMiddleBack.localPosition = _middleBack.localPosition + new Vector3(38.35f, 0, 0);

        _middleBacks.Enqueue(_secondMiddleBack);

        _forBacks = new Queue<Transform>();
        _forBacks.Enqueue(_forBack);

        for (int i = 1; i < 3; i++)
        {
            _secondForBack = ((GameObject)Instantiate(_forBack.gameObject,
                Vector3.zero,
                Quaternion.identity)).transform;

            _secondForBack.parent = _forBack.parent;
            _secondForBack.localPosition = _forBack.localPosition + new Vector3(15 * i, 0, 0);
            _secondForBack.GetComponent<SpriteRenderer>().sortingOrder -= i;

            _forBacks.Enqueue(_secondForBack);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (_middleBacks.Peek().localPosition.x < -30)
        {
            Transform tmpTrans = _middleBacks.Dequeue();

            tmpTrans.localPosition = _middleBacks.Peek().localPosition + new Vector3(38.35f, 0, 0);

            _middleBacks.Enqueue(tmpTrans);
        }
        foreach (Transform middleBack in _middleBacks.ToArray())
        {
            middleBack.position += new Vector3(_middleMoveSpeed * player._speed * Time.deltaTime, 0, 0);
        }

        if (_forBacks.Peek().localPosition.x < - 20)
        {
            Transform tmpTrans = _forBacks.Dequeue();

            tmpTrans.localPosition = new Vector3(15.5f, tmpTrans.localPosition.y);

            _forBacks.Enqueue(tmpTrans);
        }

        foreach (Transform forBack in _forBacks.ToArray())
        {
            forBack.position += new Vector3(_forMoveSpeed * player._speed * Time.deltaTime, 0, 0);
        }
    }
}
