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
        _hp = 100;
        _maxhp = 100;
        _moveSpeed = 8.0f;
        _attack = 50;
        _gold = 0;
    }

    protected override void OnDead()
    {
        Debug.Log("Player Dead");
    }
}
