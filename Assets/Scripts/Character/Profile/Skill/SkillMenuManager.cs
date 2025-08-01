using UnityEngine;
using UnityEngine.UI;

public class SkillMenuManager : MonoBehaviour
{
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    public GameObject skillQPanel;
    public GameObject skillETextPanel;
    public GameObject skillRTextPanel;

    void Start()
    {
        skill1Button.onClick.AddListener(() => ShowPanel(skillQPanel));
        skill2Button.onClick.AddListener(() => ShowPanel(skillETextPanel));
        skill3Button.onClick.AddListener(() => ShowPanel(skillRTextPanel));

        // Hiển thị panel đầu tiên khi bắt đầu
        ShowPanel(skillQPanel);
    }

    void ShowPanel(GameObject selectedPanel)
    {
        // Tắt tất cả panel trước
        skillQPanel.SetActive(false);
        skillETextPanel.SetActive(false);
        skillRTextPanel.SetActive(false);

        // Bật panel được chọn
        selectedPanel.SetActive(true);
    }
}
