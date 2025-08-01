    using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject skillPrefab;
    public Transform firePoint;
    public float minSpeed = 4f;
    public float maxSpeed = 15f;
    public float skillRange = 6f;
    public int manaCost = 20;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CastSkill();
        }
    }

    void CastSkill()
    {
        if (playerStats == null || playerStats.currentMP < manaCost)
        {
            Debug.Log("Không đủ MP để dùng skill!");
            return;
        }

        playerStats.UseMP(manaCost);

        // Tính hướng và khoảng cách tới chuột
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = (mousePos - firePoint.position).normalized;
        float distance = Vector3.Distance(firePoint.position, mousePos);

        // Tốc độ phụ thuộc vào khoảng cách (có thể tinh chỉnh)
        float skillSpeed = Mathf.Clamp(distance * 2f, minSpeed, maxSpeed); // nhân hệ số 2 để nhanh hơn

        // Tính góc xoay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // Tạo và gán dữ liệu cho skill
        GameObject skill = Instantiate(skillPrefab, firePoint.position, rotation);

        Rigidbody2D rb = skill.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * skillSpeed;
        }

        SkillQProjectile sp = skill.GetComponent<SkillQProjectile>();
        if (sp != null)
        {
            sp.SetData(firePoint.position, mousePos, skillRange, skillSpeed);
        }
    }
}
