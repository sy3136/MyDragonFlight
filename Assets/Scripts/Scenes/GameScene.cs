using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;
    GameObject _player;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        _player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

        // Bullet
        StartCoroutine(CoGenerateBullet());

        // Warn
        StartCoroutine(CoWarn());
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
