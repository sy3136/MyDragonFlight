using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxhp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxhp = 100;
        _attack = 1;
        _defense = 0;
        _moveSpeed = 7.0f;

        if (name == "Boss")
        {
            Hp = 1000;
            MaxHp = 1000;
            Defense = 10;
            _moveSpeed = 3.0f;
        }
    }
    public virtual void OnAttacked(int attackDamage)
    {
        int damage = Mathf.Max(0, attackDamage - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }
    protected virtual void OnDead()
    {
        PlayerStat playerStat = GetComponent<Stat>() as PlayerStat;
        if (playerStat != null)
        {
            //score
        }
        gameObject.GetComponent<BaseController>().State = Define.State.Die;
    }
}