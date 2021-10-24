using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }
    protected override void UpdateMoving()
    {
        // �̵�
        transform.position += Vector3.down * Time.deltaTime * _stat.MoveSpeed;

        // ȭ�� ������ ������ ����
        if(transform.position.y <= -8.0f)
        {
            State = Define.State.Die;
        }
    }

    protected override void UpdateDie()
    {
        // Magnet ����
        // 30�� Ȯ��
        int magnetRand = Random.Range(1, 11);
        if(magnetRand <= 3)
        {
            GameObject magnet = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Magnet");
            magnet.transform.position = gameObject.transform.position;
        }
        // Coin ����
        // 50�� Ȯ���� 2��
        int rand = Random.Range(1, 3);
        for (int i = 0; i < rand; i++)
        {
            GameObject coin = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Coin");
            coin.transform.position = gameObject.transform.position;
        }

        Managers.Game.Despawn(gameObject);  
    }
}
