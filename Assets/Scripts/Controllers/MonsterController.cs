using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        // Boss 이동
        if(name == "Boss")
        {
            if (transform.position.y <= 5.001f)
                return;
            transform.position += Vector3.down * Time.deltaTime * _stat.MoveSpeed;
        }

        // 이동
        transform.position += Vector3.down * Time.deltaTime * _stat.MoveSpeed;

        // 화면 밖으로 나갈시 삭제
        if(transform.position.y <= -8.0f)
        {
            Managers.Game.Despawn(gameObject);
        }
        
    }

    protected override void UpdateDie()
    {
        // Magnet 생성
        // 20퍼 확률
        int magnetRand = Random.Range(1, 11);
        if(magnetRand <= 2)
        {
            GameObject magnet = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Magnet");
            magnet.transform.position = gameObject.transform.position;
        }
        // Coin 생성
        // 50퍼 확률로 2개
        int randCoin = Random.Range(1, 3);
        for (int i = 0; i < randCoin; i++)
        {
            GameObject coin = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Coin");
            coin.transform.position = gameObject.transform.position;
        }

        GameObject score = GameObject.Find("ScoreUI");
        GameObject highScore = GameObject.Find("HighScoreUI");

        // 점수 업데이트
        int scoreCount = int.Parse(score.GetComponent<TMP_Text>().text);
        score.GetComponent<TMP_Text>().text = $"{++scoreCount}";

        // 최고점수 업데이트
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int highScoreCount = PlayerPrefs.GetInt("HighScore");
            if (scoreCount > highScoreCount)
            {
                PlayerPrefs.SetInt("HighScore", scoreCount);
                highScore.GetComponent<TMP_Text>().text = $"{scoreCount}";
            }
        }
        if(name == "Boss")
        {
            for (int i = 0; i < 25; i++)
            {
                GameObject coin = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Coin");
                coin.transform.position = gameObject.transform.position;
            }
            SpawningPool pool = GameObject.Find("SpawningPool").GetOrAddComponent<SpawningPool>();
            pool.SetKeepMonsterCount(5);
            GameScene gs = GameObject.Find("@Scene").GetComponent<GameScene>();
            gs._bossStage = true;
        }
        Managers.Game.Despawn(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Managers.Sound.Play("HIT");
            collision.GetComponent<PlayerStat>().OnAttacked(_stat.Attack);
        }
    }

}
