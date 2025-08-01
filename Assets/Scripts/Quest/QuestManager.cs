using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("UI hiển thị nhiệm vụ")]
    public List<TextMeshProUGUI> mainQuestTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> profileQuestTitleTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> profileQuestDetailTexts = new List<TextMeshProUGUI>();
    public List<Button> profileQuestButtons = new List<Button>();

    [System.Serializable]
    public class QuestData
    {
        public string questId;
        public string title;
        public string description;
        public string targetEnemyName;
        public int goalAmount;

        [Header("Phần thưởng")]
        public int rewardExp;
        public int rewardStr;
        public int rewardVit;
        public int rewardInt;
    }

    [Header("Cấu hình danh sách nhiệm vụ")]
    public List<QuestData> questConfigs = new List<QuestData>();

    private List<Quest> activeQuests = new List<Quest>();
    private int currentMainQuestIndex = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        foreach (var config in questConfigs)
        {
            Quest quest = new Quest(config);
            activeQuests.Add(quest);
        }

        UpdateQuestUI();
    }

    public void OnEnemyKilled(string enemyName)
    {
        foreach (var quest in activeQuests)
        {
            if (!quest.IsCompleted())
            {
                quest.OnEnemyKilled(enemyName);

                if (quest.IsCompleted())
                {
                    ShowReward(quest);
                    UpdateQuestUI();
                    return;
                }
            }
        }

        UpdateQuestUI();
    }

    void UpdateQuestUI()
    {
        List<Quest> visibleQuests = activeQuests.FindAll(q => !q.IsCompleted());

        // Hiển thị nhiệm vụ chính đầu tiên (main quest)
        if (mainQuestTexts.Count > 0 && mainQuestTexts[0] != null)
        {
            if (currentMainQuestIndex < activeQuests.Count && !activeQuests[currentMainQuestIndex].IsCompleted())
            {
                var quest = activeQuests[currentMainQuestIndex];
                mainQuestTexts[0].text = $"Nhiệm vụ: {quest.title}\nTiến độ: {quest.GetProgressText()}";
            }
            else
            {
                mainQuestTexts[0].text = "Tất cả nhiệm vụ đã hoàn thành!";
            }
        }

        for (int i = 0; i < profileQuestTitleTexts.Count; i++)
        {
            if (i < visibleQuests.Count)
            {
                var quest = visibleQuests[i];

                profileQuestTitleTexts[i].text = quest.title;
                profileQuestDetailTexts[i].text = $"{quest.description}\nTiến độ: {quest.GetProgressText()}";

                if (profileQuestButtons.Count > i && profileQuestButtons[i] != null)
                    profileQuestButtons[i].gameObject.SetActive(true);
            }
            else
            {
                profileQuestTitleTexts[i].text = "";
                profileQuestDetailTexts[i].text = "";
                if (profileQuestButtons.Count > i && profileQuestButtons[i] != null)
                    profileQuestButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void ShowReward(Quest quest)
    {
        string message = $"Hoàn thành nhiệm vụ: {quest.title}!\n";

        if (quest.rewardExp > 0) message += $" +{quest.rewardExp} EXP\n";
        if (quest.rewardStr > 0) message += $" +{quest.rewardStr} STR\n";
        if (quest.rewardVit > 0) message += $" +{quest.rewardVit} VIT\n";
        if (quest.rewardInt > 0) message += $" +{quest.rewardInt} INT\n";

        if (mainQuestTexts.Count > 0 && mainQuestTexts[0] != null)
        {
            mainQuestTexts[0].text = message;
        }

        // Tự động tăng chỉ số (nếu có PlayerStats.Instance thì cộng vào đây)
        // Ví dụ: PlayerStats.Instance.str += quest.rewardStr;

        currentMainQuestIndex++;
    }

    // ------------------ Quest Class ------------------

    [System.Serializable]
    public class Quest
    {
        public string questId;
        public string title;
        public string description;
        public string targetEnemyName;
        public int goalAmount;
        public int currentAmount;

        public int rewardExp;
        public int rewardStr;
        public int rewardVit;
        public int rewardInt;

        public Quest(QuestData data)
        {
            questId = data.questId;
            title = data.title;
            description = data.description;
            targetEnemyName = data.targetEnemyName;
            goalAmount = data.goalAmount;

            rewardExp = data.rewardExp;
            rewardStr = data.rewardStr;
            rewardVit = data.rewardVit;
            rewardInt = data.rewardInt;

            currentAmount = 0;
        }

        public void OnEnemyKilled(string enemyName)
        {
            if (enemyName == targetEnemyName && !IsCompleted())
            {
                currentAmount++;
                if (currentAmount > goalAmount) currentAmount = goalAmount;
            }
        }

        public bool IsCompleted()
        {
            return currentAmount >= goalAmount;
        }

        public string GetProgressText()
        {
            return $"{currentAmount} / {goalAmount}";
        }

        public QuestProgressData ToProgressData()
        {
            return new QuestProgressData
            {
                questId = this.questId,
                title = this.title,
                description = this.description,
                targetEnemyName = this.targetEnemyName,
                goalAmount = this.goalAmount,
                currentAmount = this.currentAmount,
                rewardExp = this.rewardExp,
                rewardStr = this.rewardStr,
                rewardVit = this.rewardVit,
                rewardInt = this.rewardInt
            };
        }

        public void LoadFromProgressData(QuestProgressData data)
        {
            currentAmount = data.currentAmount;
        }
    }

    [System.Serializable]
    public class QuestProgressData
    {
        public string questId;
        public string title;
        public string description;
        public string targetEnemyName;
        public int goalAmount;
        public int currentAmount;
        public int rewardExp, rewardStr, rewardVit, rewardInt;
    }

    // ------------------ SAVE & LOAD ------------------

    public List<QuestProgressData> GetQuestProgress()
    {
        List<QuestProgressData> data = new List<QuestProgressData>();
        foreach (var quest in activeQuests)
        {
            data.Add(quest.ToProgressData());
        }
        return data;
    }

    public void LoadQuestProgress(List<QuestProgressData> savedData)
    {
        activeQuests.Clear();

        foreach (var data in savedData)
        {
            Quest quest = new Quest(new QuestData
            {
                questId = data.questId,
                title = data.title,
                description = data.description,
                targetEnemyName = data.targetEnemyName,
                goalAmount = data.goalAmount,
                rewardExp = data.rewardExp,
                rewardStr = data.rewardStr,
                rewardVit = data.rewardVit,
                rewardInt = data.rewardInt
            });
            quest.LoadFromProgressData(data);
            activeQuests.Add(quest);
        }

        UpdateQuestUI();
    }

    public int GetCurrentMainQuestIndex()
    {
        return currentMainQuestIndex;
    }

    public void SetCurrentMainQuestIndex(int index)
    {
        currentMainQuestIndex = index;
        UpdateQuestUI();
    }

    public void ResetQuests()
    {
        activeQuests.Clear();
        foreach (var config in questConfigs)
        {
            Quest quest = new Quest(config);
            activeQuests.Add(quest);
        }
        currentMainQuestIndex = 0;
        UpdateQuestUI();
    }
}
