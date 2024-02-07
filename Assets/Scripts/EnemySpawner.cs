using System.Collections;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private EnemyController enemyPrefab;

    private void Start()
    {
        StartCoroutine(StartSpawningAsync());
    }

    private IEnumerator StartSpawningAsync()
    {
        while (true)
        {
            // No need to call GetComponent<EnemyController> because we instantiate an EnemyController immediately
            var enemyController = Instantiate(enemyPrefab, transform);
            enemyController.MoveTowards(target.position);

            yield return new WaitForSeconds(1f);
        }
    }
}