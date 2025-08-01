using UnityEngine;

public class SkillQProjectile : MonoBehaviour
{
    private Vector3 spawnPos;
    private Vector3 direction;
    private float speed;
    private float maxDistance = 6f;
    public int baseDamage = 50;

    public void SetData(Vector3 start, Vector3 target, float range, float speed)
    {
        spawnPos = start;
        direction = (target - start).normalized;
        this.speed = speed;
        maxDistance = range;

        // Quay đầu đạn theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, spawnPos) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        // Tính tổng sát thương dựa vào STR
        PlayerStats playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        int str = playerStats != null ? playerStats.str : 0;
        int totalDamage = baseDamage + Mathf.RoundToInt(str * 0.5f);

        // Gây sát thương
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(totalDamage);
        }

        // ✅ Gọi hiển thị không cộng dồn (chỉ hiển thị riêng lẻ mỗi viên)
        var display = FindObjectOfType<DisplayDame>();
        display?.ShowDameOne(totalDamage);

        Destroy(gameObject);
    }
}
