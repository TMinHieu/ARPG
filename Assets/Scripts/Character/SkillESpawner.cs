using UnityEngine;

public class SkillESpawner : MonoBehaviour
{
    public GameObject projectilePrefab;   // Prefab đạn
    public float projectileSpeed = 6f;
    public float projectileRange = 4f;
    public int mpCost = 100;
    public float spawnOffset = 5f; // khoảng cách tránh player

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryCastSkillE();
        }
    }

    void TryCastSkillE()
    {
        if (playerStats == null || playerStats.currentMP < mpCost) return;

        playerStats.UseMP(mpCost);

        Vector3[] directions = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            (Vector3.up + Vector3.left).normalized,
            (Vector3.up + Vector3.right).normalized,
            (Vector3.down + Vector3.left).normalized,
            (Vector3.down + Vector3.right).normalized,
        };

        foreach (Vector3 dir in directions)
        {
            // Tính vị trí bắn lệch ra khỏi người chơi một chút
            Vector3 spawnPos = transform.position + dir.normalized * spawnOffset;

            GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            SkillEProjectile proj = bullet.GetComponent<SkillEProjectile>();
            if (proj != null)
            {
                proj.Launch(dir, projectileSpeed, projectileRange);
            }
        }
    }

}
