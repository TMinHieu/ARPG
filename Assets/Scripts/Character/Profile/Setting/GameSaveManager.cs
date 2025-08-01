using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameSaveManager : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;
    public Transform playerTransform;
    public TextMeshProUGUI saveMessageText;

    [Header("Buttons")]
    public Button saveButton;
    public Button menuButton;

    private const string SaveKey = "SaveData";
    private string originalSaveButtonText;

    void Start()
    {
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(OnClickSaveButton);

            // Lưu lại text gốc của nút Save
            TextMeshProUGUI saveBtnText = saveButton.GetComponentInChildren<TextMeshProUGUI>();
            if (saveBtnText != null)
                originalSaveButtonText = saveBtnText.text;
        }

        if (menuButton != null)
            menuButton.onClick.AddListener(OnClickMenuButton);

        if (PlayerPrefs.HasKey(SaveKey))
        {
            LoadGame();
        }
    }

    public void OnClickSaveButton()
    {
        SaveGame();
        ShowSaveMessage("Đã lưu game thành công!");

        // Hiển thị tạm thời trên nút Save
        TextMeshProUGUI saveBtnText = saveButton.GetComponentInChildren<TextMeshProUGUI>();
        if (saveBtnText != null)
        {
            saveBtnText.text = "Đã lưu game!";
            CancelInvoke(nameof(RestoreSaveButtonText));
            Invoke(nameof(RestoreSaveButtonText), 2f);
        }
    }

    void RestoreSaveButtonText()
    {
        TextMeshProUGUI saveBtnText = saveButton.GetComponentInChildren<TextMeshProUGUI>();
        if (saveBtnText != null)
            saveBtnText.text = originalSaveButtonText;
    }

    public void OnClickMenuButton()
    {
        ShowSaveMessage("Chức năng Menu chưa làm");
        // SceneManager.LoadScene("MainMenu");
    }

    void SaveGame()
    {
        SaveData data = new SaveData
        {
            hp = playerStats.currentHP,
            mp = playerStats.currentMP,
            exp = playerStats.currentExp,
            level = playerStats.level,
            str = playerStats.str,
            vit = playerStats.vit,
            intel = playerStats.intel,
            expToNextLevel = playerStats.expToNextLevel,
            positionX = playerTransform.position.x,
            positionY = playerTransform.position.y,
            currentMainQuestIndex = QuestManager.Instance != null ? QuestManager.Instance.GetCurrentMainQuestIndex() : 0,
            questProgressList = QuestManager.Instance != null ? QuestManager.Instance.GetQuestProgress() : new List<QuestManager.QuestProgressData>()
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    void LoadGame()
    {
        string json = PlayerPrefs.GetString(SaveKey);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        playerStats.level = data.level;
        playerStats.currentHP = data.hp;
        playerStats.currentMP = data.mp;
        playerStats.currentExp = data.exp;
        playerStats.str = data.str;
        playerStats.vit = data.vit;
        playerStats.intel = data.intel;
        playerStats.expToNextLevel = data.expToNextLevel;

        playerTransform.position = new Vector3(data.positionX, data.positionY, 0);
        playerStats.RecalculateStats();

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.LoadQuestProgress(data.questProgressList);
            QuestManager.Instance.SetCurrentMainQuestIndex(data.currentMainQuestIndex);
        }
    }

    public void ShowSaveMessage(string message)
    {
        if (saveMessageText != null)
        {
            saveMessageText.text = message;
            CancelInvoke(nameof(ClearMessage));
            Invoke(nameof(ClearMessage), 2f);
        }
    }

    void ClearMessage()
    {
        if (saveMessageText != null)
        {
            saveMessageText.text = "";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int hp, mp, exp, level, str, vit, intel;
        public int expToNextLevel;
        public float positionX, positionY;

        public int currentMainQuestIndex;
        public List<QuestManager.QuestProgressData> questProgressList;
    }
}
