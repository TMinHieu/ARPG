using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Cài đặt máu")]
    public int maxHP = 100;
    private int currentHP;

    [Header("HUD gắn trên quái")]
    public Slider healthSlider;
    public TextMeshProUGUI enemyNameText;
    public string enemyName = "Slime Lv1";

    [Header("Hiệu ứng (tùy chọn)")]
    public GameObject hitEffectPrefab;
    [SerializeField] private GameObject damagePopupPrefab;

    [Header("Thành phần cần ẩn khi chết")]
    public SpriteRenderer bodyRenderer;
    public Collider2D hitbox;

    [Header("Di chuyển & Đuổi Player")]
    public float chaseRange = 5f;
    public float moveSpeed = 2f;
    public float returnRange = 10f;

    private Vector3 originalPosition;
    private Transform player;
    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        Init();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            MoveTowards(player.position);
        }
        else if (distance > returnRange)
        {
            MoveTowards(originalPosition);
        }
        else
        {
            // Đứng yên
            rb.linearVelocity = Vector2.zero;
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void Init()
    {
        currentHP = maxHP;
        isDead = false;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = maxHP;
            healthSlider.gameObject.SetActive(true);
        }

        if (enemyNameText != null)
        {
            enemyNameText.text = enemyName;
            enemyNameText.gameObject.SetActive(true);
        }

        if (bodyRenderer != null) bodyRenderer.enabled = true;
        if (hitbox != null) hitbox.enabled = true;

        gameObject.SetActive(true);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        if (EnemyHUDManager.Instance != null)
        {
            EnemyHUDManager.Instance.ShowEnemy(this);
        }

        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (damagePopupPrefab != null)
        {
            ShowDamagePopup(amount);
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public int CurrentHP => currentHP;

    void Die()
    {
        isDead = true;

        EnemyCombat combat = GetComponent<EnemyCombat>();
        if (combat != null)
        {
            combat.GiveExpToPlayer();
        }

        if (EnemyHUDManager.Instance != null)
        {
            EnemyHUDManager.Instance.HideHUD();
        }

        FindObjectOfType<QuestManager>()?.OnEnemyKilled(enemyName);

        if (healthSlider != null) healthSlider.gameObject.SetActive(false);
        if (enemyNameText != null) enemyNameText.gameObject.SetActive(false);
        if (bodyRenderer != null) bodyRenderer.enabled = false;
        if (hitbox != null) hitbox.enabled = false;

        rb.linearVelocity = Vector2.zero;

        StartCoroutine(RespawnAfterDelay(5f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = originalPosition;
        Init();
    }

    void ShowDamagePopup(int damage)
    {
        if (damagePopupPrefab != null)
        {
            GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            popup.GetComponent<DamagePopup>()?.Setup(damage);
        }
    }
}
