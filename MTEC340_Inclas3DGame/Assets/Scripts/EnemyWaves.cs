using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private List<GameObject> _enemy;


    private void Update()
    {
        if (_enemy.Count == 0)
        {
            SpawnWave(_waveNumber +1);
        
        }
    } 

    private void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, _enemyParent.transform);
            _enemy.Add(enemy);
        }
    }
    _waveNumber++;

}
