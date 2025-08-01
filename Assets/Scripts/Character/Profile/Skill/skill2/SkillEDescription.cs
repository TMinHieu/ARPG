using UnityEngine;
using TMPro;

public class SkillEDescription : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    [Header("Skill Info")]
    public int baseDamage = 20;
    public int manaCost = 50;
    public float strMultiplier = 0.5f;
    public float range = 8f;

    void Start()
    {
        PlayerStats stats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();

        int str = stats != null ? stats.str : 0;
        int totalDamage = baseDamage + Mathf.RoundToInt(str * strMultiplier);

        descriptionText.text =
            $"<b>E - Kiếm Vũ</b>\n" +
            $"- Gây <color=#ff5555>{totalDamage}</color> sát thương (20 + STR x 0.5)\n" +
            $"- Tiêu tốn: <color=#55aaff>{manaCost} MP</color>\n" +
            $"- Tầm bay: {range}m\n" +        
            $"- Chém ra 8 đường kiếm bao quanh bản thân.";
    }
}
