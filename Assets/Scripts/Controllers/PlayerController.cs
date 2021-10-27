using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : BaseController
{
    PlayerStat _stat;
    public bool isMagnetState = false;
    Coroutine coMagnet;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
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
                // 점수 업데이트
                GameObject go = GameObject.Find("CoinUI");
                if (PlayerPrefs.HasKey("Coin"))
                {
                    int coinCount = PlayerPrefs.GetInt("Coin");

                    PlayerPrefs.SetInt("Coin", coinCount + 1);
                    go.GetComponent<TMP_Text>().text = $"<sprite=0>{coinCount+1}";
                }
            }
        }
        if (collision.CompareTag("Meteo"))
        {
            Managers.Sound.Play("HIT");
            GetComponent<PlayerStat>().OnAttacked(999);
        }
    }
    protected override void UpdateDie()
    {
        //Effect넣기
        Managers.Game.Despawn(gameObject);
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
