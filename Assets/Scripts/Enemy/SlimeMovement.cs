using UnityEngine;
using System.Collections;

public class SlimeMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveInterval = 2f;
    public float moveDistance = 1f;
    public Vector2 movementBounds = new Vector2(3f, 3f);
    public float knockbackDuration = 0.2f;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private bool isKnockbacked = false;
    private float knockbackTimer = 0f;

    private Coroutine bounceMoveCoroutine;
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originPosition = transform.position;
        initialScale = transform.localScale; // 🟢 Lưu scale bầu dục gốc

        InvokeRepeating(nameof(ChooseNewTarget), 0f, moveInterval);
    }

    void ChooseNewTarget()
    {
        if (isKnockbacked) return;

        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 proposedPosition = transform.position + (Vector3)(direction * moveDistance);

        proposedPosition.x = Mathf.Clamp(proposedPosition.x, originPosition.x - movementBounds.x, originPosition.x + movementBounds.x);
        proposedPosition.y = Mathf.Clamp(proposedPosition.y, originPosition.y - movementBounds.y, originPosition.y + movementBounds.y);

        targetPosition = proposedPosition;

        if (bounceMoveCoroutine != null) StopCoroutine(bounceMoveCoroutine);
        bounceMoveCoroutine = StartCoroutine(BounceAndMove(targetPosition));
    }

    IEnumerator BounceAndMove(Vector3 target)
    {
        float duration = 0.3f;
        float timer = 0f;

        Vector3 startPos = transform.position;

        while (timer < duration)
        {
            float t = timer / duration;
            transform.position = Vector3.Lerp(startPos, target, t);

            // Hiệu ứng squash/stretch
            float stretch = Mathf.Sin(t * Mathf.PI); // 0 → 1 → 0
            Vector3 stretchScale = new Vector3(
                initialScale.x * (1f + 0.1f * (1 - stretch)), // co ngang khi đẩy
                initialScale.y * (1f + 0.2f * stretch),       // giãn dọc khi nhảy
                1f
            );
            transform.localScale = stretchScale;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        transform.localScale = initialScale; // 🟢 Trả lại hình dạng ban đầu
    }

    void FixedUpdate()
    {
        if (isKnockbacked)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockbacked = false;
            }
        }
    }

    public void ApplyKnockbackDelay()
    {
        isKnockbacked = true;
        knockbackTimer = knockbackDuration;
    }
}
