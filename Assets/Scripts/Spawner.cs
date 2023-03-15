using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _pointOfSpawn;
    [SerializeField] private Player _player;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _countOfSpawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if(_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn > _currentWave.Delay)
        {
            InstantiateEnemy();
            _countOfSpawned++;
            _timeAfterLastSpawn = 0;

            EnemyCountChanged?.Invoke(_countOfSpawned, _currentWave.Count);
        }

        if(_currentWave.Count <= _countOfSpawned)
        {
            if(_waves.Count > _currentWaveNumber + 1)
            {
                AllEnemySpawned?.Invoke();
            }

            _currentWave = null;
        }
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _countOfSpawned = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _pointOfSpawn.position, _pointOfSpawn.rotation, _pointOfSpawn).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]

public class Wave
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _count;
    [SerializeField] private float _delay;

    public float Delay => _delay;
    public GameObject Template => _template;
    public int Count => _count;
}
