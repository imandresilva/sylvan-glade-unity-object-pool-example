using UnityEngine;

public class PlayerDamageDealer: MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask enemyLayerMask;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.SphereCast(ray, 0.5f, out var hit, Mathf.Infinity, enemyLayerMask))
        {
            return;
        }

        if(!hit.collider.TryGetComponent<Damageable>(out var damageable)) {
            return;
        }

        damageable.TakeDamage(damage);
    }
}