using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一波波地生成动物：当前这波清空了就刷下一波，数量逐波递增
// 从一组动物 prefab 里随机挑一个生成，这样每次刷出来的动物不一样。
public class EnemyWaves : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject[] _enemyPrefabs;   // 所有能刷的动物，随机选
    [SerializeField] private float _spawnRange = 8.0f;     // 以本物体为中心的生成范围
    [SerializeField] private float _spawnHeight = 1.5f;    // 生成高度
    [SerializeField] private float _waveCooldown = 1.0f;   // 每波之间隔多久

    [Header("Difficulty")]
    [SerializeField] private int _firstWaveCount = 1;      // 第一波几只
    [SerializeField] private bool _increasePerWave = true; // 是否每波多一只

    // 当前还活着的动物，动物消失时会把自己从这里移除
    public List<GameObject> Enemies = new List<GameObject>();

    private int _waveNumber = 0;
    private bool _isSpawning = false;

    private void Update()
    {
        // 场上清空且没在生成中，就开下一波
        if (Enemies.Count == 0 && !_isSpawning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        _isSpawning = true;

        yield return new WaitForSeconds(_waveCooldown);

        _waveNumber++;

        // 这波刷几只
        int count = _increasePerWave
            ? _firstWaveCount + (_waveNumber - 1)
            : _firstWaveCount;

        for (int i = 0; i < count; i++)
        {
            // 随机挑一种动物
            GameObject prefab = PickRandomPrefab();
            if (prefab == null) continue;   // 数组没配就跳过，别报错

            // 范围内随机落点
            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(-_spawnRange, _spawnRange),
                _spawnHeight,
                transform.position.z + Random.Range(-_spawnRange, _spawnRange)
            );

            Quaternion rot = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

            // 生成成本物体的子物体，动物才能在消失时找到我、通知刷新
            GameObject enemy = Instantiate(prefab, pos, rot, transform);
            Enemies.Add(enemy);
        }

        _isSpawning = false;
    }

    // 从动物数组里随机取一个，空数组返回 null
    private GameObject PickRandomPrefab()
    {
        if (_enemyPrefabs == null || _enemyPrefabs.Length == 0)
        {
            return null;
        }

        int index = Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[index];
    }
}