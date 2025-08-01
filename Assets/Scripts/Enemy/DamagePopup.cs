using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;  // đúng cho text trong Canvas
    public float floatSpeed = 1f;
    public float duration = 1f;

    private Vector3 moveDir;
    private float timer;

    void Start()
    {
        moveDir = new Vector3(Random.Range(-0.3f, 0.3f), 1f, 0f); // bay lên lệch nhẹ
        timer = 0f;
    }

    void Update()
    {
        transform.position += moveDir * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
    }
}
