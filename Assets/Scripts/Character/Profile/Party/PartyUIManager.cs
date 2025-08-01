using UnityEngine;
using UnityEngine.UI;

public class PartyUIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] buttons;         // 12 nút

    [Header("Panels")]
    public GameObject[] panels;      // 12 panel
    public Image[] panelImages;      // Image bên trong mỗi panel

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Lưu chỉ số
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    void OnButtonClicked(int index)
    {
        // Ẩn tất cả panel
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        // Hiện panel được chọn
        panels[index].SetActive(true);

        // Lấy sprite từ chính image của button
        Image buttonImage = buttons[index].GetComponent<Image>();
        if (buttonImage != null && panelImages[index] != null)
        {
            panelImages[index].sprite = buttonImage.sprite;
            panelImages[index].color = Color.white; // Đảm bảo ảnh không bị trong suốt
        }
    }
}
