using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseController
{
    GameObject _player;
    private int damage;
    public float _speed = 10.0f;
    int level = 1;
    Sprite bulletSprite;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Bullet;
        _player = Managers.Game.GetPlayer();
        if (_player == null)
            return;
        transform.position = _player.transform.position;

        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }

        bulletSprite = Managers.Resource.Load<Sprite>("Images/Weapons/bullet_"+level.ToString("00"));
        GetComponent<SpriteRenderer>().sprite = bulletSprite;
        damage = _player.GetComponent<PlayerStat>().Attack + level * 10;
        
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
