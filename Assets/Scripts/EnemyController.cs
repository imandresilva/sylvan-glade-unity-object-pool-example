using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController: MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float destinationReachedRadius = 0.5f;
    [SerializeField] private EnemyEventRelay eventRelay;

    private Action<EnemyController> _deathCallback;

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

    public void MoveTowards(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    private void OnDeath()
    {
        agent.ResetPath();
        Destroy(gameObject);
    }

    private void DestinationReached()
    {
        Destroy(gameObject);
    }
}