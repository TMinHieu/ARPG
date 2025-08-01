using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResetButton : MonoBehaviour
{
    public Button resetButton;
    public PlayerStats playerStats;
    public Transform playerTransform;
    public TextMeshProUGUI resetMessageText;

    private Vector3 defaultStartPosition;
    private string originalResetButtonText;

    void Start()
    {
        if (playerTransform != null)
            defaultStartPosition = playerTransform.position;

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnClickResetButton);

            // Lưu lại text gốc trên nút
            if (resetButton.GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                originalResetButtonText = resetButton.GetComponentInChildren<TextMeshProUGUI>().text;
            }
        }
    }

    void OnClickResetButton()
    {
        PlayerPrefs.DeleteKey("SaveData");

        if (playerStats != null)
        {
            playerStats.level = 1;
            playerStats.currentExp = 0;
            playerStats.str = 0;
            playerStats.vit = 0;
            playerStats.intel = 0;
            playerStats.expToNextLevel = 100;

            playerStats.currentHP = playerStats.maxHP;
            playerStats.currentMP = playerStats.maxMP;
            playerStats.RecalculateStats();
        }

        if (playerTransform != null)
            playerTransform.position = defaultStartPosition;

        if (QuestManager.Instance != null)
            QuestManager.Instance.ResetQuests();

        ShowResetMessage("Đã reset game!");
    }

    void ShowResetMessage(string message)
    {
        if (resetMessageText != null)
        {
            resetMessageText.text = message;
        }

        // Thay đổi text trên nút thành thông báo
        if (resetButton.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            resetButton.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }

        CancelInvoke(nameof(ClearMessage));
        Invoke(nameof(ClearMessage), 2f);
    }

    void ClearMessage()
    {
        if (resetMessageText != null)
            resetMessageText.text = "";

        // Khôi phục lại dòng chữ gốc trên nút
        if (resetButton.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            resetButton.GetComponentInChildren<TextMeshProUGUI>().text = originalResetButtonText;
        }
    }
}
