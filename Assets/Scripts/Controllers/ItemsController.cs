using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : BaseController
{

    private float magnetRadius = 3.5f;
    private float magnetForce = 70.0f;
    GameObject _player;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Item;
        if (CompareTag("Item"))
        {
            float randX = Random.Range(-50.0f, 50.0f);

            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX, 10.0f));
        }
        _player = Managers.Game.GetPlayer();
    }
    
    protected override void UpdateMoving()
    {
        if (_player == null)
            return;

        if (_player.GetComponent<PlayerController>().isMagnetState)
        {
            Vector2 dir = _player.transform.position - transform.position;
            float distance = dir.magnitude;
            //float theta = Mathf.Atan2(dir.y, dir.x) * 180f / Mathf.PI;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, theta), step);
            
            if (magnetRadius >= distance)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(dir.normalized * magnetForce * (1 / (distance * distance)));

                // 강제로 (안그러면 이상하게 날라가는 경우 생김)
                if (dir.x < 0.7f && transform.position.y < -4.5f && transform.position.y > -5.0f)
                {
                    transform.position = new Vector2(transform.position.x, -4.5f);
                }
            }

        }

        if (transform.position.y <= -6.5f)
            State = Define.State.Die;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Monster") || collision.CompareTag("Bullet") || collision.CompareTag("Item"))
        //    return;
        if (collision.CompareTag("Player"))
        {
            State = Define.State.Die;
        }
    }
    protected override void UpdateDie()
    {
        Managers.Game.Despawn(gameObject);
        //Effect넣기
    }
}
