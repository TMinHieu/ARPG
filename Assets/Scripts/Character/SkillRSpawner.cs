using UnityEngine;

public class SkillRSpawner : MonoBehaviour
{
    public GameObject skillRAreaPrefab;
    public int mpCost = 100;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CastSkillR();
        }
    }

    void CastSkillR()
    {
        if (playerStats == null || playerStats.currentMP < mpCost) return;

        playerStats.UseMP(mpCost);
        Instantiate(skillRAreaPrefab, transform.position, Quaternion.identity);
    }
}
