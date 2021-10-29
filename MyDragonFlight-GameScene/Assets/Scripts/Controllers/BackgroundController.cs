using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : BaseController
{
    public GameObject[] backgrounds;
    float topPosY = 0f;
    float botPosY = 0f;
    float yScreenHalfSize;
    float _speed = 5.0f;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.BackGround;
        State = Define.State.Moving;
        yScreenHalfSize = Camera.main.orthographicSize;
        topPosY = yScreenHalfSize * 2 * backgrounds.Length;
        botPosY = -(yScreenHalfSize * 2);
    }
    protected override void UpdateMoving()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += new Vector3(0, -_speed, 0) * Time.deltaTime;

            if (backgrounds[i].transform.position.y < botPosY)
            {
                Vector3 tmpPos = backgrounds[i].transform.position;
                backgrounds[i].transform.position = new Vector3(tmpPos.x, tmpPos.y + topPosY, 0);
            }
        }
    }
}
