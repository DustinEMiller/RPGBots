
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _activeQuests;
    [SerializeField] private QuestPanel _questPanel;
    [SerializeField] private List<Quest> _allQuests;
    public static QuestManager Instance { get; private set; }

    private void Awake() => Instance = this;

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }

    public void AddQuestByName(string questName)
    {
        var quest = _allQuests.FirstOrDefault(t => t.name == questName);
        if (quest != null)
        {
            AddQuest(quest);
        }
        else
        {
            Debug.Log("Missing quest {questName} attempted to add from dialog");
        }
    }
}


