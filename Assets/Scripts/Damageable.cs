using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyEventRelay eventRelay;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 10f;

    private float _health;
    private float Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, maxHealth);
    }

    private void OnEnable()
    {
        Health = maxHealth;
    }

    /// <summary>
    /// Apply an amount of damage to this damageable.
    /// If it makes the enemy health reach 0, invokes the enemy died event.
    /// </summary>
    /// <param name="amount">The amount of damage to take.</param>
    public void TakeDamage(float amount)
    {
        Health -= amount;

        if (Health == 0f)
        {
            eventRelay.OnEnemyDied();
        }
    }
}
