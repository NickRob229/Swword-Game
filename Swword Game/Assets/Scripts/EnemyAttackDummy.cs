using UnityEngine;
using System.Collections;

public class EnemyAttackDummy : MonoBehaviour
{
    public float attackRange = 2f;
    public int lightAttackDamage = 10;
    public float attackDelay = 1.5f; // time between each directional attack

    public LayerMask playerLayer;

    public EnemyDirectionalSelector enemyDirectionSelector; // Correct type here

    private int currentDirection = 0; // 0=Top, 1=Right, 2=Bottom, 3=Left
    private string[] directions = new string[] { "Top", "Right", "Bottom", "Left" };
    private Vector3[] directionVectors = new Vector3[]
    {
        Vector3.forward,  // Top
        Vector3.right,    // Right
        Vector3.back,     // Bottom
        Vector3.left      // Left
    };

    void Start()
    {
        if (enemyDirectionSelector == null)
        {
            Debug.LogError("EnemyDirectionalSelector reference missing from EnemyAttackDummy.");
            return;
        }

        StartCoroutine(CycleAndAttack());
    }

    IEnumerator CycleAndAttack()
    {
        while (true)
        {
            SetDirection(currentDirection);
            yield return new WaitForSeconds(attackDelay);
            PerformAttack(directionVectors[currentDirection]);
            enemyDirectionSelector.ResetArrowColors(); // Reset after attack
            currentDirection = (currentDirection + 1) % directions.Length;
        }
    }

    void SetDirection(int dirIndex)
    {
        Vector2 dirVector = Vector2.zero;

        switch (dirIndex)
        {
            case 0: dirVector = Vector2.up; break;      // Top
            case 1: dirVector = Vector2.right; break;   // Right
            case 2: dirVector = Vector2.down; break;    // Bottom
            case 3: dirVector = Vector2.left; break;    // Left
        }

        enemyDirectionSelector.ForceDirection(dirVector);
        Debug.Log("Enemy selected: " + directions[dirIndex]);
    }

    void PerformAttack(Vector3 attackDir)
    {
        Vector3 origin = transform.position + Vector3.up;

        Debug.DrawRay(origin, attackDir * attackRange, Color.green, 1f);

        if (Physics.Raycast(origin, attackDir, out RaycastHit hit, attackRange, playerLayer))
        {
            PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(lightAttackDamage);
                Debug.Log("Enemy attacked player for " + lightAttackDamage);
            }
        }
    }
}
