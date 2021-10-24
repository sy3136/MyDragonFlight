using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    PlayerStat _stat;
    public bool isMagnetState = false;
    Coroutine coMagnet;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateMoving()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime *  _stat.MoveSpeed);
            //transform.position += Vector3.left * Time.deltaTime * _stat.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _stat.MoveSpeed);
            //transform.position += Vector3.right * Time.deltaTime * _stat.MoveSpeed;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.name.Contains("Magnet"))
            {
                if (coMagnet != null)
                {
                    isMagnetState = false;
                    StopCoroutine(coMagnet);
                }

                coMagnet = StartCoroutine(CoMagnet());
            }
            else if (collision.name.Contains("Coin"))
            {
                // ���� ������Ʈ
            }
            return;
        }
        State = Define.State.Die;
    }
    protected override void UpdateDie()
    {
        Managers.Game.Despawn(gameObject);
        //Effect�ֱ�
    }
    IEnumerator CoMagnet()
    {
        isMagnetState = true;
        Debug.Log("StartCoMagnet!");
        yield return new WaitForSeconds(10.0f);
        Debug.Log("StopCoMagnet!");
        isMagnetState = false;
    }
}
