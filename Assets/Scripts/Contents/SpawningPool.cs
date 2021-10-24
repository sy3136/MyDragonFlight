using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0;
    [SerializeField]
    float _spawnTime = 3.0f;

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while(_monsterCount + _reserveCount < _keepMonsterCount)
            StartCoroutine(ReserveSpawn());
    }
    IEnumerator ReserveSpawn()
    {
        _reserveCount += 5;
        yield return new WaitForSeconds(_spawnTime);
        for(int i = 0; i<5; i++)
        {
            int rand = Random.Range(1, 3);
            GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, $"Monsters/Monster{rand}");

            //Vector3 pos = new Vector3(0.14f + i * 0.14f, 1f);
            //obj.transform.position = Camera.main.ViewportToWorldPoint(pos);
            obj.transform.position = new Vector3(-2.5f + i*1.27f, 6.0f);
        }
        _reserveCount -= 5;
    }
}

