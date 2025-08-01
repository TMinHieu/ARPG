using UnityEngine;
using TMPro;

public class SkillRDescription : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    [Header("Skill Info")]
    public int baseDamage = 60;
    public int manaCost = 100;
    public float knockbackForce = 10f;
    public float maxRadius = 0.2f;
    public float rotateSpeed = 180f;

    void Start()
    {
        PlayerStats stats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();

        int str = stats != null ? stats.str : 0;
        int totalDamage = baseDamage + Mathf.RoundToInt(str * 0.5f);

        descriptionText.text =
            $"<b>R - Vòng Xoáy Bóng Tối</b>\n" +
            $"- Gây <color=#ff5555>{totalDamage}</color> sát thương (60 + STR x 0.5)\n" +
            $"- Tiêu tốn: <color=#55aaff>{manaCost} MP</color>\n" +
            $"- Tốc độ xoay: {rotateSpeed}°/giây\n" +                     
            $"- Mở rộng nhanh và đẩy lùi kẻ địch";
    }
}
