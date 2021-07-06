using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private QuestGiver m_QuestGiver;
    [SerializeField] private Quest _quest;

    public QuestGiver(QuestGiver questGiver)
    {
        m_QuestGiver = questGiver;
    }

    public void AddQuest()
    {
        QuestManager.Instance.AddQuest(_quest);
    }
}