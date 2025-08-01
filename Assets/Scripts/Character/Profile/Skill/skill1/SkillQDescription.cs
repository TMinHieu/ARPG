using UnityEngine;
using TMPro;

public class SkillQDescription : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    [Header("Skill Info")]
    public int baseDamage = 50;
    public int manaCost = 20;
    public float strMultiplier = 0.5f;
    public float range = 6f;

    void Start()
    {
        PlayerStats stats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();

        int str = stats != null ? stats.str : 0;
        int totalDamage = baseDamage + Mathf.RoundToInt(str * strMultiplier);

        descriptionText.text =
            $"<b>Q - Lưỡi Liềm Bóng Tối</b>\n" +
            $"- Gây <color=#ff5555>{totalDamage}</color> sát thương (50 + STR x 0.5)\n" +
            $"- Tiêu tốn: <color=#55aaff>{manaCost} MP</color>\n" +
            $"- Tầm bay: {range}m\n" +          
            $"- Chém ra 1 lưỡi liềm về hướng chỉ định.";
    }
}
