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
    GameObject _boss;
    private object pool;
    public bool _bossStage = true;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        _player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        _coin = GameObject.Find("CoinUI");
        _highScore = GameObject.Find("HighScoreUI");
        _boss = null;
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

        if (_player == null)
        {
            if (Time.timeScale == 0.0f)
                return;
            Time.timeScale = 0.0f;
            StartCoroutine(CoOver());
        }

        //Boss
        if (_bossStage)
        {
            StartCoroutine(CoBoss());
            _bossStage = false;
        }
    }
    IEnumerator CoOver()
    {
        GameObject gameOver = Managers.Resource.Instantiate("UI/GAMEOVER");
        GameObject tmpGameOver = gameOver.transform.GetChild(0).GetChild(0).gameObject;
        TMP_Text tmpText = tmpGameOver.GetComponent<TMP_Text>();
        StartCoroutine(CoChangeColor(tmpText));
        Vector3 p1 = tmpGameOver.transform.position;
        float time = 0.0f;

        while (true)
        {
            if (time <= 0.2f)
                tmpGameOver.transform.position = new Vector3(p1.x, p1.y + (80 - 400 * time), p1.z);
            else if (time < 0.3f)
                tmpGameOver.transform.position = new Vector3(p1.x, p1.y + time * 60, p1.z);
            else if (time < 0.4f)
                tmpGameOver.transform.position = new Vector3(p1.x, p1.y + 18 - time * 42, p1.z);
            else
            {
                tmpGameOver.transform.position = new Vector3(p1.x, p1.y, p1.z);
                break;
            }
            time += Time.unscaledDeltaTime;
            yield return null;
        }


        yield return new WaitForSecondsRealtime(0.5f);

        GameObject exitButton = Managers.Resource.Instantiate("UI/ExitButton");
        GameObject tmpExitButton = exitButton.transform.GetChild(0).GetChild(0).gameObject;
        Vector3 p2 = tmpExitButton.transform.position;
        time = 0.0f;

        while (true)
        {
            if (time < 0.2f)
                tmpExitButton.transform.position = new Vector3(p2.x, p2.y - (60 - 300 * time), p2.z);
            else
            {
                tmpExitButton.transform.position = new Vector3(p2.x, p2.y, p2.z);
                break;
            }
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        yield return null;
    }

    IEnumerator CoChangeColor(TMP_Text tmpText)
    {
        while (true)
        {
            if (tmpText == null)
                break;
            float randR = Random.Range(0f, 1f);
            float randG = Random.Range(0f, 1f);
            float randB = Random.Range(0f, 1f);
            tmpText.color = new Color(randR, randG, randB);

            tmpText.colorGradient = new VertexGradient(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            yield return new WaitForSecondsRealtime(0.1f);

        }
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
            yield return new WaitForSeconds(4.5f);
            Managers.Game.Spawn(Define.WorldObject.Warn, "Env/Warn");
            if (_player == null)
                break;
        }
    }
    IEnumerator CoBoss()
    {
        yield return new WaitForSeconds(22.0f);
        SpawningPool pool = GameObject.Find("SpawningPool").GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(0);
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Boss, "Monsters/boss_silhouette");
        yield return new WaitForSeconds(5.0f);
        Managers.Game.Despawn(obj);
        Managers.Sound.Play("dragon_breathe");
        _boss = Managers.Game.Spawn(Define.WorldObject.Boss, "Monsters/Boss");
        StartCoroutine(CoBossAttack());
    }
    IEnumerator CoBossAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            Managers.Game.Spawn(Define.WorldObject.Warn, "Env/Warn");
            if (_boss == null)
                break;
        }
    }
}
