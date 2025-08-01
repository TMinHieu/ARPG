using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;            // Tốc độ tối đa
    public float accelerationTime = 0.1f;     // Thời gian tăng tốc (mượt)
    public float decelerationTime = 0.2f;     // Thời gian giảm tốc (quán tính)

    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private Vector2 currentVelocity;          // Biến trung gian để SmoothDamp
    private Vector2 smoothedVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Nhận input
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection = inputDirection.normalized;
    }

    void FixedUpdate()
    {
        // Tính vận tốc mượt bằng SmoothDamp
        float smoothTime = inputDirection.magnitude > 0.1f ? accelerationTime : decelerationTime;

        smoothedVelocity = Vector2.SmoothDamp(
            rb.linearVelocity,
            inputDirection * moveSpeed,
            ref currentVelocity,
            smoothTime
        );

        rb.linearVelocity = smoothedVelocity;
    }
}
