using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_ins;
    public static GameManager Ins
    {
        get
        {
            if (!m_ins)
            {
                m_ins = FindObjectOfType<GameManager>();
                if (!m_ins) m_ins = new GameObject().AddComponent<GameManager>();
            }
            return m_ins;
        }
    }
    
    
    public bool CanMoveUpAndDown;
    [SerializeField] private EnemyScript _enemyScript;
    [SerializeField] private int _amountEnemy;
    [SerializeField] private float _timeDelaySpawnEnemy = 0.1f;
    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        StartCoroutine(DelaySpawnEnemy());
    }

    IEnumerator DelaySpawnEnemy()
    {
        yield return new WaitForSeconds(_timeDelaySpawnEnemy);
        EnemyScript newEnemy = Instantiate(_enemyScript);
        newEnemy.Active(_amountEnemy);
        _amountEnemy--;
        if (_amountEnemy > 0) StartCoroutine(DelaySpawnEnemy());
    }
}