using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public int lightAttackDamage = 10;  // Change damage to int
    public int heavyAttackDamage = 30; // Change damage to int

    public float lightAttackCooldown = 0.5f; // Faster cooldown for light attack
    public float heavyAttackCooldown = 1.5f; // Longer cooldown for heavy attack

    private float nextLightAttackTime = 0f;
    private float nextHeavyAttackTime = 0f;

    public LayerMask enemyLayer;

    void Update()
    {
        // Light Attack (Left Click)
        if (Time.time >= nextLightAttackTime && Input.GetMouseButtonDown(0))
        {
            PerformLightAttack();
            nextLightAttackTime = Time.time + lightAttackCooldown; // Set next light attack time
        }

        // Heavy Attack (Right Click)
        if (Time.time >= nextHeavyAttackTime && Input.GetMouseButtonDown(1))
        {
            PerformHeavyAttack();
            nextHeavyAttackTime = Time.time + heavyAttackCooldown; // Set next heavy attack time
        }

        // Debug Ray for visualization
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.forward;
        Debug.DrawRay(origin, direction * attackRange, Color.red);
    }

    void PerformLightAttack()
    {
        Attack(lightAttackDamage);
    }

    void PerformHeavyAttack()
    {
        Attack(heavyAttackDamage);
    }

    void Attack(int damage) // Change this parameter to 'int' as well
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, attackRange, enemyLayer))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // This should now work without the error
            }
        }
    }
}
