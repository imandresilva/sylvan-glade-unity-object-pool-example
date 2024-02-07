using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventRelay: MonoBehaviour
{
    public event Action EnemyDied;
    [SerializeField] private UnityEvent enemyDied;

    public void OnEnemyDied()
    {
        EnemyDied?.Invoke();
        enemyDied?.Invoke();
    }
}