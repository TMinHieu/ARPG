using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Tấn công")]
    public int attackPower = 10;
    public float attackCooldown = 2f;
    public float attackRange = 5f;

    [Header("Bắn đạn")]
    public GameObject projectilePrefab;     // Prefab viên đạn
    public Transform firePoint;             // Vị trí bắn
    public float projectileSpeed = 5f;      // Tốc độ bay

    [Header("EXP")]
    public int expReward = 20;

    private float lastAttackTime = -Mathf.Infinity;
    private Transform player;
    private Enemy enemyStats;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyStats = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemyStats.CurrentHP <= 0) return;

        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                ShootProjectile();
                lastAttackTime = Time.time;
            }
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null || firePoint == null || player == null) return;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        EnemyProjectile proj = bullet.GetComponent<EnemyProjectile>();
        if (proj != null)
        {
            proj.SetDamage(attackPower);
        }
    }

    public void GiveExpToPlayer()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.GainExp(expReward);
        }
    }
}
