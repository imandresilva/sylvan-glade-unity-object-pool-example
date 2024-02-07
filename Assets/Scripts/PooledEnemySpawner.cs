using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PooledEnemySpawner: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private PooledEnemyController enemyPrefab;

    private ObjectPool<PooledEnemyController> _pool;

    private void Start()
    {
        _pool = new ObjectPool<PooledEnemyController>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReturnToPool);
        StartCoroutine(StartSpawningAsync());
    }

    private PooledEnemyController CreateEnemy()
    {
        var enemyController = Instantiate(enemyPrefab, transform);
        enemyController.gameObject.SetActive(false);
        return enemyController;
    }

    private void OnGetFromPool(PooledEnemyController enemyController)
    {
        enemyController.transform.position = transform.position;
        enemyController.gameObject.SetActive(true);
    }

    private void OnReturnToPool(PooledEnemyController enemyController)
    {
        enemyController.gameObject.SetActive(false);
    }

    private IEnumerator StartSpawningAsync()
    {
        while (true)
        {
            var enemyController = _pool.Get();
            enemyController.MoveTowards(target.position, _pool.Release);

            yield return new WaitForSeconds(1f);
        }
    }
}