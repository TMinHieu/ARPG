using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHUDManager : MonoBehaviour
{
    public static EnemyHUDManager Instance;

    [Header("Thành phần UI")]
    public TextMeshProUGUI nameText;
    public Slider healthSlider;
    public TextMeshProUGUI hpText; // <-- Thêm dòng này để hiển thị số máu

    private Enemy currentTarget;
    private float lastHitTime;
    private float hideDelay = 2f;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false); // Ẩn HUD lúc khởi tạo
    }

    void Update()
    {
        if (currentTarget != null)
        {
            if (currentTarget == null || currentTarget.CurrentHP <= 0)
            {
                HideHUD(); // Nếu quái chết thì ẩn luôn
                return;
            }

            // Cập nhật giá trị máu
            healthSlider.maxValue = currentTarget.maxHP;
            healthSlider.value = Mathf.Clamp(currentTarget.CurrentHP, 0, currentTarget.maxHP);

            // Cập nhật text máu nếu có
            if (hpText != null)
            {
                hpText.text = $"{currentTarget.CurrentHP} / {currentTarget.maxHP}";
            }

            // Ẩn nếu không bị bắn thêm sau 2 giây
            if (Time.time - lastHitTime > hideDelay)
            {
                HideHUD();
            }
        }
    }

    public void ShowEnemy(Enemy enemy)
    {
        currentTarget = enemy;

        nameText.text = enemy.enemyName;
        healthSlider.maxValue = enemy.maxHP;
        healthSlider.value = enemy.CurrentHP;

        if (hpText != null)
        {
            hpText.text = $"{enemy.CurrentHP} / {enemy.maxHP}";
        }

        lastHitTime = Time.time;
        gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        gameObject.SetActive(false);
        currentTarget = null;
    }
}
