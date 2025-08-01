using UnityEngine;
using UnityEngine.UI;

public class BagMenuController : MonoBehaviour
{
    [Header("Menu Buttons")]
    public Button B1Button;
    public Button B2Button;
    public Button B3Button;
    public Button B4Button;

    [Header("Panels")]
    public GameObject B1Panel;
    public GameObject B2Panel;
    public GameObject B3Panel;
    public GameObject B4Panel;

    [Header("Button Images (optional for visual state)")]
    public Image B1Image;
    public Image B2Image;
    public Image B3Image;
    public Image B4Image;

    public Color selectedColor = new Color(1f, 1f, 1f, 0.5f); // Mờ
    public Color normalColor = new Color(1f, 1f, 1f, 1f);     // Bình thường

    void Start()
    {
        // Gán sự kiện click
        B1Button.onClick.AddListener(() => ShowPanel(1));
        B2Button.onClick.AddListener(() => ShowPanel(2));
        B3Button.onClick.AddListener(() => ShowPanel(3));
        B4Button.onClick.AddListener(() => ShowPanel(4));

        ShowPanel(1); // Mặc định mở B1
    }

    void ShowPanel(int index)
    {
        // Hiển thị đúng panel được chọn
        B1Panel.SetActive(index == 1);
        B2Panel.SetActive(index == 2);
        B3Panel.SetActive(index == 3);
        B4Panel.SetActive(index == 4);

        // Cập nhật trạng thái nút (làm mờ nút đang chọn)
        if (B1Image != null) B1Image.color = (index == 1) ? selectedColor : normalColor;
        if (B2Image != null) B2Image.color = (index == 2) ? selectedColor : normalColor;
        if (B3Image != null) B3Image.color = (index == 3) ? selectedColor : normalColor;
        if (B4Image != null) B4Image.color = (index == 4) ? selectedColor : normalColor;
    }
}
