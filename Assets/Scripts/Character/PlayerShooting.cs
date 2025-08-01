using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    // Gán prefab MagicBullet
    public Transform firePoint;        // Gán FirePoint (vị trí tay)
    public float bulletSpeed = 8f;
    public float bulletRange = 5f;     // Phạm vi bay của đạn (đơn vị Unity unit)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || Camera.main == null)
        {
            Debug.LogError("❌ Chưa gán đủ Prefab hoặc FirePoint hoặc Camera!");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("❌ Prefab MagicBullet thiếu Rigidbody2D!");
            return;
        }

        rb.linearVelocity = direction * bulletSpeed;

        // Xoay đạn theo hướng bắn
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Gọi script Projectile và truyền thông tin
        NomalAttach projectile = bullet.GetComponent<NomalAttach>();
        if (projectile != null)
        {
            projectile.SetData(firePoint.position, bulletRange);
        }
    }
}
