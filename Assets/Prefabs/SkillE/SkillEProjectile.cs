using UnityEngine;

public class SkillEProjectile : MonoBehaviour
{
    public int baseDamage = 20;
    public GameObject explosionEffect;

    private Vector3 direction;
    private float speed;
    private float maxDistance;
    private float distanceTraveled = 0f;

    private DisplayDame damageDisplay;
    private int totalDamageDealt = 0;

    public void Launch(Vector3 dir, float speed, float range)
    {
        this.direction = dir.normalized;
        this.speed = speed;
        this.maxDistance = range;

        // Xoay đầu đạn theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        damageDisplay = FindObjectOfType<DisplayDame>();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += direction * step;
        distanceTraveled += step;

        if (distanceTraveled >= maxDistance)
        {
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        // Tính sát thương theo STR
        PlayerStats playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        int str = playerStats != null ? playerStats.str : 0;
        int totalDamage = baseDamage + Mathf.RoundToInt(str * 0.5f);

        // Gây sát thương
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(totalDamage);
        }

        // ✅ Tích lũy tổng sát thương để hiển thị sau (Aoe)
        totalDamageDealt += totalDamage;

        // Gọi nổ
        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // ✅ Hiển thị tổng sát thương theo dạng Aoe (cộng dồn)
        if (damageDisplay != null && totalDamageDealt > 0)
        {
            damageDisplay.ShowDameAoe(totalDamageDealt);
        }

        Destroy(gameObject);
    }
}
