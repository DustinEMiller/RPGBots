
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] private Quest _selectedQuest;
    private Step _selectedStep => _selectedQuest.CurrentStep;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _currentObjectivesText;
    [SerializeField] private Image _iconImage;

    [ContextMenu("Bind")]
    public void Bind()
    {
        _iconImage.sprite = _selectedQuest.Sprite;
        _nameText.SetText(_selectedQuest.DisplayName);
        _descriptionText.SetText(_selectedQuest.Description);
        _nameText.SetText(_selectedQuest.name);
        
        DisplayStepInstructionsObjectives();
    }

    public void DisplayStepInstructionsObjectives()
    {
        StringBuilder builder = new StringBuilder();
        if (_selectedStep != null)
        {
            builder.AppendLine(_selectedStep.Instructions);
            foreach (var objective in _selectedStep.Objectives)
            {
                string rgb = objective.IsCompleted ? "green" : "red";
                builder.AppendLine($"<color={rgb}>{objective}</color>");
            }
        }

        _currentObjectivesText.SetText(builder);
    }

    public void SelectQuest(Quest quest)
    {
        if (_selectedQuest)
        {
            _selectedQuest.Changed -= DisplayStepInstructionsObjectives;
        }
        
        _selectedQuest = quest;
        Bind();
        Show();

        _selectedQuest.Changed += DisplayStepInstructionsObjectives;
    }
}
