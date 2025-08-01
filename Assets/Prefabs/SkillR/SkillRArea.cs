using UnityEngine;
using System.Collections.Generic;

public class SkillRArea : MonoBehaviour
{
    [Header("Cài đặt skill")]
    public float expandSpeed = 0.1f;
    public float maxRadius = 0.2f;
    public int damage = 60;
    public float knockbackForce = 10f;
    public GameObject explosionEffect;
    public float rotateSpeed = 180f;

    private CircleCollider2D circleCollider;
    private float currentRadius = 0f;
    private bool hasFinishedExpanding = false;

    private HashSet<Enemy> damagedEnemies = new HashSet<Enemy>();
    private DisplayDame damageDisplay;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        transform.localScale = Vector3.zero;

        damageDisplay = FindObjectOfType<DisplayDame>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (!hasFinishedExpanding)
        {
            currentRadius += expandSpeed * Time.deltaTime;
            float scale = Mathf.Min(currentRadius * 2f, maxRadius * 2f);
            transform.localScale = new Vector3(scale, scale, 1f);

            if (currentRadius >= maxRadius)
            {
                hasFinishedExpanding = true;
                Destroy(gameObject, 0.3f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null || damagedEnemies.Contains(enemy)) return;

        damagedEnemies.Add(enemy);
        enemy.TakeDamage(damage);

        // ✅ Hiển thị ngay sát thương, đồng thời cộng dồn
        damageDisplay?.ShowDameAoe(damage);

        // Knockback
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockDir = (enemy.transform.position - transform.position).normalized;
            rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
        }

        enemy.GetComponent<SlimeMovement>()?.ApplyKnockbackDelay();
    }

    void OnDestroy()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // ❌ Không hiển thị tổng sát thương nữa vì đã cộng dồn từng lần
    }
}
