using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // Tham chiếu đến nhân vật
    public float smoothSpeed = 5f; // Độ mượt khi camera theo

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
