using System;
using UnityEngine;
using UnityEngine.AI;

public class PooledEnemyController: MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float destinationReachedRadius = 0.5f;
    [SerializeField] private EnemyEventRelay eventRelay;

    private Action<PooledEnemyController> _deathCallback;

    private void OnEnable()
    {
        eventRelay.EnemyDied += OnDeath;
    }

    private void OnDisable()
    {
        eventRelay.EnemyDied -= OnDeath;
    }

    private void Update()
    {
        if (agent.hasPath && agent.remainingDistance < destinationReachedRadius)
        {
            DestinationReached();
        }
    }

    public void MoveTowards(Vector3 targetPosition, Action<PooledEnemyController> taskCompletedCallback)
    {
        agent.SetDestination(targetPosition);
        _deathCallback = taskCompletedCallback;
    }

    private void OnDeath()
    {
        agent.ResetPath();
        _deathCallback?.Invoke(this);
    }

    private void DestinationReached()
    {
        _deathCallback?.Invoke(this);
    }
}