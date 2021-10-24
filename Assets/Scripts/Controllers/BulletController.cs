using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseController
{
    GameObject _player;
    private int damage;
    public float _speed = 10.0f;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Bullet;
        _player = Managers.Game.GetPlayer();
        if (_player == null)
            return;
        damage = _player.GetComponent<PlayerStat>().Attack;
        transform.position = _player.transform.position;

        State = Define.State.Moving;
    }
    protected override void UpdateMoving()
    {
        transform.position += Vector3.up * Time.deltaTime * _speed;

        if (transform.position.y >= 6.0f)
            State = Define.State.Die;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteo") || collision.CompareTag("Item"))
            return;
        State = Define.State.Die;
        Stat targetStat = collision.gameObject.GetComponent<Stat>();
        targetStat.OnAttacked(damage);
    }

    protected override void UpdateDie()
    {
        Managers.Game.Despawn(gameObject);
    }
}
