using UnityEngine;
using TMPro;
using DG.Tweening; // ✅ Thêm thư viện DOTween

public class DisplayDame : MonoBehaviour
{
    [Header("UI hiển thị sát thương")]
    public TextMeshProUGUI damageText;

    private int currentTotalDamage = 0;
    private float hideDelay = 1f;
    private Vector3 originalScale;

    private void Awake()
    {
        ClearText();
        if (damageText != null)
        {
            originalScale = damageText.transform.localScale;
        }
    }

    /// <summary>
    /// Hiển thị sát thương không cộng dồn (dùng cho đánh thường, Skill Q)
    /// </summary>
    public void ShowDameOne(int amount)
    {
        if (damageText == null) return;

        currentTotalDamage = amount;
        damageText.text = currentTotalDamage.ToString();
        damageText.gameObject.SetActive(true);

        AnimateDamageText();

        CancelInvoke(nameof(ClearText));
        Invoke(nameof(ClearText), hideDelay);
    }

    /// <summary>
    /// Hiển thị sát thương cộng dồn (dùng cho skill AOE như R)
    /// </summary>
    public void ShowDameAoe(int amount)
    {
        if (damageText == null) return;

        currentTotalDamage += amount;
        damageText.text = currentTotalDamage.ToString();
        damageText.gameObject.SetActive(true);

        AnimateDamageText();

        CancelInvoke(nameof(ClearText));
        Invoke(nameof(ClearText), hideDelay);
    }

    void AnimateDamageText()
    {
        // Hủy tween cũ để tránh xung đột
        damageText.transform.DOKill();
        damageText.DOKill();

        // Reset lại alpha và scale
        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1f);
        damageText.transform.localScale = Vector3.zero;

        // Tween mới
        damageText.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
        damageText.DOFade(0f, hideDelay).SetDelay(0.2f);
    }


    void ClearText()
    {
        damageText.text = "";
        damageText.gameObject.SetActive(false);
        currentTotalDamage = 0;
    }
}
