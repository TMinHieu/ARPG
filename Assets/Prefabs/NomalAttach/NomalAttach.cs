using UnityEngine;

public class NomalAttach : MonoBehaviour
{
    public GameObject explosionEffect;
    private Vector3 spawnPosition;
    private float maxDistance = 5f;
    private bool hasHit = false;

    public void SetData(Vector3 startPos, float range)
    {
        spawnPosition = startPos;
        maxDistance = range;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, spawnPosition);
        if (distance >= maxDistance)
        {
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;
        if (!other.CompareTag("Enemy")) return;

        hasHit = true;

        PlayerStats playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        int damage = 20;

        if (playerStats != null)
        {
            damage = Mathf.RoundToInt(playerStats.totalDamage);
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // ✅ Hiển thị sát thương từng đòn riêng biệt
        var damageDisplay = FindObjectOfType<DisplayDame>();
        damageDisplay?.ShowDameOne(damage);

        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
