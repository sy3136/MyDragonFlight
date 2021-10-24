using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;
    GameObject _player;
    GameObject _coin;
    GameObject _highScore;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        _player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        _coin = GameObject.Find("CoinUI");
        _highScore = GameObject.Find("HighScoreUI");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

        // Bullet
        StartCoroutine(CoGenerateBullet());

        // Warn
        StartCoroutine(CoWarn());

        // UI
        if (PlayerPrefs.HasKey("Coin"))
            _coin.GetComponent<TMP_Text>().text = $"<sprite=0>{PlayerPrefs.GetInt("Coin")}";
        else
            PlayerPrefs.SetInt("Coin", 0);

        if (PlayerPrefs.HasKey("HighScore"))
            _highScore.GetComponent<TMP_Text>().text = $"{PlayerPrefs.GetInt("HighScore")}";
        else
            PlayerPrefs.SetInt("HighScore", 0);

    }

    public override void Clear()
    {
    }
    void Update()
    {
    }
    IEnumerator CoGenerateBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Managers.Game.Spawn(Define.WorldObject.Bullet, "Bullet");
            if (_player == null)
                break;
        }
    }
    IEnumerator CoWarn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            Managers.Game.Spawn(Define.WorldObject.Warn, "Env/Warn");
            if (_player == null)
                break;
        }
    }
}
