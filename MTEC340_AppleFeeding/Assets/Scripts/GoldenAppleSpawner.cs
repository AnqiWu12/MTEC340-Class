using System.Collections;
using UnityEngine;

// 每隔一段时间在场地随机位置放一个黄金苹果。
// 场上已经有一个还没被捡时就先不放，避免堆一堆。
public class GoldenAppleSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject _goldenApplePrefab;
    [SerializeField] private float _spawnInterval = 10.0f;   // 每隔几秒尝试放一个
    [SerializeField] private float _spawnRange = 8.0f;       // 以本物体为中心的范围
    [SerializeField] private float _spawnHeight = 1.0f;      // 生成高度

    private GameObject _current;   // 场上那一个，被捡走后自动变 null

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            // 上一个还没被捡，这轮跳过
            if (_current != null) continue;

            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(-_spawnRange, _spawnRange),
                _spawnHeight,
                transform.position.z + Random.Range(-_spawnRange, _spawnRange)
            );

            _current = Instantiate(_goldenApplePrefab, pos, Quaternion.identity);
        }
    }
}