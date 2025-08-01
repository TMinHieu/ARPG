using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPanelManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] questButtons;         // Gồm ButtonQ1 → ButtonQ6
    public TextMeshProUGUI[] buttonTexts; // Gồm TextBQ1 → TextBQ6

    [Header("Panels")]
    public GameObject[] questPanels;      // Gồm PanelQ1 → PanelQ6

    [Header("Detail Texts (nội dung mô tả trong panel)")]
    public TextMeshProUGUI[] detailTexts; // Gồm TextDetail1 → TextDetail6

    private int currentActiveIndex = 0;

    void Start()
    {
        // Mặc định chỉ hiển thị quest 1
        for (int i = 0; i < questButtons.Length; i++)
        {
            int index = i;
            questButtons[i].onClick.AddListener(() => OnQuestButtonClick(index));

            // Ẩn tất cả các nút trừ Q1
            questButtons[i].gameObject.SetActive(i == 0);
        }

        // Gán tên và mô tả quest 1
        SetQuestInfo(0, "Tiêu diệt 10 Slime", "Tiêu diệt 10 con Slime quanh làng (0/10)");

        ActivatePanel(0); // Mặc định mở Panel 1
    }

    void OnQuestButtonClick(int index)
    {
        ActivatePanel(index);
    }

    void ActivatePanel(int index)
    {
        currentActiveIndex = index;

        for (int i = 0; i < questPanels.Length; i++)
        {
            bool isActive = i == index;
            questPanels[i].SetActive(isActive);

            if (buttonTexts[i] != null)
                buttonTexts[i].alpha = isActive ? 0.5f : 1f; // Làm mờ nút đang mở
        }
    }

    // Gọi khi muốn hiển thị một nhiệm vụ mới
    public void UnlockQuest(int index, string title, string detail)
    {
        if (index >= 0 && index < questButtons.Length)
        {
            questButtons[index].gameObject.SetActive(true);
            SetQuestInfo(index, title, detail);
        }
    }

    void SetQuestInfo(int index, string title, string detail)
    {
        if (buttonTexts[index] != null)
            buttonTexts[index].text = title;

        if (detailTexts[index] != null)
            detailTexts[index].text = detail;
    }
}
