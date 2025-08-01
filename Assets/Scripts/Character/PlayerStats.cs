using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Level System")]
    public int level = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;

    [Header("Base Stats")]
    public int str = 0;
    public int vit = 0;
    public int intel = 0;

    [Header("Calculated Stats")]
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    [Header("Combat")]
    public float baseDamage = 10f;
    public float totalDamage;

    [Header("UI HP/MP")]
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    public Slider mpSlider;
    public TextMeshProUGUI mpText;

    [Header("UI EXP")]
    public Slider expSlider;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;  // ✅ Thêm dòng này để hiển thị level

    [Header("Respawn")]
    public Transform respawnPoint;

    void Start()
    {
        RecalculateStats();
        UpdateHPUI();
        UpdateMPUI();
        UpdateExpUI();
        UpdateLevelUI(); // ✅ Gọi hàm cập nhật Level

        InvokeRepeating(nameof(RegenerateMP), 1f, 1f);
    }

    public void GainExp(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
        }

        UpdateExpUI();
    }

    void LevelUp()
    {
        level++;
        str++;
        vit++;
        intel++;
        expToNextLevel += 50;

        RecalculateStats();
        UpdateLevelUI(); // ✅ Cập nhật giao diện level
    }

    public void RecalculateStats()
    {
        maxHP = 200 + vit * 100;
        maxMP = 100 + intel * 50;
        totalDamage = baseDamage + str * 0.5f;

        currentHP = maxHP;
        currentMP = maxMP;

        UpdateHPUI();
        UpdateMPUI();
        UpdateExpUI();
        UpdateLevelUI(); // ✅ Đảm bảo level luôn được cập nhật
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateHPUI();

        if (currentHP <= 0)
        {
            DieAndRespawn();
        }
    }

    public bool UseMP(int amount)
    {
        if (currentMP < amount)
            return false;

        currentMP -= amount;
        if (currentMP < 0) currentMP = 0;

        UpdateMPUI();
        return true;
    }

    void RegenerateMP()
    {
        if (currentMP < maxMP)
        {
            currentMP += 10;
            if (currentMP > maxMP) currentMP = maxMP;
            UpdateMPUI();
        }
    }

    void UpdateHPUI()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        if (hpText != null)
        {
            hpText.text = $"{currentHP} / {maxHP}";
        }
    }

    void UpdateMPUI()
    {
        if (mpSlider != null)
        {
            mpSlider.maxValue = maxMP;
            mpSlider.value = currentMP;
        }

        if (mpText != null)
        {
            mpText.text = $"{currentMP} / {maxMP}";
        }
    }

    void UpdateExpUI()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToNextLevel;
            expSlider.value = currentExp;
        }

        if (expText != null)
        {
            expText.text = $"EXP: {currentExp} / {expToNextLevel}";
        }
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = "Level " + level;
        }
    }

    void DieAndRespawn()
    {
        Debug.Log("Player died. Respawning...");

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        currentHP = maxHP;
        currentMP = maxMP;

        UpdateHPUI();
        UpdateMPUI();
    }
}
