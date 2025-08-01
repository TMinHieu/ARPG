using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Text Hiển thị")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI strText;
    public TextMeshProUGUI vitText;
    public TextMeshProUGUI intText;

    [Header("Thanh máu, mana, exp")]
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    void Update()
    {
        if (playerStats == null) return;

        // Cập nhật số liệu
        levelText.text = "Level " + playerStats.level;
        hpText.text = $"{playerStats.currentHP} / {playerStats.maxHP}";
        mpText.text = $"{playerStats.currentMP} / {playerStats.maxMP}";
        expText.text = $"EXP: {playerStats.currentExp} / {playerStats.expToNextLevel}";

        strText.text = "STR: " + playerStats.str;
        vitText.text = "VIT: " + playerStats.vit;
        intText.text = "INT: " + playerStats.intel;

        // Cập nhật thanh trượt
        if (hpSlider != null)
        {
            hpSlider.maxValue = playerStats.maxHP;
            hpSlider.value = playerStats.currentHP;
        }

        if (mpSlider != null)
        {
            mpSlider.maxValue = playerStats.maxMP;
            mpSlider.value = playerStats.currentMP;
        }

        if (expSlider != null)
        {
            expSlider.maxValue = playerStats.expToNextLevel;
            expSlider.value = playerStats.currentExp;
        }
    }
}
