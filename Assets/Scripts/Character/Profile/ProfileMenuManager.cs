using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProfileMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button btnCharacter;
    public Button btnInventory;
    public Button btnSkill;
    public Button btnParty;
    public Button btnQuest;
    public Button btnSettings;

    [Header("Panels")]
    public GameObject characterPanel;
    public GameObject inventoryPanel;
    public GameObject skillPanel;
    public GameObject partyPanel;
    public GameObject questPanel;
    public GameObject settingsPanel;

    private Dictionary<Button, GameObject> menuMap;

    void Start()
    {
        // Gán mỗi button tương ứng với 1 panel
        menuMap = new Dictionary<Button, GameObject>()
        {
            { btnCharacter, characterPanel },
            { btnInventory, inventoryPanel },
            { btnSkill, skillPanel },
            { btnParty, partyPanel },
            { btnQuest, questPanel },
            { btnSettings, settingsPanel }
        };

        // Gán sự kiện click
        btnCharacter.onClick.AddListener(() => SwitchTab(btnCharacter));
        btnInventory.onClick.AddListener(() => SwitchTab(btnInventory));
        btnSkill.onClick.AddListener(() => SwitchTab(btnSkill));
        btnParty.onClick.AddListener(() => SwitchTab(btnParty));
        btnQuest.onClick.AddListener(() => SwitchTab(btnQuest));
        btnSettings.onClick.AddListener(() => SwitchTab(btnSettings));

        // Mở tab đầu tiên mặc định
        SwitchTab(btnCharacter);
    }

    void SwitchTab(Button selectedButton)
    {
        foreach (var pair in menuMap)
        {
            bool isActive = pair.Key == selectedButton;
            pair.Value.SetActive(isActive);

            // Làm mờ nút đang chọn
            ColorBlock colors = pair.Key.colors;
            colors.normalColor = isActive ? new Color(1f, 1f, 1f, 0.5f) : Color.white;
            pair.Key.colors = colors;
        }
    }
}
