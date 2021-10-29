using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _gold;
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 3;
        _maxhp = 3;
        _moveSpeed = 8.0f;
        _attack = 35;
        _gold = 0;
    }

    protected override void OnDead()
    {
        Debug.Log("Player Dead");
        gameObject.GetComponent<BaseController>().State = Define.State.Die;
    }
}
