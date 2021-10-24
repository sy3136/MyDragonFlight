using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnController : BaseController
{
    float PlayerPosX;
    float time = 0;
    float twinkleTime = 0.35f;

    private float _speed = 0.8f;
    private float _meteoSpeed = 8.0f;
    private float _skillTime = 2.0f;

    GameObject mark;
    GameObject line;
    GameObject Meteo;

    SpriteRenderer markSprite;
    SpriteRenderer lineSprite;

    GameObject _player;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Warn;
        transform.position = new Vector3(Random.Range(-2.5f, 2.5f),0,0);
        mark = transform.GetChild(0).gameObject;
        line = transform.GetChild(1).gameObject;
        Meteo = transform.GetChild(2).gameObject;

        markSprite = mark.GetComponent<SpriteRenderer>();
        lineSprite = line.GetComponent<SpriteRenderer>();

        StartCoroutine(CoSkillMeteo(_skillTime));

        // Moving
        _player = Managers.Game.GetPlayer();
        if (_player == null)
            return;
    }

    protected override void UpdateMoving()
    {
        if(mark.activeSelf && line.activeSelf)
        {
            // Twinkle
            if (time < twinkleTime)
                markSprite.color = new Color(1, 1, 1, 1 - time * (1 / twinkleTime));
            else
            {
                markSprite.color = new Color(1, 1, 1, time);
                if (time > 2 * twinkleTime)
                    time = 0;
            }
            time += Time.deltaTime;

            if (_player == null)
                return;

            PlayerPosX = _player.transform.position.x;
            Vector3 dest = new Vector3(PlayerPosX - transform.position.x, 0, 0).normalized;
            transform.Translate(dest * _speed * Time.deltaTime);
        }
        //Meteo
        else
        {
            Meteo.transform.Translate(Vector3.down * _meteoSpeed * Time.deltaTime);
            Meteo.transform.GetChild(0).rotation = Quaternion.Euler(0,0,time * 30.0f);
            if (Meteo.transform.position.y <= -7.0f)
                State = Define.State.Die;
            time += Time.deltaTime;
        }
    }
    IEnumerator CoSkillMeteo(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        //State = Define.State.Die;
        mark.SetActive(false);
        line.SetActive(false);
        Meteo.SetActive(true);
        time = 0.0f;
    }
    protected override void UpdateDie()
    {
        Managers.Game.Despawn(gameObject);
        //Effect³Ö±â
    }
}
